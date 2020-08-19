using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using RestaurantsList.Api.ApiResponses;
using RestaurantsList.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantsList.Api.Extensions
{
    public static class ApiResponseExtensions
    {
        public static bool IsOk(this IEnumerable<object> enumerable) =>
            enumerable != null && enumerable.Count() > 0;

        public static bool IsOk(this object obj) =>
            obj != null;

        public static ErrorResponse CreateNotFoundApiResponse(this object obj) =>
            new ErrorResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "Could not found"
            };

        public static Response<TData> CreateApiResponse<TData>(this TData data) =>
            new Response<TData>(data);

        public static Response<TData> CreatePagedApiResponse<TData>(
            this TData data,
            PagedReqest pagedReqest,
            int totalCount,
            HttpRequest request)
            where TData : IEnumerable<object>
        {
            var enpoint = request == null 
                ? "http://localhost:8080/"
                : string.Concat(
                    request.Scheme,
                    "://",
                    request.Host.ToUriComponent(),
                    request.Path.Value);

            var totalRecords = totalCount;
            var totalPages = ((double)totalRecords / (double)pagedReqest.PageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            return new PagedResponse<TData>(data, pagedReqest.PageNumber, pagedReqest.PageSize)
            {
                TotalRecords = totalRecords,
                TotalPages = roundedTotalPages,
                NextPage = pagedReqest.PageNumber >= 1 && pagedReqest.PageNumber < roundedTotalPages
                    ? GetPageUri(enpoint, pagedReqest.PageNumber + 1, pagedReqest.PageSize)
                    : null,
                PreviousPage = pagedReqest.PageNumber - 1 >= 1 && pagedReqest.PageNumber <= roundedTotalPages
                    ? GetPageUri(enpoint, pagedReqest.PageNumber - 1, pagedReqest.PageSize)
                    : null,
                FirstPage = GetPageUri(enpoint, 1, pagedReqest.PageSize),
                LastPage = GetPageUri(enpoint, roundedTotalPages, pagedReqest.PageSize),
            };
        }

        private static Uri GetPageUri(string enpoint, int pageNumber, int pageSize)
        {
            var enpointUri = new Uri(enpoint);
            var modifiedUri = QueryHelpers.AddQueryString(enpointUri.ToString(), "pageNumber", pageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
