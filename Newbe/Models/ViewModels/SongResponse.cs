﻿namespace Newbe.Models.ViewModels;

public class SongResponse
{
    public Guid SongID { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public List<string> Singers { get; set; }
    public string ImageURL { get; set; }
    
    public string CategoryName { get; set; }
}