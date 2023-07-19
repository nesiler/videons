using Videons.Core.Entities.Concrete;
using Videons.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class PlaylistDto : IDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid ChannelId { get; set; }

    public PlaylistVisibility Visibility { get; set; }
}