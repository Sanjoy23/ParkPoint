using System.ComponentModel.DataAnnotations;

public class RegisterDto{
    public string FullName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string Roles { get; set; } = "User";

}