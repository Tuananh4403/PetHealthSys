using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Models.Services
{
    public class CreateServiceReq
    {
            public int TypeOfService { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string? Status { get; set; }
            public string? Note { get; set; }
    }
}
