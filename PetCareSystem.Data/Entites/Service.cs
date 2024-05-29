using PetCareSystem.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string TypeOfService { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<ManageService> ManageSevices { get; set; }
    }
}
