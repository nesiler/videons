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
        if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var result = _playlistService.Add(playlistDto);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(Guid id, PlaylistUpdateDto playlistUpdateDto)
    {
        if (playlistUpdateDto.Name == string.Empty) return BadRequest("Channel name cannot be null");

        var result = _playlistService.Update(id, playlistUpdateDto);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(Guid id)
    {
        var result = _playlistService.Delete(id);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpDelete("admin-remove-playlist/{id}")]
    public IActionResult AdminRemovePlaylist(Guid id)
    {
        var result = _playlistService.AdminRemovePlaylist(id);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }

    [HttpPost("{id}/videos/{videoId}")]
    [Authorize]
    public IActionResult AddVideo(Guid id, Guid videoId)
    {
        var result = _playlistService.AddVideo(id, videoId);

        return result.Success
            ? Ok(result.Message)
            : BadRequest(result.Message);
    }
}