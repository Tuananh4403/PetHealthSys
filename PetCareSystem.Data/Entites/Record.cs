using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Record
    {
        public int RecordId { get; set; }
        public string Medicine { get; set; }
        public string Vaccine { get; set; }
        public List<ManageRecord> ManageRecords { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
