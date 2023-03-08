using Videons.Core.Entities;

namespace Videons.Entities.Concrete;

public class Comment : EntityBase
{
    public Guid VideoId { get; set; }
    public virtual Video Video { get; set; }
    public Guid ChannelId { get; set; }
    public virtual Channel Channel { get; set; }

    public string Text { get; set; }
}