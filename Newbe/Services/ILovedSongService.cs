using Newbe.Models.RequestModels;
using Newbe.Models.ViewModels;

namespace Newbe.Services;

public interface ILovedSongService
{
    List<LovedSongResponse> GetListSongLoved();
    List<LovedSongResponse> GetLovedSongByUser(Guid guid);
    string CreateLovedSong(CreateLovedSongRequest request);
    string DeleteLovedSong(Guid id);
}