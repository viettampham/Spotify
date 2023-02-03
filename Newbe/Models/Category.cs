using System.ComponentModel.DataAnnotations;

namespace Newbe.Models;

public class Category
{
    [Key]
    public Guid ID { get; set; }
    public string Name { get; set; }
    public List<Song> Songs { get; set; }
}