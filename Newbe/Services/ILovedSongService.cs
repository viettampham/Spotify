using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services;

public interface ILovedSongService
{
    List<LovedSongResponse> GetListSongLoved();
    LovedSongResponse CreateLovedSong(CreateLovedSongRequest request);
    LovedSongResponse DeleteLovedSong(DeleteLovedSongRequest request);
}