using System.ComponentModel.DataAnnotations.Schema;
using Videons.Core.Entities;

namespace Videons.Entities.Concrete;

public class PlaylistVideo : EntityBase
{
    [ForeignKey("Playlist")] public Guid PlaylistId { get; set; }

    public virtual Playlist Playlist { get; set; }

    [ForeignKey("Video")] public Guid VideoId { get; set; }

    public virtual Video Video { get; set; }
}