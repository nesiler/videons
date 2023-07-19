using Videons.Core.Entities;

namespace Videons.Entities.Concrete;

public class Subscription : EntityBase
{
    public Guid SubscriberId { get; set; }
    public virtual Channel Subscriber { get; set; }
    public Guid ChannelId { get; set; }
    public virtual Channel Channel { get; set; }
    public bool Notification { get; set; } = false;
}