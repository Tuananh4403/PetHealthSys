using PetCareSystem.Data.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    
    public class Customer : BaseModel
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Pet>? Pets { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
