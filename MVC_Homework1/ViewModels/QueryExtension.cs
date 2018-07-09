using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Homework1.ViewModels
{
    public static class QueryExtension
    {
        public static IQueryable<TSource> GetPage<TSource>(this IQueryable<TSource> source, QueryOption queryOption) =>
            source.GetPage(queryOption.Page);

        public static IQueryable<TSource> GetPage<TSource>(this IQueryable<TSource> source, int page = 1,
            int pageSize = 10)
        {
            return source.Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public static int GetPageCount<TSource>(this IQueryable<TSource> source, int pageSize = 10)
        {
            return source.Count() / pageSize + 1;
        }
    }
}