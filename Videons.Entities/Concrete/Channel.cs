using System.ComponentModel.DataAnnotations.Schema;
using Videons.Core.Entities;
using Videons.Core.Entities.Concrete;

namespace Videons.Entities.Concrete;

public class Channel : EntityBase
{
    [ForeignKey("User")] public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public string Name { get; set; }
    public string Slug { get; set; }
    public string ImagePath { get; set; }
    public bool Verified { get; set; }
    public ICollection<Video> Videos { get; set; }
}