using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Newbe.Models;
using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services.Impl;

public class SongService:ISongService
{
    private readonly MasterDbContext _context;

    public SongService(MasterDbContext context)
    {
        _context = context;
    }
    public List<SongResponse> GetListSong()
    {
        var listSong = _context.Songs.Select(s => new SongResponse()
        {
            SongID = s.SongID,
            Name = s.Name,
            Author = s.Author,
            Singers = s.Singers,
            ImageURL = s.ImageURL,
            PathMusic = s.PathMusic,
            CategoryName = s.Category.Name,
            UserLoved = s.UserLoved,
            IsDelete = s.IsDelete,
        }).ToList();
        var listSongResponse = new List<SongResponse>();
        foreach (var song in listSong)
        {
            if (song.IsDelete == false)
            {
                listSongResponse.Add(song);
            }
        }
        return listSongResponse;
    }

    public List<SongResponse> GetListSongByCategory(Guid id)
    {
        var listSong = _context.Songs
            .Include(s=>s.Category)
            .Select(s=>s).ToList();
        var listSongResponse = new List<SongResponse>();
        foreach (var song in listSong)
        {
            if (song.Category.ID == id)
            {
                var songResponse = new SongResponse()
                {
                    SongID = song.SongID,
                    Name = song.Name,
                    Author = song.Author,
                    Singers = song.Singers,
                    ImageURL = song.ImageURL,
                    PathMusic = song.PathMusic,
                    CategoryName = song.Category.Name,
                    UserLoved = song.UserLoved,
                    IsDelete = song.IsDelete
                };
                listSongResponse.Add(songResponse);
            }
        }

        return listSongResponse;
    }

    public List<SongResponse> GetListSongDeleted()
    {
        var listSong = _context.Songs.Select(s => new SongResponse()
        {
            SongID = s.SongID,
            Name = s.Name,
            Author = s.Author,
            Singers = s.Singers,
            ImageURL = s.ImageURL,
            PathMusic = s.PathMusic,
            CategoryName = s.Category.Name,
            UserLoved = s.UserLoved,
            IsDelete = s.IsDelete,
        }).ToList();
        var listSongResponse = new List<SongResponse>();
        foreach (var song in listSong)
        {
            if (song.IsDelete == true)
            {
                listSongResponse.Add(song);
            }
        }
        return listSongResponse;
    }

    public SongResponse CreateSong(CreateSongRequest request)
    {
        var targetCategory = _context.Categories
            .FirstOrDefault(c => c.ID == request.CategoryID);
        var newSong = new Song()
        {
            SongID = Guid.NewGuid(),
            Name = request.Name,
            Author = request.Author,
            Singers = request.Singers,
            ImageURL = request.ImageURL,
            PathMusic = request.PathMusic,
            Category = targetCategory,
            UserLoved = new List<Guid>(),
            IsDelete = false
        };
        _context.Add(newSong);
        _context.SaveChanges();
        return new SongResponse()
        {
            SongID = newSong.SongID,
            Name = newSong.Name,
            Author = newSong.Author,
            Singers = newSong.Singers,
            ImageURL = newSong.ImageURL,
            PathMusic = newSong.PathMusic,
            CategoryName = newSong.Category.Name,
            UserLoved = newSong.UserLoved,
            IsDelete = false
        };
    }

    public SongResponse EditSong(EditSongRequest request)
    {
        var targetCategory = _context.Categories
            .FirstOrDefault(c => c.ID == request.CategoryID);
        var targetSong = _context.Songs
            .FirstOrDefault(s => s.SongID == request.SongID);
        if (targetSong == null)
        {
            throw new Exception("not found this song");
        }

        targetSong.Name = request.Name;
        targetSong.Author = request.Author;
        targetSong.Singers = request.Singers;
        targetSong.ImageURL = request.ImageURL;
        targetSong.PathMusic = request.PathMusic;
        targetSong.Category = targetCategory;
        _context.SaveChanges();
        return new SongResponse()
        {
            SongID = targetSong.SongID,
            Name = targetSong.Name,
            Author = targetSong.Author,
            Singers = targetSong.Singers,
            ImageURL = targetSong.ImageURL,
            PathMusic = targetSong.PathMusic,
            CategoryName = targetSong.Category.Name
        };
    }

    public bool DeleteSong(Guid id)
    {
        var targetSong = _context.Songs.FirstOrDefault(s => s.SongID == id);
        if (targetSong != null)
        {
            targetSong.IsDelete = true;
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public string DeleteLovedSongByUserID(DeleteUserFromSong request)
    {
        var targetLovedSong = _context.LovedSongs
            .FirstOrDefault(ls => ls.UserID == request.UserID && ls.Song.SongID == request.SongID);
        if (targetLovedSong == null)
        {
            return "Not found";
        }

        var targetSong = _context.Songs
            .FirstOrDefault(s => s.SongID == request.SongID);
        
        _context.Remove(targetLovedSong);
        targetSong.UserLoved.Remove(request.UserID);
        _context.SaveChanges();
        return "Xóa thành công";
    }


    public bool RestoreSong(Guid id)
    {
        var targetSong = _context.Songs.FirstOrDefault(s => s.SongID == id);

        if (targetSong != null)
        {
            targetSong.IsDelete = false;
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}