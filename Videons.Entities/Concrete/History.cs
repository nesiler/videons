using Videons.Core.Entities;

namespace Videons.Entities.Concrete;

public class History : EntityBase
{
    public Guid ChannelId { get; set; }
    public virtual Channel Channel { get; set; }
    public Guid VideoId { get; set; }
    public virtual Video Video { get; set; }
    public short Time { get; set; } // playback position in seconds
}