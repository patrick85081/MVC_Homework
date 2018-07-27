using System.Linq;
using System.Linq.Dynamic;

namespace MVC_Homework.Models.ViewModels
{
    public static class QueryExtension
    {
        public static IQueryable<TSource> GetCurrentPage<TSource>(this IQueryable<TSource> source, QueryOption queryOption) =>
            source.OrderBy(queryOption.GetSortString())
                .GetCurrentPage(queryOption.Page, queryOption.GetPageSize());

        private static IQueryable<TSource> GetCurrentPage<TSource>(this IQueryable<TSource> source, int page = 1,
            int pageSize = 10)
        {
            return source.Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public static int GetPageCount<TSource>(this IQueryable<TSource> source, QueryOption queryOption) =>
            source.GetPageCount(queryOption.GetPageSize());

        private static int GetPageCount<TSource>(this IQueryable<TSource> source, int pageSize = 10)
        {
            int count = source.Count();
            return (count / pageSize) + (count % pageSize > 0 ? 1 : 0);
        }
    }
}