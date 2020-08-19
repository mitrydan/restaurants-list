using System;

namespace RestaurantsList.Api.ApiResponses
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public Uri FirstPage { get; set; }

        public Uri LastPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public Uri NextPage { get; set; }

        public Uri PreviousPage { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
            : base(data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
