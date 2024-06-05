namespace PetCareSystem.Services.Models.Auth
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
