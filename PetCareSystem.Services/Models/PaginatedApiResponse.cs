namespace PetCareSystem.Services.Models
{
    public class PaginatedApiResponse<T>
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public IEnumerable<T>? Data { get; set; }
        public int? TotalCount { get; set; }

        public PaginatedApiResponse() {}

        public PaginatedApiResponse(IEnumerable<T> data, int totalCount)
        {
            Success = true;
            Message = string.Empty;
            Data = data;
            TotalCount = totalCount;
        }

        public PaginatedApiResponse(string message, bool isError)
        {
            Success = isError ? false : true;
            Message = message;
            Data = Enumerable.Empty<T>();
            TotalCount = 0;
        }
    }
}
