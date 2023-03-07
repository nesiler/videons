using Microsoft.EntityFrameworkCore;
using Videons.Core.Entities;
using Videons.Core.Entities.Concrete;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class VideonsContext : DbContext
{
    public VideonsContext(DbContextOptions<VideonsContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<History> History { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistVideo> PlaylistVideo { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Video> Videos { get; set; }
}