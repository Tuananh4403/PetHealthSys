using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Repositories.Roles
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByTitleAsync(string RoleTilte);
        Task<Role> GetRoleByIdAsync(int RoleId);
        Task<List<Role>> GetAll();
        Task Create(Role role);
        void Delete(int id);
    }
}