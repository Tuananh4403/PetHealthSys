using PetCareSystem.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Service : BaseModel
    {
        public string? TypeOfService { get; set; }
        public string? ServiceName { get; set; }
        public decimal Price { get; set; }
        public ICollection<BookingService>? BookingServicess { get; set; }
    }
}
