using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Altairis.Application.Helpers
{
    public class ApiResponseBase
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }

    public class SingleApiResponse<T> : ApiResponseBase
    {
        public T? Data { get; set; } = default(T?);
    }

    public class ListApiResponse<T> : ApiResponseBase
    {
        public List<T>? Items { get; set; } = [];
    }

    public class PagedListApiResponse<T> : ListApiResponse<T>
    {
        public int TotalCount { get; set; } = 0;
        public int CurrentCount => Items?.Count ?? 0;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages =>
            (TotalCount > PageSize)
                ? (int)Math.Ceiling((double)(TotalCount / PageSize))
                : 1;
        public int[] PagesArray =>
            (TotalCount > PageSize)
                ? Enumerable.Range(1, TotalPages).ToArray()
                : [1];
    }
}
