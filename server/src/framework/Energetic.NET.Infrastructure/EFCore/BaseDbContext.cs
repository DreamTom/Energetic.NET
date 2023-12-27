using Energetic.NET.SharedKernel.IModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Energetic.NET.Infrastructure.EFCore
{
    public abstract class BaseDbContext(DbContextOptions dbContextOptions,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : DbContext(dbContextOptions)
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;

        protected abstract string TablePrefix { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var config = App.GetConfigOptions<DbConnectionConfigOptions>();
            if (config.EnableSoftDeletionFilter)
                modelBuilder.EnableSoftDeletionGlobalFilter();
            if (!config.ToUnderline)
                return;
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Replace table names
                string? tableName = default;
                if (!string.IsNullOrWhiteSpace(TablePrefix))
                    tableName = TablePrefix + entity.GetTableName()?.ToSnakeCase();
                entity.SetTableName(tableName);

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName()?.ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName()?.ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName()?.ToSnakeCase());
                }
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            var config = App.GetConfigOptions<DbConnectionConfigOptions>();
            configurationBuilder.Properties<string>().AreUnicode(false).HaveMaxLength(config.FieldDefaultLength);
            configurationBuilder.Properties<DateTime>().HavePrecision(3);
        }

        public override int SaveChanges()
        {
            var softDeletedEntities = ChangeTracker.Entries<IDeletedEntity>()
                .Where(e => e.State == EntityState.Modified && e.Entity.IsDeleted)
                .Select(e => e.Entity).ToList();//在提交到数据库之前，记录那些被“软删除”实体对象。一定要ToList()，否则会延迟到ForEach的时候才执行
            var result = base.SaveChanges();
            softDeletedEntities.ForEach(e => Entry(e).State = EntityState.Detached);//把被软删除的对象从Cache删除，否则FindAsync()还能根据Id获取到这条数据
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var softDeletedEntities = ChangeTracker.Entries<IDeletedEntity>()
                .Where(e => e.State == EntityState.Modified && e.Entity.IsDeleted)
                .Select(e => e.Entity).ToList();//在提交到数据库之前，记录那些被“软删除”实体对象。一定要ToList()，否则会延迟到ForEach的时候才执行
            var result = await base.SaveChangesAsync(cancellationToken);
            softDeletedEntities.ForEach(e => Entry(e).State = EntityState.Detached);//把被软删除的对象从Cache删除，否则FindAsync()还能根据Id获取到这条数据
            //因为FindAsync如果能从本地Cache找到，就不会去数据库上查询，而从本地Cache找的过程中不会管QueryFilter
            //就会造成已经软删除的数据仍然能够通过FindAsync查到的问题，因此这里把对应跟踪对象的state改为Detached，就会从缓存中删除了
            return result;
        }
    }
}
