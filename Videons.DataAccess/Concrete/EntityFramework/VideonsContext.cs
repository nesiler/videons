using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<OperationClaim>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<UserOperationClaim>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Category>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Channel>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Comment>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<History>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Playlist>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<PlaylistVideo>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Subscription>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Video>().HasQueryFilter(e => !e.IsDeleted);
    }

    //override savechanges for soft delete
    // public override int SaveChanges()
    // {
    //     foreach (var entry in ChangeTracker.Entries())
    //     {
    //         var entity = entry.Entity;
    //         if (entry.State != EntityState.Deleted) continue;
    //         entry.State = EntityState.Modified;
    //         entry.CurrentValues["IsDeleted"] = true;
    //         entry.CurrentValues["DeletedAt"] = DateTime.Now.ToUniversalTime();
    //         // entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);
    //         // entity.GetType().GetProperty("DeletedAt")?.SetValue(entity, DateTime.Now.ToUniversalTime());
    //     }
    //
    //     return base.SaveChanges();
    // }

    private void HandleDependent(EntityEntry entry)
    {
        // ...
    }

    private void ProcessEntities()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            foreach (var navigationEntry in entry.Navigations
                         .Where(n => !n.Metadata.IsDependentToPrincipal()))
            {
                if (navigationEntry is CollectionEntry collectionEntry)
                {
                    foreach (var dependentEntry in collectionEntry.CurrentValue)
                    {
                        HandleDependent(Entry(dependentEntry));
                    }
                }
                else
                {
                    var dependentEntry = navigationEntry.CurrentValue;
                    if (dependentEntry != null)
                    {
                        HandleDependent(Entry(dependentEntry));
                    }
                }
            }
        }
    }
}