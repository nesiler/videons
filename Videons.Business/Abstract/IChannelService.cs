using Videons.Core.Utilities.Results;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Abstract;

public interface IChannelService
{
    IDataResult<IList<Channel>> GetList();
    Channel GetById(Guid channelId);
    Channel GetByUserId(Guid userId);
    Channel GetByUserEmail(string email);
    IResult Add(ChannelDto channelDto);
    IResult Update(Guid id, ChannelUpdateDto channelUpdateDto);
    IResult Watch(Guid id, History history);
    IResult ChannelAddVideo(Guid channelId, Guid videoId);
    IResult ChannelRemoveVideo(Guid channelId, Guid videoId);
    IResult Delete(Guid id);

    IResult AdminRemoveChannel(Guid channelId);
}