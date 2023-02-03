using System.ComponentModel.DataAnnotations;

namespace Newbe.Models;

public class LovedSong
{
    [Key]
    public Guid UserID { get; set; }
    public Song Song { get; set; }
    
}