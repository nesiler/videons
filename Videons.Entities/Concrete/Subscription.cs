using System.ComponentModel.DataAnnotations.Schema;
using Videons.Core.Entities;

namespace Videons.Entities.Concrete;

public class Subscription : EntityBase
{
    [ForeignKey("Subscriber")] public Guid SubscriberId { get; set; }

    public virtual Channel Subscriber { get; set; }

    [ForeignKey("Channel")] public Guid ChannelId { get; set; }

    public virtual Channel Channel { get; set; }

    public bool Notification { get; set; }
}