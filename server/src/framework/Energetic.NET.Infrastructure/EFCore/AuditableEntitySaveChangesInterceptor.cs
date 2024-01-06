using Energetic.NET.SharedKernel;
using Energetic.NET.SharedKernel.IModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Energetic.NET.Infrastructure.EFCore
{
    public class AuditableEntitySaveChangesInterceptor(ICurrentUserService currentUserService,
         IDateTimeService dateTimeService) : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IDateTimeService _dateTimeService = dateTimeService;

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext == null) return;
            string realName = string.Empty;
            if (_currentUserService.CurrentUser != null && !string.IsNullOrWhiteSpace(_currentUserService.CurrentUser.RealName))
                realName = _currentUserService.CurrentUser.RealName;

            long userId = _currentUserService.CurrentUser == null ? 0 : _currentUserService.CurrentUser.Id;
            foreach (var entry in dbContext.ChangeTracker.Entries<ICreatedEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created(userId, realName, _dateTimeService.Now);
                }
            }
            foreach (var entry in dbContext.ChangeTracker.Entries<ICreatedEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created(userId, realName, _dateTimeService.Now);
                }
            }
            foreach (var entry in dbContext.ChangeTracker.Entries<IUpdatedEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.Updated(userId, realName, _dateTimeService.Now);
                }
            }
            foreach (var entry in dbContext.ChangeTracker.Entries<IDeletedEntity>())
            {
                if (entry.State == EntityState.Modified && entry.Entity.IsDeleted)
                {
                    entry.Entity.Deleted(userId, realName, _dateTimeService.Now);
                }
                if (entry.State == EntityState.Modified && !entry.Entity.IsDeleted)
                {
                    entry.Entity.Recover();
                }
            }
        }
    }
}
