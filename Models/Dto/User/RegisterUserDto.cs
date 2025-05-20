namespace Tournament.Management.API.Models.Dto.User
{
    public class RegisterUserDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
}