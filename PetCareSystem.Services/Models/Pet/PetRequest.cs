﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Models.Pet
{
    public class PetRequest
    {
        public string? PetName { get; set; }
        public string? KindOfPet { get; set; }
        public Boolean Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string? Species { get; set; }
    }
}
