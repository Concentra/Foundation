using System.Collections.Generic;

namespace Foundation.Web.Paging
{
    public interface IPagedList<T> : IList<T>
    {
        PagingInfo PagingInfo { get; }
    }
}