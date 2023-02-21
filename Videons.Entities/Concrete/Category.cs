using Videons.Core.Entities;

namespace Videons.Entities.Concrete;

public class Category : EntityBase
{
    public string Name { get; set; }
    public virtual ICollection<Video> Videos { get; set; }
}