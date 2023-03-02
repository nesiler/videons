using Videons.Core.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class ChannelUpdateDto : IDto
{
    public string Name { get; set; }
    public string ImagePath { get; set; }
}