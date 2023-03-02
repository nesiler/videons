using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Videons.Business.Abstract;
using Videons.Entities.DTOs;

namespace Videons.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _playlistService;

    public PlaylistsController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    [HttpGet]
    public IActionResult GetList()
    {
        var result = _playlistService.GetList();

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }

    [HttpGet("channel/{id}")]
    public IActionResult GetListByChannelId(Guid id)
    {
        var result = _playlistService.GetListByChannelId(id);

        return result.Success
            ? Ok(result.Data)
            : BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var playlist = _playlistService.GetById(id);

        return playlist != null
            ? Ok(playlist)
            : NotFound();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Add([FromBody] PlaylistDto playlistDto)
    {
        var result = _playlistService.Add(playlistDto);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(Guid id, PlaylistUpdateDto playlistUpdateDto)
    {
        var result = _playlistService.Update(id, playlistUpdateDto);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }
}