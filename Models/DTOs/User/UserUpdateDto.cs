using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.DTOs.User;

public record UserUpdateDto(
    [StringLength(50)] 
    string? Name,
    
    [StringLength(50)]
    string? Surname,
    
    [EmailAddress]
    [StringLength(100)]
    string? Email
);