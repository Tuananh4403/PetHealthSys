
namespace PetCareSystem.Data.Entites{
    public class Role : BaseModel
    {

        public string? Title { get; set; }
        public string? Name { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }

    }
}