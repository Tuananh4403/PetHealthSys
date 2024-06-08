using PetCareSystem.Data.Configurations;
using System.Text.Json.Serialization;

namespace PetCareSystem.Data.Entites
{
    public class Pet : BaseModel
    {
        public string? PetName { get; set; }
        public string? KindOfPet { get; set; }
        public Boolean Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string? Species { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Record>? Records { get; set; }
    }
}
