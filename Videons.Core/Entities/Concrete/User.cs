namespace Videons.Core.Entities;

public class User : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    
    public DateTime LastLogin { get; set; } = DateTime.Today.ToUniversalTime();
    public bool Status { get; set; } = true;
}