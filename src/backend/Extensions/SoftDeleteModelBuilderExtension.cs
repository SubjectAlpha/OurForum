using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OurForum.Backend.Entities;

namespace OurForum.Backend.Extensions
{
    internal static class SoftDeleteModelBuilderExtension
    {
        public static ModelBuilder ApplySoftDeleteQueryFilter(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(Base).IsAssignableFrom(entityType.ClrType))
                {
                    continue;
                }

                var param = Expression.Parameter(entityType.ClrType, "entity");
                var prop = Expression.PropertyOrField(param, nameof(Base.IsDeleted));
                var entityNotDeleted = Expression.Lambda(
                    Expression.Equal(prop, Expression.Constant(false)),
                    param
                );

                entityType.SetQueryFilter(entityNotDeleted);
            }

            return modelBuilder;
        }
    }
}
