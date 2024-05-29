using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class ManageRecord
    {
        public int ManageId { get; set; }
        public DateTime DateRecord { get; set; }
        public string DetailPrediction { get; set; }
        public string Conclude { get; set; }
        public Boolean saveBarn { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int RecordId { get; set; }
        public Record Record { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
