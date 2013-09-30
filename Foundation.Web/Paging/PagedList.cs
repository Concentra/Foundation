using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.Web.Paging
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize) :
            this(PageListExtensions.GetPage(source, pageIndex, pageSize), pageIndex, pageSize, x => x.Count())
        {
        }

        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, Func<IEnumerable<T>, int> totalFunc)
        {
            var sourceAsList = source as IList<T> ?? source.ToList();

            var totalCount = totalFunc(sourceAsList);

            this.AddRange(sourceAsList);

            int total = totalCount;

            var totalPages = (int)Math.Ceiling((decimal)total / pageSize);

            var showingFrom = (pageIndex * pageSize) + 1;

            var showingTo = totalPages == (pageIndex + 1) ? total : (showingFrom - 1) + pageSize;

            this.PagingInfo = new PagingInfo()
            {
                ShowingFrom = showingFrom,
                ShowingTo = showingTo,
                TotalItems = total,
                TotalPages = totalPages,
                PageNumber = pageIndex,
                PageSize = pageSize
            };
        }

        public PagingInfo PagingInfo { get; private set; }
    }
}