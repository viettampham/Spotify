namespace Newbe.Models.ViewModels;

public class LovedSongResponse
{
    public Guid ID { get; set; }
    public Guid UserID { get; set; }
    public Song Song { get; set; }
}