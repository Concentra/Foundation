using System;

namespace Foundation.Persistence
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public int ShowingFrom { get; set; }

        public int ShowingTo { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public Func<object, string> ActionFunc { get; set; }
    }
}
