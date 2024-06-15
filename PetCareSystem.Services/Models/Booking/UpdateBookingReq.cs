using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Models.Booking
{
    public class UpdateBookingReq
    {
        public int CustomerId { get; set; }
        public int PetId { get; set; }
        public int DoctorId { get; set; }
        public int[]? ServiceIds { get; set; }
        public DateTime BookingDate { get; set; }
        public string Note { get; set; }
    }
}