using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace PetCareSystem.Data.Entites
{
    public class Doctor : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string? Specialty { get; set; }
        public bool  Status { get; set; }
        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

        public Doctor()
        {
            Bookings = new List<Booking>();
            Records  = new List<Record>();
        }
    }
}
