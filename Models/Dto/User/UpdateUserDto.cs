namespace Tournament.Management.API.Models.Dto.User
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}