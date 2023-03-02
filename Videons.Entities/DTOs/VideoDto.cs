using Videons.Core.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class VideoDto : IDto
{
    public Guid ChannelId { get; set; }

    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string StreamId { get; set; }
}