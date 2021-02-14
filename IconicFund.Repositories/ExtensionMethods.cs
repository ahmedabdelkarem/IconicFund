using IconicFund.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace IconicFund.Repositories
{
    public static class ExtensionMethods
    {
        public static IQueryable<TEntity> FindQuery<TEntity>(this DbSet<TEntity> set, params object[] keyValues) where TEntity : class
        {
            var context = ((IInfrastructure<IServiceProvider>)set).GetService<IconicFundDbContext>();

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            var entries = context.ChangeTracker.Entries<TEntity>();
            //Edit to find with the full entity object
            var i = 0;
            if (Convert.GetTypeCode(keyValues[0]) == TypeCode.Object)//is object
            {
                var newKeyValues = new object[key.Properties.Count];
                var entity = keyValues[0];
                i = 0;
                foreach (var property in key.Properties)
                {
                    newKeyValues[i] = entity.GetType().GetProperty(property.Name).GetValue(entity);
                    i++;
                }
                keyValues = newKeyValues;
            }

            i = 0;
            foreach (var property in key.Properties)
            {
                var keyValue = keyValues[i];
                entries = entries.Where(e => e.Property(property.Name).CurrentValue == keyValue);
                i++;
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.AsQueryable();
            i = 0;
            foreach (var property in key.Properties)
            {
                var propertyName = key.Properties[i].Name;
                Type clrType = key.Properties[i].ClrType;
                var keyValue = TypeDescriptor.GetConverter(key.Properties[i].ClrType).ConvertFromInvariantString(Convert.ToString(keyValues[i]));

                query = query.Where((Expression<Func<TEntity, bool>>)Expression.Lambda(
                            Expression.Equal(Expression.Property(parameter, propertyName), Expression.Constant(keyValue)),
                            parameter)
                        );
                i++;
            }
            return query;
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
                          bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
