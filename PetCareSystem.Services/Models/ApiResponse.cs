namespace PetCareSystem.Services.Models
{
    public class ApiResponse<T>
    {
        // Status of the response (e.g., "Success" or "Error")
        
        public string? Status { get; set; }
        
        // A message providing additional information about the response
        public string? Message { get; set; }
        
        // The data returned in the response
        public T? Data { get; set; }

        // Default constructor
        public ApiResponse() {}

        // Constructor for success response with data
        public ApiResponse(T data)
        {
            Status = "Success";
            Message = string.Empty;
            Data = data;
        }

        // Constructor for error response
        public ApiResponse(string message, bool isError)
        {
            Status = isError ? "Error" : "Success";
            Message = message;
            Data = default(T);
        }
    }
}
