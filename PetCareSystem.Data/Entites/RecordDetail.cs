using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class RecordDetail : BaseModel
    {    
        public string? Medicine { get; set; }
        public string? Vaccine { get; set; }
        public int RecordId { get; set; }
        public Record? Record { get; set; }
        public int ServiceId { get; set; }
        
        public Service? Service { get; set;}
    }
}
