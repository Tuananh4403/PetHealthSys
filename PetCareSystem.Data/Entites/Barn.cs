﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Entites
{
    public class Barn : BaseModel
    {
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set;}
        public Boolean? Status { get; set; }
        public ICollection<Record>? Records{ get; set;}
        public string? Result { get; set; }
    }
}
