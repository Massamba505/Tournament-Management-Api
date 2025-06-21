namespace Tournament.Management.API.Models.DTOs.User
{
    public record Manager(
        Guid Id,
        string FullName,
        string ProfilePicture
    );
}
