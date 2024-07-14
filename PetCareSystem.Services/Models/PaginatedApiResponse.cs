using PetCareSystem.Data.Entites;

namespace PetCareSystem.Services.Models
{
    public class PaginatedApiResponse<T>
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public IEnumerable<T>? Data { get; set; }
        public int? TotalCount { get; set; }
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }

        public PaginatedApiResponse() {}

        public PaginatedApiResponse(IEnumerable<T> data, int totalCount, int currentPage, int pageSize)
        {
            Success = true;
            Message = string.Empty;
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PaginatedApiResponse(string message, bool isError)
        {
            Success = isError ? false : true;
            Message = message;
            Data = Enumerable.Empty<T>();
            TotalCount = 0;
            CurrentPage = 0;
            PageSize = 0;
        }
    }
}
