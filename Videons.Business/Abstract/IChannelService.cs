using Videons.Core.Utilities.Results;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Abstract;

public interface IChannelService
{
    IDataResult<IList<Channel>> GetList();
    Channel GetById(Guid channelId);
    IResult Add(ChannelDto channelDto);
    IResult Update(Guid id, ChannelUpdateDto channelUpdateDto);
}