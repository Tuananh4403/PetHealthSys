namespace PetCareSystem.Services.Models
{
    public class ApiResponse<T>
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public ApiResponse() {}

        public ApiResponse(T? data, string message = "")
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Constructor for error response
        public ApiResponse(string message, bool isError)
        {
            Success = isError ? false : true;
            Message = message;
            Data = default(T);
        }
    }
}
