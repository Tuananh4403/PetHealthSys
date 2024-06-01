using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.WebApp.Models.Booking
{
    public class CreateBookingReq
    {
        public int OwnerId { get; set; }
        public int PetId { get; set; }
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        public DateTime BookingDate { get; set;}
        public string Note { get; set;}
    }
}
