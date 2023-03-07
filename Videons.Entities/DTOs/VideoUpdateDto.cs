using System.ComponentModel.DataAnnotations.Schema;
using Videons.Core.Entities.Concrete;
using Videons.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class VideoUpdateDto : IDto
{
    public Guid CategoryId { get; set; }
    
    public virtual Category Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string StreamId { get; set; } // cloud stream id
    public VideoVisibility Visibility { get; set; }
}