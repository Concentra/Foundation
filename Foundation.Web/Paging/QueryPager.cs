using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NHibernate.Linq;

namespace Foundation.Web.Paging
{
    public static class PageListExtensions
    {
        private static int PageSize
        {
            get
            {
                var configuredPageSize = ConfigurationManager.AppSettings["Foundation_PageSize"];
                return string.IsNullOrEmpty(configuredPageSize) ? 15 : Convert.ToInt32(configuredPageSize);
            }
        }

        internal static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = PageSize;
            }

            return source.Skip(pageIndex * pageSize)
                .Take(pageSize);
        }

        public static IPagedList<T> FetchPaged<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = PageSize;
            }
            
            var futureCount = query.ToFutureValue(x => Queryable.Count<T>(x));

            var queryPaged = query.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToFuture();

            return new PagedList<T>(queryPaged,(pageIndex - 1), pageSize, x => futureCount.Value);
        }

        public static IPagedList<T> FetchPaged<T>(this IQueryable<T> parentQuery, IQueryable<T> queryWithChildren, int pageIndex, int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = PageSize;
            }
            
            var futureCount = parentQuery.Count();
            return new PagedList<T>(
                queryWithChildren.Skip((pageIndex - 1) * pageSize).Take(pageSize),
                (pageIndex - 1),
                pageSize,
                x => futureCount);
        }

        public static IPagedList<T> FetchPaged<T>(this IQueryable<T> query, IQueryable<T> queryWithChildren, IQueryable<T> childrenQueryWithGrandchildren, int pageIndex, int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = PageSize;
            }

            var futureCount = query.ToFutureValue(x => x.Count());

            var values1 = queryWithChildren.ToFuture();
            childrenQueryWithGrandchildren.ToFuture();

            var allValues = values1.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PagedList<T>(
                allValues,
                (pageIndex - 1),
                pageSize,
                x => futureCount.Value);
        }
    }
}
