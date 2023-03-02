using Videons.Business.Abstract;
using Videons.Core.Utilities.Results;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Concrete;

public class VideoManager : IVideoService
{
    private readonly IVideoDal _videoDal;
    private readonly  IChannelService _channelService;
    private readonly  IAuthService _authService;
    private readonly IUserService _userService;

    public VideoManager(IVideoDal videoDal , IChannelService channelService)
    {
        _channelService = channelService;
        _videoDal = videoDal;
    }

    public IDataResult<IList<Video>> GetList()
    {
        var videos = _videoDal.GetList();
        return new SuccessDataResult<IList<Video>>(videos);
    }

    public Video GetById(Guid videoId)
    {
        return _videoDal.Get(v => v.Id == videoId);
    }

    public IResult Add(VideoDto videoDto)
    {
        var channelExist = _channelService.GetById(videoDto.ChannelId);
        if(channelExist == null) return new ErrorResult("Invalid channel");
        
        var video = new Video
        {
            Title = videoDto.Title,
            Description = videoDto.Description,
            ChannelId = videoDto.ChannelId,
            StreamId = videoDto.StreamId,
        }; 
        
        if (!_videoDal.Add(video)) return new ErrorResult("Video cannot added!");

        return new SuccessResult("Video added.");
        
    }

    public IResult Update(Guid id, VideoUpdateDto videoUpdateDto)
    {
        var video = GetById(id);
        
        if(video == null) return new ErrorResult("Video cannot found!");
        
        video.Title = videoUpdateDto.Title;
        video.Description = videoUpdateDto.Description;
        video.StreamId = videoUpdateDto.StreamId;
        video.Visibility = videoUpdateDto.Visibility;
        
        return _videoDal.Update(video)
            ? new SuccessResult("Video updated.")
            : new ErrorResult("Video cannot updated!");
    }

    public IResult Watch(Guid videoId)
    {
        var video = GetById(videoId);
        
        
    }
}