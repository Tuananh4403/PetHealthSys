using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Models.Barn
{
    public class BarnReq
    {
        public DateTime DateStart {  get; set; }

        public DateTime DateEnd { get; set; }

        public bool Status { get; set; }

        public string Result { get; set; }
    }
}
