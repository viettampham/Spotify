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
                ID = ls.ID,
                UserID = ls.UserID,
                SongID = ls.Song.SongID,
                Name = ls.Song.Name,
                Author = ls.Song.Author,
                Singers = ls.Song.Singers,
                ImageURL = ls.Song.ImageURL,
                PathMusic = ls.Song.PathMusic,
                CategoryName = ls.Song.Category.Name,
                UserLoved = ls.Song.UserLoved,
                IsDelete = ls.Song.IsDelete
                
            }).ToList();
        var listLovedSongResponse = new List<LovedSongResponse>();
        foreach (var ls in listLovedSong)
        {
            if (ls.IsDelete == false)
            {
                listLovedSongResponse.Add(ls);
            }
        }
        return listLovedSongResponse;
    }

    public List<LovedSongResponse> GetLovedSongByUser(Guid id)
    {
        var listLovedSong = this.GetListSongLoved();
        var lovedSongResponses = new List<LovedSongResponse>();
        foreach (var lovedSong in listLovedSong)
        {
            if (lovedSong.UserID == id)
            {
                lovedSongResponses.Add(lovedSong);
            }
        }
        return lovedSongResponses;
    }

    public LovedSongResponse CreateLovedSong(CreateLovedSongRequest request)
    {
        var targetSong = _context.Songs
            .FirstOrDefault(s => s.SongID == request.SongID);
        var newLS = new LovedSong()
        {
            ID = Guid.NewGuid(),
            UserID = request.UserID,
            Song = targetSong
        };
        
        _context.Add(newLS);
        targetSong.UserLoved.Add(request.UserID);
        _context.SaveChanges();
        
        return new LovedSongResponse()
        {
            ID = newLS.ID,
            UserID = newLS.UserID,
            SongID = newLS.Song.SongID,
            Name = newLS.Song.Name,
            Author = newLS.Song.Author,
            Singers = newLS.Song.Singers,
            ImageURL = newLS.Song.ImageURL,
            PathMusic = newLS.Song.PathMusic,
            CategoryName = newLS.Song.Category.Name,
            UserLoved = newLS.Song.UserLoved,
            IsDelete = newLS.Song.IsDelete
        };
    }

    public LovedSongResponse DeleteLovedSong(Guid id)
    {
        
        var targetLS = _context.LovedSongs
            .Include(ls=>ls.Song)    
            .FirstOrDefault(ls => ls.ID == id);
        if (targetLS == null)
        {
            throw new Exception("not found");
        }

        var targetSong = _context.Songs
            .FirstOrDefault(s => s.SongID == targetLS.Song.SongID);
        targetSong.UserLoved.Remove(targetLS.UserID);
        _context.Remove(targetLS);
        _context.SaveChanges();
        return new LovedSongResponse()
        {
            ID = targetLS.ID,
            UserID = targetLS.UserID,
            SongID = targetLS.Song.SongID,
            Name = targetLS.Song.Name,
            Author = targetLS.Song.Author,
            Singers = targetLS.Song.Singers,
            ImageURL = targetLS.Song.ImageURL,
            PathMusic = targetLS.Song.PathMusic,
            CategoryName = targetLS.Song.Category.Name,
            UserLoved = targetLS.Song.UserLoved,
            IsDelete = targetLS.Song.IsDelete
        };
    }
}