using Newbe.Models;
using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services;

public interface ISongService
{
    List<SongResponse> GetListSong();
    SongResponse CreateSong(CreateSongRequest request);
    SongResponse EditSong(EditSongRequest request);
    SongResponse DeleteSong(Guid id);
}