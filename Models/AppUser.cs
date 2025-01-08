using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{

    [Required]
    public string FullName { get; set; } = string.Empty;
}