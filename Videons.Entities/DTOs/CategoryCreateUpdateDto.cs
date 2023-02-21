using Videons.Core.Entities.Concrete;

namespace Videons.Entities.DTOs;

public class CategoryCreateUpdateDto : IDto
{
    public string Name { get; set; }
}