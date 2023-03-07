using Videons.Core.Entities;
using Videons.Core.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class UserForLoginDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}