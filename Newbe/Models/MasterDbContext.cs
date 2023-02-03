using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Newbe.Models;

public class MasterDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
{
    public DbSet<ApplicationUser> AspNetUsers { get; set; }
    public DbSet<ApplicationRole> AspNetRoles { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<LovedSong> LovedSongs { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    
    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
        
    }
}