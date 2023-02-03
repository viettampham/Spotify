﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpDelete("delete-song")]
    public IActionResult DeleteSong(Guid id)
    {
        var targetSong = _songService.DeleteSong(id);
        return Ok(targetSong);
    }
}