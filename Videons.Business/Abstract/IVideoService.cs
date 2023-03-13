using Videons.Core.Utilities.Results;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Abstract;

public interface IVideoService
{
    IDataResult<IList<Video>> GetList();
    IDataResult<IList<Video>> GetListByChannelId(Guid channelId);
    IDataResult<IList<Video>> GetListByCategoryId(Guid categoryId);
    Video GetById(Guid videoId);
    IResult Add(VideoDto videoDto);
    IResult Update(Guid id, VideoUpdateDto videoUpdateDto);
    Video Watch(Guid videoId, Guid channelId);
    IResult Delete(Guid id);
    IResult AdminRemovVideo(Guid videoId);
}