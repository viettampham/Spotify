using System.ComponentModel.DataAnnotations;

namespace Newbe.Models;

public class Song
{
    [Key]
    public Guid SongID { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public List<string> Singers { get; set; }
    public string ImageURL { get; set; }
    public string PathMusic { get; set; }
    public Category Category { get; set; }
    public bool IsDelete { get; set; }
    
}