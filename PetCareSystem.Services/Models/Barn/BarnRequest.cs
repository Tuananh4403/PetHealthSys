using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Services.Models.Barn
{
    public class BarnRequest
    {
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool? Status { get; set; }
        public ICollection<Record>? Records { get; set; }
        public string? Result { get; set; }
    }
}
