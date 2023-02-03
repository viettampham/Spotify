namespace Newbe.Models.RequestModels;

public class EditSongRequest
{
    public Guid SongID { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public List<string> Singers { get; set; }
    public string ImageURL { get; set; }
    public string PathMusic { get; set; }
    public Guid CategoryID { get; set; }
}