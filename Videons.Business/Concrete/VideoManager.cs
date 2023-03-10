using Videons.Business.Abstract;
using Videons.Core.Utilities.Results;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Concrete;

public class VideoManager : IVideoService
{
    private readonly IChannelService _channelService;
    private readonly IVideoDal _videoDal;

    public VideoManager(IVideoDal videoDal, IChannelService channelService)
    {
        _videoDal = videoDal;
        _channelService = channelService;
    }

    public IDataResult<IList<Video>> GetList()
    {
        var videos = _videoDal.GetList();
        return new SuccessDataResult<IList<Video>>(videos);
    }

    public IDataResult<IList<Video>> GetListByChannelId(Guid channelId)
    {
        var videos = _videoDal.GetList(v => v.ChannelId == channelId);
        return new SuccessDataResult<IList<Video>>(videos);
    }

    public IDataResult<IList<Video>> GetListByCategoryId(Guid categoryId)
    {
        var videos = _videoDal.GetList(v => v.CategoryId == categoryId);
        return new SuccessDataResult<IList<Video>>(videos);
    }

    public Video GetById(Guid videoId)
    {
        return _videoDal.Get(v => v.Id == videoId);
    }

    public IResult Add(VideoDto videoDto)
    {
        var channelExist = _channelService.GetById(videoDto.ChannelId);
        if (channelExist == null) return new ErrorResult("Invalid channel");

        var video = new Video
        {
            Title = videoDto.Title,
            Description = videoDto.Description,
            ChannelId = videoDto.ChannelId,
            CategoryId = videoDto.CategoryId,
            StreamId = videoDto.StreamId,
            PublishDate = DateTime.Now.ToUniversalTime()
        };

        if (!_videoDal.Add(video)) return new ErrorResult("Video cannot added!");

        return new SuccessResult("Video added.");
    }

    public IResult Update(Guid id, VideoUpdateDto videoUpdateDto)
    {
        var video = GetById(id);

        if (video == null) return new ErrorResult("Video cannot found!");

        video.Title = videoUpdateDto.Title;
        video.Description = videoUpdateDto.Description;
        video.StreamId = videoUpdateDto.StreamId;
        video.Visibility = videoUpdateDto.Visibility;

        return _videoDal.Update(video)
            ? new SuccessResult("Video updated.")
            : new ErrorResult("Video cannot updated!");
    }

    public Video Watch(Guid videoId, Guid channelId)
    {
        var video = GetById(videoId);
        if (video == null) return null;

        var channel = _channelService.GetById(channelId);
        if (channel == null) return null;

        if (video.Visibility == VideoVisibility.Private && video.ChannelId != channel.Id) return null;
        _videoDal.Watch(videoId);

        var history = new History
        {
            ChannelId = channel.Id,
            VideoId = video.Id,
            Time = (short)new Random().Next(0, 100)
        };

        _channelService.Watch(channel.Id, history);

        return video;
    }

    public IResult Delete(Guid id)
    {
        var video = GetById(id);
        if (video == null) return new ErrorResult("Video cannot found!");

        return _videoDal.Delete(video)
            ? new SuccessResult("Video deleted.")
            : new ErrorResult("Video cannot deleted!");
    }

    public IResult AdminRemovVideo(Guid videoId)
    {
        var video = GetById(videoId);
        if (video == null) return new ErrorResult("Video cannot found!");

        return _videoDal.Delete(video)
            ? new SuccessResult("Video deleted.")
            : new ErrorResult("Video cannot deleted!");
    }
}