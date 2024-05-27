using PetCareSystem.Data.Entites;

namespace PetHealthSys.PetCareSystem.WebApp.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public int? validateToken(string token);
    }
}
