using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Record : BaseModel
    {
        public Boolean saveBarn { get; set; }
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public string? DetailPrediction { get; set; }
        public string? Conclude { get; set; }
        public int? PetId { get; set; }
        public Pet? Pet { get; set; }
        public int? BarnId { get; set; }
        public Barn? Barn { get; set; }
        public ICollection<RecordDetail>? RecordDetails { get; set; }
    }
}
