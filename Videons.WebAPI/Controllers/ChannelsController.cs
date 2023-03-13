using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Videons.Business.Abstract;
using Videons.Entities.DTOs;

namespace Videons.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChannelsController : ControllerBase
{
    private readonly IChannelService _channelService;

    public ChannelsController(IChannelService channelService)
    {
        _channelService = channelService;
    }

    [HttpGet]
    public IActionResult GetList()
    {
        var result = _channelService.GetList();

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }

    [HttpGet("id/{id}")]
    public IActionResult GetById(Guid id)
    {
        var channel = _channelService.GetById(id);

        return channel != null
            ? Ok(channel)
            : NotFound();
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetByUserId(Guid userId)
    {
        var channel = _channelService.GetByUserId(userId);

        return channel != null
            ? Ok(channel)
            : NotFound();
    }

    [HttpGet("email/{email}")]
    public IActionResult GetByUserEmail([FromBody] string email)
    {
        var channel = _channelService.GetByUserEmail(email);

        return channel != null
            ? Ok(channel)
            : NotFound();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Add([FromBody] ChannelDto channelDto)
    {
        if (channelDto.Name == string.Empty) return BadRequest("Channel name cannot be null");

        var result = _channelService.Add(channelDto);
        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpPut("update/{id}")]
    [Authorize]
    public IActionResult Update(Guid id, ChannelUpdateDto channelUpdateDto)
    {
        if (channelUpdateDto.Name == string.Empty) return BadRequest("Channel name cannot be null");

        var result = _channelService.Update(id, channelUpdateDto);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }


    [HttpDelete("delete-channel/{id}")]
    [Authorize]
    public IActionResult Delete(Guid id)
    {
        var result = _channelService.Delete(id);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpDelete("admin-remove-channel/{id}")]
    public IActionResult AdminRemoveChannel(Guid id)
    {
        var result = _channelService.AdminRemoveChannel(id);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }
}