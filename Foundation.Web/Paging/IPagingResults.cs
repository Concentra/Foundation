using System;

namespace Foundation.Web.Paging
{
    public interface IPagingResults
    {
        int TotalItems { get;  }
        int TotalPages { get;  }
        int ShowingFrom { get;  }
        int ShowingTo { get;  }
        Func<object, string> ActionFunc { get;  }
        int PageSize { get; }
        int PageNumber { get; }
    }
}