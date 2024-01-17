using Energetic.NET.Infrastructure;
using IdGen;
using MediatR;
using Microsoft.Extensions.Options;

namespace Energetic.NET.Basic.Infrastructure
{
    public class BasicDbContext(DbContextOptions<BasicDbContext> dbContextOptions,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
        IIdGenerator<long> idGenerator, IOptions<DbConnectionConfigOptions> dbConnectionConfig, IMediator? mediator)
        : BaseDbContext(dbContextOptions, auditableEntitySaveChangesInterceptor,
            idGenerator, dbConnectionConfig, mediator)
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

        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }
    }
}
