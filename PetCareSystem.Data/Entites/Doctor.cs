using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Doctor : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Record>? Records { get; set; }
    }
}
