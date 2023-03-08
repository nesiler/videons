using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Videons.Business.Abstract;
using Videons.Entities.DTOs;

namespace Videons.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideosController : ControllerBase
{
    private readonly IChannelService _channelService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IVideoService _videoService;

    public VideosController(IVideoService videoService, IMapper mapper, IUserService userService,
        IChannelService channelService)
    {
        _videoService = videoService;
        _mapper = mapper;
        _userService = userService;
        _channelService = channelService;
    }

    [HttpGet]
    public IActionResult GetList()
    {
        var result = _videoService.GetList();

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }
    
    [HttpGet("channel/{id}")]
    public IActionResult GetListByChannelId(Guid id)
    {
        var result = _videoService.GetListByChannelId(id);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }
    
    [HttpGet("category/{id}")]
    public IActionResult GetListByCategoryId(Guid id)
    {
        var result = _videoService.GetListByCategoryId(id);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var video = _videoService.GetById(id);

        return video != null
            ? Ok(video)
            : NotFound();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Add([FromBody] VideoDto videoDto)
    {
        var result = _videoService.Add(videoDto);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }
    
    [HttpPost("channel/{id}/video/{videoId}")]
    [Authorize]
    public IActionResult ChannelAddVideo(Guid id, Guid videoId)
    {
        var result = _channelService.ChannelAddVideo(id, videoId);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }
    
    [HttpDelete("channel/{id}/video/{videoId}")]
    [Authorize]
    public IActionResult ChannelDeleteVideo(Guid id, Guid videoId)
    {
        var result = _channelService.ChannelRemoveVideo(id, videoId);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(Guid id, VideoUpdateDto videoUpdateDto)
    {
        var result = _videoService.Update(id, videoUpdateDto);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpGet("watch/{id}")]
    [Authorize]
    public IActionResult Watch(Guid id)
    {
        var currentUser = HttpContext.User;
        var email = currentUser.FindFirst(ClaimTypes.Email)?.Value;
        var user = _userService.GetByEmail(email);
        var channel = _channelService.GetByUserId(user.Id);
        var video = _videoService.Watch(id, channel.Id);
        var videoDto = _mapper.Map<VideoDto>(video);

        return videoDto != null
            ? Ok(video)
            : NotFound();
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(Guid id)
    {
        var result = _videoService.Delete(id);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }
    
}