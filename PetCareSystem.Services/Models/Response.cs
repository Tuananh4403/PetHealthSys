using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Models
{
    public class ApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public JObject Data { get; set; }

        public ApiResponse() { }

        public ApiResponse(JObject data)
        {
            Status = "Success";
            Message = string.Empty;
            Data = data;
        }

        public ApiResponse(string jsonData)
        {
            Status = "Success";
            Message = string.Empty;
            Data = JObject.Parse(jsonData);
        }

        public ApiResponse(string message, bool isError)
        {
            Status = isError ? "Error" : "Success";
            Message = message;
            Data = null;
        }

        public void SetData(string jsonData)
        {
            Data = JObject.Parse(jsonData);
        }

        public string GetDataAsString()
        {
            return Data?.ToString();
        }
    }
}
