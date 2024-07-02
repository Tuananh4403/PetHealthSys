using System.ComponentModel.DataAnnotations;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.Services.Models.Auth
{
    public class UpdateUserRoleReq
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int[] Roles { get; set; }
    }
}
