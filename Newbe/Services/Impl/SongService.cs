using System.Runtime.InteropServices.ComTypes;
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
            CategoryName = s.Category.Name
        }).ToList();
        return listSong;
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
            Category = targetCategory
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
            CategoryName = newSong.Category.Name
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
        targetSong.Category = targetCategory;
        _context.SaveChanges();
        return new SongResponse()
        {
            SongID = targetSong.SongID,
            Name = targetSong.Name,
            Author = targetSong.Author,
            Singers = targetSong.Singers,
            ImageURL = targetSong.ImageURL,
            CategoryName = targetSong.Category.Name
        };
    }

    public SongResponse DeleteSong(Guid id)
    {
        var targetSong = _context.Songs.FirstOrDefault(s => s.SongID == id);
        if (targetSong == null)
        {
            throw new Exception("not found");
        }

        _context.Remove(targetSong);
        _context.SaveChanges();

        return new SongResponse()
        {
            SongID = targetSong.SongID,
            Name = targetSong.Name,
            Author = targetSong.Author,
            Singers = targetSong.Singers,
            ImageURL = targetSong.ImageURL,
            CategoryName = targetSong.Category.Name
        };
    }
}