using Videons.Core.Entities.Concrete;

namespace VideoApp.Entities.DTOs
{
    public class ChannelDto : IDto
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}