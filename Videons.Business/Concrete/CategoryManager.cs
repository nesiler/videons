using AutoMapper;
using Videons.Business.Abstract;
using Videons.Core.Utilities.Results;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;

    public CategoryManager(IMapper mapper, ICategoryDal categoryDal)
    {
        _mapper = mapper;
        _categoryDal = categoryDal;
    }

    public IDataResult<IList<CategoryDto>> GetList()
    {
        var categoryList = _categoryDal.GetList();
        var categories = _mapper.Map<IList<CategoryDto>>(categoryList);
        return new SuccessDataResult<IList<CategoryDto>>(categories);
    }

    public IDataResult<Category> GetById(Guid id)
    {
        var category = _categoryDal.Get(c => c.Id == id);

        return category == null
            ? new ErrorDataResult<Category>(null, "Category cannot found!")
            : new SuccessDataResult<Category>(category);
    }

    public IDataResult<Category> GetByName(string name)
    {
        var category = _categoryDal.Get(c => c.Name == name);

        return category == null
            ? new ErrorDataResult<Category>(null, "Category cannot found!")
            : new SuccessDataResult<Category>(category);
    }

    public IResult Add(CategoryCreateUpdateDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);

        return _categoryDal.Add(category)
            ? new SuccessResult("Category created.")
            : new ErrorResult("Category cannot created!");
    }

    public IResult Update(Guid id, CategoryCreateUpdateDto categoryDto)
    {
        var categoryResult = GetById(id);
        if (!categoryResult.Success) return new ErrorResult(categoryResult.Message);

        var category = categoryResult.Data;
        category.Name = categoryDto.Name;

        return _categoryDal.Update(category)
            ? new SuccessResult("Category updated.")
            : new ErrorResult("Category cannot updated!");
    }

    public IResult Delete(Guid id)
    {
        var categoryResult = GetById(id);
        if (!categoryResult.Success) return new ErrorResult(categoryResult.Message);

        var category = categoryResult.Data;
        return _categoryDal.Delete(category)
            ? new SuccessResult("Category deleted.")
            : new ErrorResult("Category cannot deleted!");
    }
}