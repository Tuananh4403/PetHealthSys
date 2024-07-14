using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using PetCareSystem.Data.Enums;

namespace PetCareSystem.Data.Entites
{
    public class Record : BaseModel
    {
        public bool SaveBarn { get; set; }
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public float? PetWeigth { get; set; }
        public float? PetHeight { get; set; }
        public RecordStautus? Status{ get; set; }
        public string? DetailPrediction { get; set; }
        public string? Conclude { get; set; }
        public int? PetId { get; set; }
        public Pet? Pet { get; set; }
        public int? BarnId { get; set; }
        public Barn? Barn { get; set; }
        public decimal Total { get; set; }
        public ICollection<RecordDetail>? RecordDetails { get; set; }
    }
}
