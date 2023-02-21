using Videons.Core.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class ChangePasswordDto : IDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}