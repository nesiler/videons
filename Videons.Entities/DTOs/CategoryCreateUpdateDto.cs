using Videons.Core.Entities.Concrete;

namespace VideoApp.Entities.DTOs
{
    public class CategoryCreateUpdateDto : IDto
    {
        public string Name { get; set; }
    }
}
