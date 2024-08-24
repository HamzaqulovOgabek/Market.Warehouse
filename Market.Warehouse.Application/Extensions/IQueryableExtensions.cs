using Market.Warehouse.Application.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Market.Warehouse.Application.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> SortFilter<T>(this IQueryable<T> query,
        BaseSortFilterDto options, params string[] searchingProperties)
    {
        if (options.HasSearch)
            query = Search2(query, options, searchingProperties);

        // Paging logic
        query = query
            .Skip(options.PageSize * (options.Page - 1))
            .Take(options.PageSize);

        // Sorting logic
        if (options.HasSort)
            query = query.OrderByProperty<T>(propertyName: options.SortBy, descending: options.SortType.ToLower() == "desc");

        return query;
    }

    private static IQueryable<T> Search<T>(IQueryable<T> query,
        BaseSortFilterDto options,
        string[] searchingProperties)

    {
        if (options.HasSearch && searchingProperties.Length > 0)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            Expression? predicate = null;

            foreach (var property in searchingProperties)
            {
                var propertyInfo = typeof(T).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);

                    // Create the EF.Functions.Like method call expression
                    var likeMethod = typeof(EF).GetMethod("Like", new[] { typeof(string), typeof(string) });
                    if (likeMethod != null)
                    {
                        var likeExpression = Expression.Call(
                            likeMethod,
                            propertyAccess,
                            Expression.Constant($"%{options.SearchingWord}%")
                        );


                        predicate = predicate == null ? likeExpression : Expression.OrElse(predicate, likeExpression);
                    }
                }
            }

            if (predicate != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }
        }

        return query;
    }
    private static IQueryable<T> Search2<T>(IQueryable<T> query,
        BaseSortFilterDto options,
        string[] searchingProperties)

    {
        if (options.HasSearch && searchingProperties.Length > 0)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            Expression? predicate = null;

            foreach (var property in searchingProperties)
            {
                var propertyInfo = typeof(T).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
                    Expression propertyValueExpression = propertyInfo.PropertyType == typeof(string)
                   ? propertyAccess
                   : Expression.Call(propertyAccess, "ToString", null, null);

                    var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                    if (containsMethod != null)
                    {
                        var searchTerm = Expression.Constant(options.SearchingWord, typeof(string));
                        var containsExpression = Expression.Call(propertyValueExpression, containsMethod, searchTerm);

                        predicate = predicate == null ? containsExpression : Expression.OrElse(predicate, containsExpression);
                    }
                }
            }

            if (predicate != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
                query = query.Where(lambda);
            }
        }

        return query;
    }

    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool descending = false)
    {
        var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
        {
            throw new ArgumentException($"Property '{propertyName}' does not exist on type '{typeof(T)}'.");
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        string methodName = descending ? "OrderByDescending" : "OrderBy";
        var method = typeof(Queryable).GetMethods()
                                       .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                                       .Single()
                                       .MakeGenericMethod(typeof(T), property.PropertyType);


        var result = Expression.Call(
            method,
            source.Expression,
            orderByExpression
        );

        // Return the queryable result
        return source.Provider.CreateQuery<T>(result);

        
    }
}
