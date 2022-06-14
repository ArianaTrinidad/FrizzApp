namespace FirzzApp.Business.Dtos.RequestDto
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AdminKey { get; set; }
    }
}
