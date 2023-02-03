using Microsoft.EntityFrameworkCore;
using Newbe.Models;
using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services.Impl;

public class LovedSongService:ILovedSongService
{
    private readonly MasterDbContext _context;

    public LovedSongService(MasterDbContext context)
    {
        _context = context;
    }
    public List<LovedSongResponse> GetListSongLoved()
    {
        var listLovedSong = _context.LovedSongs
            .Include(ls=>ls.Song)
            .Select(ls => new LovedSongResponse()
            {
                UserID = ls.UserID,
                Song = new Song()
                {
                    SongID = ls.Song.SongID,
                    Name = ls.Song.Name,
                    Author = ls.Song.Author,
                    Singers = ls.Song.Singers,
                    ImageURL = ls.Song.ImageURL,
                    Category = ls.Song.Category
                }
            }).ToList();
        return listLovedSong;
    }

    public LovedSongResponse CreateLovedSong(CreateLovedSongRequest request)
    {
        var targetSong = _context.Songs
            .FirstOrDefault(s => s.SongID == request.SongID);
        var newLS = new LovedSong()
        {
            UserID = request.UserID,
            Song = targetSong
        };
        _context.Add(newLS);
        _context.SaveChanges();
        return new LovedSongResponse()
        {
            UserID = newLS.UserID,
            Song = newLS.Song
        };
    }

    public LovedSongResponse DeleteLovedSong(DeleteLovedSongRequest request)
    {
        var targetLS = _context.LovedSongs
                .FirstOrDefault(ls => ls.UserID == request.UserID && ls.Song.SongID == request.SongID);
        if (targetLS == null)
        {
            throw new Exception("not found");
        }

        _context.Remove(targetLS);
        _context.SaveChanges();
        return new LovedSongResponse()
        {
            UserID = targetLS.UserID,
            Song = targetLS.Song
        };
    }
}