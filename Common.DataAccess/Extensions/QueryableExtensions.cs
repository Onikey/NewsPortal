using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.DataAccess.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties)
            where T : class
        {
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }

        public static IQueryable<TEntity> Order<TEntity>(this IQueryable<TEntity> source, SortDirections direction, string[] properties)
        {
            string o;

            switch (direction)
            {
                case SortDirections.ZtoA:
                    o = "Descending";
                    break;
                default:
                    o = string.Empty;
                    break;
            }

            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, "e");

            var order = "OrderBy" + o;
            var result = source;

            foreach (var property in properties)
            {
                Type propertyType;
                var propertyAccess = MakeMemberAccess(type, parameter, property, out propertyType);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var resultExpression = Expression.Call(typeof(Queryable), order, new[] { type, propertyType },
                    result.Expression, Expression.Quote(orderByExpression));

                result = result.Provider.CreateQuery<TEntity>(resultExpression);

                order = "ThenBy" + o;
            }

            return result;
        }

        public static IQueryable<TEntity> Order<TEntity>(this IQueryable<TEntity> source, SortDirections direction, string ordering)
        {
            if (ordering.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
            {
                return source.Order(direction, ordering.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }

            string order;

            switch (direction)
            {
                case SortDirections.ZtoA:
                    order = "OrderByDescending";
                    break;
                default:
                    order = "OrderBy";
                    break;
            }

            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, "e");

            Type propertyType;
            var propertyAccess = MakeMemberAccess(type, parameter, ordering, out propertyType);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), order, new[] { type, propertyType },
                source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        private static Expression MakeMemberAccess(Type type, Expression parameter, string propertyPath, out Type propertyType)
        {
            Expression expression = parameter;
            var currentType = type;

            foreach (var field in propertyPath.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var property = currentType.GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expression = Expression.MakeMemberAccess(expression, property);
                currentType = property.PropertyType;
            }

            propertyType = currentType;

            return expression;
        }
    }
}