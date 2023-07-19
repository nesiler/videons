namespace Videons.Entities.Concrete;

public class PlaylistVideo
{
    public Guid PlaylistId { get; set; }
    public virtual Playlist Playlist { get; set; }
    public Guid VideoId { get; set; }
    public virtual Video Video { get; set; }
}