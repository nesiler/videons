using Videons.Core.Entities.Concrete;

namespace Videons.Core.Entities;

public class DtoBase : IDto
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}