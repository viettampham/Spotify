namespace Newbe.Models.RequestModels;

public class DeleteLovedSongRequest
{
    public Guid UserID { get; set; }
    public Guid SongID { get; set; }
}