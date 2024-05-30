using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class BookingService
    {
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        public int BookingId { get; set;}
        public Booking? Booking { get; set; }
    }
}
