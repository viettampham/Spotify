namespace Newbe.Models.RequestModels;

public class DeleteUserFromSong
{
    public Guid UserID { get; set; }
    public Guid SongID { get; set; }
}