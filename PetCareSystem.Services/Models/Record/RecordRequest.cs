using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Models.Record
{
    public class RecordRequest
    {
        public int DoctorId { get; set; }

        public int PetId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; }
        public int BarnId { get; set; }
        public string DetailPrediction { get; set; }
        public string Conclude { get; set; }
        public bool SaveBarn { get; set; }
    }
}
