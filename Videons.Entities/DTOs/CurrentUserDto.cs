using Videons.Core.Entities;

namespace Videons.Entities.DTOs;

public class CurrentUserDto : DtoBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime LastLogin { get; set; }
    public bool Status { get; set; }
}