using System.ComponentModel.DataAnnotations;

namespace Metro60.Core.Models;

public class AuthenticateModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
