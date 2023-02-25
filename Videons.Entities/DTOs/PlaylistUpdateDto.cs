using Videons.Core.Entities.Concrete;
using Videons.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class PlaylistUpdateDto : IDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public PlaylistVisibility Visibility { get; set; }
    public PlaylistVideo PlaylistVideos { get; set; }
}