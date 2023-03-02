using Videons.Business.Abstract;
using Videons.Core.DataAccess.EntityFramework;
using Videons.Core.Utilities.Results;
using Videons.DataAccess.Abstract;
using Videons.DataAccess.Concrete.EntityFramework;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Concrete;

public class ChannelManager : IChannelService
{
    private readonly IChannelDal _channelDal;
    private readonly IUserService _userService;
    private readonly IHistoryDal _historyDal;

    public ChannelManager(IChannelDal channelDal, IUserService userService, IHistoryDal historyDal)
    {
        _channelDal = channelDal;
        _userService = userService;
        _historyDal = historyDal;
    }

    public Channel GetByUserEmail(string email)
    {
        return _channelDal.Get(c => c.User.Email == email);
    }

    public IResult Add(ChannelDto channelDto)
    {
        var userExist = _userService.GetById(channelDto.UserId);
        if (userExist == null) return new ErrorResult("Invalid user");

        var channel = new Channel
        {
            Name = channelDto.Name,
            Verified = false,
            UserId = channelDto.UserId
        };

        if (!_channelDal.Add(channel)) return new ErrorResult("Channel cannot created!");

        _channelDal.Update(channel);

        return new SuccessResult("Channel created.");
    }

    public IResult Update(Guid id, ChannelUpdateDto channelUpdateDto)
    {
        var channel = GetById(id);

        if (channel == null) return new ErrorResult("Channel cannot found!");

        channel.Name = channelUpdateDto.Name;
        channel.ImagePath = channelUpdateDto.ImagePath;

        return _channelDal.Update(channel)
            ? new SuccessResult("Channel updated.")
            : new ErrorResult("Channel cannot updated!");
    }

    public IResult ChannelAction(Guid id, History history)
    {
        var channel = GetById(id);

        if (channel == null) return new ErrorResult("Channel cannot found!");
        _historyDal.Add(history);
        
        return _channelDal.Update(channel)
            ? new SuccessResult("Channel updated.")
            : new ErrorResult("Channel cannot updated!");
    }

    public IResult ChannelAddVideo(Guid id, Video video)
    {
        var channel = GetById(id);

        if (channel == null) return new ErrorResult("Channel cannot found!");

        channel.Videos.Add(video);

        return _channelDal.Update(channel)
            ? new SuccessResult("Channel updated.")
            : new ErrorResult("Channel cannot updated!");
    }

    public IDataResult<IList<Channel>> GetList()
    {
        var channels = _channelDal.GetList();
        return new SuccessDataResult<IList<Channel>>(channels);
    }

    public Channel GetById(Guid channelId)
    {
        return _channelDal.Get(c => c.Id == channelId);
    }

    public Channel GetByUserId(Guid userId)
    {
        return _channelDal.Get(c => c.UserId == userId);
    }
}