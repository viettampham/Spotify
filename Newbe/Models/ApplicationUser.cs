using Microsoft.AspNetCore.Identity;

namespace Newbe.Models;

public class ApplicationUser:IdentityUser<Guid>
{
    public List<LovedSong> ULovedSongs { get; set; }
}