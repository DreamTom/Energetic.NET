using Energetic.NET.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Energetic.NET.Basic.Infrastructure
{
    public class BasicDbContext(DbContextOptions<BasicDbContext> dbContextOptions,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : BaseDbContext(dbContextOptions, auditableEntitySaveChangesInterceptor)
    {
        protected override string TablePrefix => "sys_";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
