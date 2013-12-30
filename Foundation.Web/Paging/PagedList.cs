using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Foundation.Web.Paging
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
       
        internal PagedList(IEnumerable<T> source, int pageIndex, int pageSize) :
            this(source.GetPage(pageIndex, pageSize), pageIndex, pageSize, x => x.Count())
        {
        }

        internal PagedList(IEnumerable<T> source, int pageIndex, int pageSize, Func<IEnumerable<T>, int> totalFunc)
        {
            
            var sourceAsList = source as IList<T> ?? source.ToList();

            var totalCount = totalFunc(sourceAsList);

            this.AddRange(sourceAsList);

            int total = totalCount;

            var totalPages = (int)Math.Ceiling((decimal)total / pageSize);

            var showingFrom = (pageIndex * pageSize) + 1;

            var showingTo = totalPages == (pageIndex + 1) ? total : (showingFrom - 1) + pageSize;

            this.PagedQueryResults = new PagingResults(total, totalPages, showingFrom, showingTo, pageSize, pageIndex);

            this.PagingViewModel = new PagingInfoViewModel()
            {
                ShowingFrom = showingFrom,
                ShowingTo = showingTo,
                TotalItems = total,
                TotalPages = totalPages,
                PageNumber = pageIndex,
                PageSize = pageSize
            };
        }

        public PagingResults PagedQueryResults { get; private set; }

        public PagingInfoViewModel PagingViewModel { get; private set; }
    }
}