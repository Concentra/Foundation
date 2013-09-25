using System.Collections.Generic;

namespace Foundation.Persistence
{
    public interface IPagedList<T> : IList<T>
    {
        PagingInfo PagingInfo { get; }
    }
}