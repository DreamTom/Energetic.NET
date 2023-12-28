using Energetic.NET.Infrastructure;
using Energetic.NET.Infrastructure.EFCore;
using IdGen;
using Microsoft.Extensions.Options;

namespace Energetic.NET.Basic.Infrastructure
{
    public class BasicDbContext(DbContextOptions<BasicDbContext> dbContextOptions,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
        IIdGenerator<long> idGenerator, IOptions<DbConnectionConfigOptions> dbConnectionConfig)
        : BaseDbContext(dbContextOptions, auditableEntitySaveChangesInterceptor, idGenerator, dbConnectionConfig)
    {
        protected override string TablePrefix => "sys_";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<FileAttachment> FileAttachments { get; set; }
    }
}
