using Videons.Business.Abstract;
using Videons.Core.Utilities.Results;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Concrete;

public class ChannelManager : IChannelService
{
    private readonly IChannelDal _channelDal;
    private readonly IUserService _userService;
    private readonly IVideoDal _videoDal;

    public ChannelManager(IChannelDal channelDal, IUserService userService, IVideoDal videoDal)
    {
        _channelDal = channelDal;
        _userService = userService;
        _videoDal = videoDal;
    }

    public Channel GetByUserEmail(string email)
    {
        return _channelDal.Get(c => c.User.Email == email);
    }

    public IResult Add(ChannelDto channelDto)
    {
        if (_userService.GetById(channelDto.UserId) == null) return new ErrorResult("Invalid user");

        var channel = new Channel
        {
            Name = channelDto.Name,
            Slug = channelDto.Name + "_" + channelDto.UserId,
            Description = $"My name is {channelDto.Name} and I'm a Videons user.",
            Verified = false,
            UserId = channelDto.UserId
        };

        if (!_channelDal.Add(channel)) return new ErrorResult("Channel cannot created!");

        return new SuccessResult("Channel created.");
    }

    public IResult Update(Guid id, ChannelUpdateDto channelUpdateDto)
    {
        var channel = GetById(id);

        if (channel == null) return new ErrorResult("Channel cannot found!");

        channel.Name = channelUpdateDto.Name;
        channel.Slug = channelUpdateDto.Name + "_" + channel.UserId;
        channel.Description = channelUpdateDto.Description;
        channel.ImagePath = channelUpdateDto.ImagePath;

        return _channelDal.Update(channel)
            ? new SuccessResult("Channel updated.")
            : new ErrorResult("Channel cannot updated!");
    }

    public IResult Watch(Guid id, History history)
    {
        var channel = GetById(id);
        if (channel == null) return new ErrorResult("Channel cannot found!");

        channel.Histories.Add(history);

        return _channelDal.Update(channel)
            ? new SuccessResult("Channel updated.")
            : new ErrorResult("Channel cannot updated!");
    }


    public IResult ChannelAddVideo(Guid channelId, Guid videoId)
    {
        var channel = GetById(channelId);
        if (channel == null) return new ErrorResult("Channel cannot found!");

        var video = _videoDal.Get(v => v.Id == videoId);
        channel.Videos.Add(video);

        return _channelDal.Update(channel)
            ? new SuccessResult("Channel updated.")
            : new ErrorResult("Channel cannot updated!");
    }

    public IResult ChannelRemoveVideo(Guid channelId, Guid videoId)
    {
        var channel = GetById(channelId);
        if (channel == null) return new ErrorResult("Channel cannot found!");

        var video = _videoDal.Get(v => v.Id == videoId);

        if (!channel.Videos.Remove(video)) return new ErrorResult("Video cannot removed!");

        return _channelDal.Update(channel)
            ? new SuccessResult("Channel updated.")
            : new ErrorResult("Channel cannot updated!");
    }


    public IResult Delete(Guid id)
    {
        var channel = GetById(id);
        if (channel == null) return new ErrorResult("Channel cannot found!");

        return _channelDal.Delete(channel)
            ? new SuccessResult("Channel deleted.")
            : new ErrorResult("Channel cannot deleted!");
    }

    public IResult AdminRemoveChannel(Guid channelId)
    {
        var channel = GetById(channelId);
        if (channel == null) return new ErrorResult("Channel cannot found!");

        return _channelDal.Delete(channel)
            ? new SuccessResult("Channel deleted by Admin.")
            : new ErrorResult("Channel cannot deleted!");
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