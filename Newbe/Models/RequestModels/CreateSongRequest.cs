namespace Newbe.Models.RequestModels;

public class CreateSongRequest
{
    public string Name { get; set; }
    public string Author { get; set; }
    public List<string> Singers { get; set; }
    public string ImageURL { get; set; }
    
    public Guid CategoryID { get; set; }
}