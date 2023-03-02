using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Videons.Business.Abstract;
using Videons.Entities.DTOs;

namespace Videons.WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VideosController : ControllerBase
{
    private readonly IVideoService _videoService;

    public VideosController(IVideoService videoService)
    {
        _videoService = videoService;
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
    
    [HttpGet("{id}")]
    public IActionResult Watch(Guid id)
    {
        var video = _videoService.Watch(id);

        return video != null
            ? Ok(video)
            : NotFound();
    }
}