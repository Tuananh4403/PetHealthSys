using PetCareSystem.Data.Entites;

namespace PetCareSystem.Data.Repositories.Roles
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetRoleByTitleAsync(string RoleTilte);
        Task<Role> GetRoleByIdAsync(int RoleId);
    }
}