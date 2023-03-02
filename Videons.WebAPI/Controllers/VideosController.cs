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
}