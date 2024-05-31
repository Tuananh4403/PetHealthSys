using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class RecordDetail : BaseModel
    {
        public int RecordId { get; set; }
        public Record? Record { get; set; }
        public int ServiceId { get; set; }
        public Service? Service { get; set;}
        public int Quantity { get; set; }
    }
}
