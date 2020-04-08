using System;
using System.Linq;
using System.Linq.Expressions;

namespace SBAccountAPI.Controllers
{
    public static class QueryableHelper
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> source, string columnName, string keyword)
        {
            var arg = Expression.Parameter(typeof(T), "p");
            var body = Expression.Call(Expression.Property(arg, columnName),"Contains",null,Expression.Constant(keyword));
            var predicate = Expression.Lambda<Func<T, bool>>(body, arg);
            return source.Where(predicate);
        }

        public static PagedResult<T> GetTable<T>(this IQueryable<T> source, PagedResult<T> pagedResult) where T : class
        {
            IQueryable<T> result = source.Where(pagedResult.Searchable, pagedResult.Search)
              .OrderBy(pagedResult.SortColumn, pagedResult.IsAsc)
              .Skip((pagedResult.CurrentPage - 1) * pagedResult.PageSize)
              .Take(pagedResult.PageSize);

            int CurrentPage = pagedResult.CurrentPage;
            int PageSize = pagedResult.PageSize;
            int TotalCount = source.Where(pagedResult.Searchable, pagedResult.Search).Count(); 
            int TotalPages = (int)System.Math.Ceiling(TotalCount / (double)PageSize);

            pagedResult.RowCount = TotalCount;
            pagedResult.PageSize = PageSize;
            pagedResult.CurrentPage = CurrentPage;
            pagedResult.PageCount = TotalPages;
            pagedResult.Results = result.ToList();

            return pagedResult;
        }
    }
}