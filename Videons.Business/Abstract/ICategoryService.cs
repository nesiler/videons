using Videons.Core.Utilities.Results;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Abstract;
public interface ICategoryService
{
    IDataResult<IList<CategoryDto>> GetList();
    IDataResult<Category> GetById(Guid id);
    IResult Add(CategoryCreateUpdateDto categoryDto);
    IResult Update(Guid id, CategoryCreateUpdateDto categoryDto);
}