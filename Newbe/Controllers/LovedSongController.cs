using Microsoft.AspNetCore.Mvc;
using Newbe.Models.RequestModels;
using Newbe.Services;

namespace Newbe.Controllers;
[ApiController]
[Route("[controller]")]
public class LovedSongController:ControllerBase
{
    private readonly ILovedSongService _lovedSongService;

    public LovedSongController(ILovedSongService lovedSongService)
    {
        _lovedSongService = lovedSongService;
    }

    [HttpGet("get-list-loved-song")]
    public IActionResult GetListLovedSong()
    {
        var listLovedSong = _lovedSongService.GetListSongLoved();
        return Ok(listLovedSong);
    }
    
    [HttpGet("get-list-loved-song-by-user")]
    public IActionResult GetListLovedSongByUser(Guid id)
    {
        var listLovedSongByUser = _lovedSongService.GetLovedSongByUser(id);
        return Ok(listLovedSongByUser);
    }

    [HttpPost("create-loved-song")]
    public IActionResult CreateLovedSong(CreateLovedSongRequest request)
    {
        var newLovedSong = _lovedSongService.CreateLovedSong(request);
        return Ok(newLovedSong);
    }

    [HttpDelete("delete-loved-song")]
    public IActionResult DeleteLovedSong(Guid id)
    {
        var targetLovedSong = _lovedSongService.DeleteLovedSong(id);
        return Ok(targetLovedSong);
    }
    
}