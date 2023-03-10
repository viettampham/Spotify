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
    MessageResponse DeleteSong(Guid id);
    MessageResponse DeleteLovedSongByUserID(DeleteUserFromSong request);
    MessageResponse RestoreSong(Guid id);
}