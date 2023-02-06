using Microsoft.AspNetCore.Mvc;
using Newbe.Models.RequestModels;
using Newbe.Services;

namespace Newbe.Controllers;
[ApiController]
[Route("[controller]")]
public class SongController:ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet("get-list-song")]
    public IActionResult GetSong()
    {
        var listSong = _songService.GetListSong();
        return Ok(listSong);
    }

    [HttpGet("get-song-by-category")]
    public IActionResult GetSongByCategory(Guid id)
    {
        var listSong = _songService.GetListSongByCategory(id);
        return Ok(listSong);
    }
    
    [HttpGet("get-list-song-deleted")]
    public IActionResult GetSongDeleted()
    {
        var listSong = _songService.GetListSongDeleted();
        return Ok(listSong);
    }

    [HttpPost("create-song")]
    public IActionResult CreateSong(CreateSongRequest request)
    {
        var newSong = _songService.CreateSong(request);
        return Ok(newSong);
    }

    [HttpPost("edit-song")]
    public IActionResult EditSong(EditSongRequest request)
    {
        var targetSong = _songService.EditSong(request);
        return Ok(targetSong);
    }

    [HttpPost("delete-song")]
    public IActionResult DeleteSong(Guid id)
    {
        var targetSong = _songService.DeleteSong(id);
        return Ok(targetSong);
    }

    [HttpPost("restore-song")]
    public IActionResult RestoreSong(Guid id)
    {
        var targetSong = _songService.RestoreSong(id);
        return Ok(targetSong);
    }
}