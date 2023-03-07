using System.ComponentModel.DataAnnotations.Schema;
using Videons.Core.Entities;

namespace Videons.Entities.Concrete;

public enum PlaylistVisibility
{
    Public,
    Private,
    Unlisted
}

public class Playlist : EntityBase
{
    [ForeignKey("Channel")] public Guid ChannelId { get; set; }

    public virtual Channel Channel { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PlaylistVisibility Visibility { get; set; } = PlaylistVisibility.Unlisted;

    public ICollection<PlaylistVideo> PlaylistVideos { get; set; } = new HashSet<PlaylistVideo>();
}