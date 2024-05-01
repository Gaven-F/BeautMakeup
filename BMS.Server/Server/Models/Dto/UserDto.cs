using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class UserDto
{
    [Required]
    public string Uid { get; set; } = string.Empty;

    [Required]
    public string Pwd { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public IEnumerable<string> Roles { get; set; } = [];
}

public class UserLoginDto
{
    [Required]
    public string Uid { get; set; } = string.Empty;

    [Required]
    public string Pwd { get; set; } = string.Empty;
}
