namespace Newbe.Models.RequestModels;

public class CreateLovedSongRequest
{
    public Guid UserID { get; set; }
    public Guid SongID { get; set; }
}