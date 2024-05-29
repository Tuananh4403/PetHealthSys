using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class ManageService
    {
        public int ManageId { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
