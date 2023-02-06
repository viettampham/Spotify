using Newbe.Models;
using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services;

public interface ISongService
{
    List<SongResponse> GetListSong();
    List<SongResponse> GetListSongByCategory(Guid id);
    List<SongResponse> GetListSongDeleted();
    SongResponse CreateSong(CreateSongRequest request);
    SongResponse EditSong(EditSongRequest request);
    bool DeleteSong(Guid id);
    bool RestoreSong(Guid id);
}