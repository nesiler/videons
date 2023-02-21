using AutoMapper;
using Videons.Core.Entities.Concrete;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryCreateUpdateDto, Category>();

        CreateMap<User, CurrentUserDto>();
    }
}