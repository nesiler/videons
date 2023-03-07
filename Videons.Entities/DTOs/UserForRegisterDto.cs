using Videons.Core.Entities;
using Videons.Core.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class UserForRegisterDto : DtoBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}