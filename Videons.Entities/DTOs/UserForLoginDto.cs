using Videons.Core.Entities;

namespace Videons.Entities.DTOs;

public class UserForLoginDto : DtoBase
{
    public string Email { get; set; }
    public string Password { get; set; }
}