using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class BookingService : BaseModel
    {
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        public int BookingId { get; set;}
        public virtual Booking? Booking { get; set; }
        public string? Note { get; set; }
        public decimal Price { get; set; }
    }
}
