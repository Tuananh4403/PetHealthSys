using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Barn
    {
        public int BarnId { get; set; }
        public DateTime DateSaveBarn { get; set; }
        public string Status { get; set; }
        public string Medicine { get; set; }
        public string Vaccine { get; set; }
        public Boolean Result { get; set; }
    }
}
