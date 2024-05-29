using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int Number { get; set; }
        public decimal Total { get; set; }
        public DateTime BookingTime { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public List<Customer> Customers { get; set; }

    }
}
