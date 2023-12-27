using Energetic.NET.SharedKernel.IModels;
using Microsoft.EntityFrameworkCore;
using static System.Linq.Expressions.Expression;

namespace Energetic.NET.Infrastructure.EFCore
{
    public static class EFCoreExtensions
    {
        /// <summary>
        /// 开启全局软删除过滤
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void EnableSoftDeletionGlobalFilter(this ModelBuilder modelBuilder)
        {
            var entityTypesHasSoftDeletion = modelBuilder.Model.GetEntityTypes()
                .Where(e => e.ClrType.IsAssignableTo(typeof(IDeletedEntity)));

            foreach (var entityType in entityTypesHasSoftDeletion)
            {
                var isDeletedProperty = entityType.FindProperty(nameof(IDeletedEntity.IsDeleted));
                if (isDeletedProperty == null || isDeletedProperty.PropertyInfo == null)
                    continue;
                var parameter = Parameter(entityType.ClrType, "p");
                var filter = Lambda(Not(Property(parameter, isDeletedProperty.PropertyInfo)), parameter);
                entityType.SetQueryFilter(filter);
            }
        }
    }
}
