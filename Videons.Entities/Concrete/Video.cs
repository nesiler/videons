using Videons.Core.Entities;

namespace Videons.Entities.Concrete;

public enum VideoVisibility
{
    Public,
    Private,
    Unlisted
}

public class Video : EntityBase
{
    public Guid ChannelId { get; set; }
    public virtual Channel Channel { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string StreamId { get; set; } // cloud stream id
    public VideoVisibility Visibility { get; set; } = VideoVisibility.Unlisted;
    public DateTime PublishDate { get; set; }
    public uint Views { get; set; }
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public ICollection<PlaylistVideo> PlaylistVideos { get; set; } = new HashSet<PlaylistVideo>();
}