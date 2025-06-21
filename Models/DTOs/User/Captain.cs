namespace Tournament.Management.API.Models.DTOs.User
{
    public record Captain(
        Guid Id,
        string FullName,
        string ProfilePicture
    );
}
