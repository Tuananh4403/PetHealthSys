using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Booking : BaseModel
    {
        public Booking() {
            BookingServices = new List<BookingService>();
        }

        public int? Number { get; set; }
        public decimal Total { get; set; }
        public DateTime? BookingTime { get; set; }
        public int? CustomerId { get; set; }
        public string? Status { get; set; }
        public virtual Customer Customer { get; set; }
        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }
        public ICollection<BookingService> BookingServices { get; set; }
        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
