using Videons.Core.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class ChannelDto : IDto
{
    public string Name { get; set; }
    public Guid UserId { get; set; }
}