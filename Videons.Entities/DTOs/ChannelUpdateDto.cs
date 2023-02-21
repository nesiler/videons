using Videons.Core.Entities.Concrete;

namespace VideoApp.Entities.DTOs
{
    public class ChannelUpdateDto : IDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ImagePath { get; set; }
    }
}
