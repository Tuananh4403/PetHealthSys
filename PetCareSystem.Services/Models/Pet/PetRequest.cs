using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Models.Pet
{
    public class PetRequest
    {
        [Required]
        public string? PetName { get; set; }
        [Required]
        public string? KindOfPet { get; set; }
        [Required]
        public Boolean Gender { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string? Species { get; set; }
    }
}
