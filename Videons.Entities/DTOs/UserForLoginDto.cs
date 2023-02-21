using Videons.Core.Entities.Concrete;

namespace VideoApp.Entities.DTOs
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}