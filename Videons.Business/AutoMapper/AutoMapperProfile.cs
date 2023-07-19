using AutoMapper;
using Videons.Core.Entities;
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
        CreateMap<User, UserForRegisterDto>();
        CreateMap<User, UserForLoginDto>();
        CreateMap<User, UserUpdateDto>();
        CreateMap<Channel, ChannelDto>();
        CreateMap<Channel, ChannelUpdateDto>();
        CreateMap<Video, VideoDto>();
        CreateMap<Video, VideoUpdateDto>();
        CreateMap<Playlist, PlaylistDto>();
        CreateMap<Playlist, PlaylistUpdateDto>();
    }
}