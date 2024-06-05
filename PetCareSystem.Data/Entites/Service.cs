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
        public string? Status { get; set; }
        public string? Note { get; set; }
        public virtual ICollection<BookingService>? BookingServicess { get; set; }
        public virtual ICollection<RecordDetail>?  RecordDetails { get; set; }
    }
}
