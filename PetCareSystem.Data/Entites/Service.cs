using PetCareSystem.Data.Configurations;
using PetCareSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Service : BaseModel
    {
        public int TypeId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }

        public string? Note { get; set; }
        public ServiceUnit Unit {get; set; }
        public virtual ICollection<BookingService>? BookingServicess { get; set; }
        public virtual ICollection<RecordDetail>?  RecordDetails { get; set; }
    }
}
