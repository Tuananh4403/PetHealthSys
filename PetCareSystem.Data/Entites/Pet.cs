using PetCareSystem.Data.Configurations;
using System.Text.Json.Serialization;

namespace PetCareSystem.Data.Entites
{
    public class Pet
    {
        public int PetId { get; set; }
        public string? PetName { get; set; }
        public string? KindOfPet { get; set; }
        public Boolean Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string? Species { get; set; }
        public List<Booking>? Bookings { get; set; }
        public List<Record>? Records { get; set; }
    }
}
