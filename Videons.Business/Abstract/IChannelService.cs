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
    IResult ChannelAction(Guid id, History history);
    IResult ChannelAddVideo(Guid id, Video video);
    IResult ChannelRemoveVideo(Guid id, Video video);
    IResult Delete(Guid id);
}