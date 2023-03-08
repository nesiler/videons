using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Video> Videos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Expression<Func<EntityBase, bool>> filterExpr = bm => !bm.IsDeleted;
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
            // check if current entity type is child of BaseModel
            if (entity.ClrType.IsAssignableTo(typeof(EntityBase)))
            {
                // modify expression to handle correct child type
                var parameter = Expression.Parameter(entity.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter,
                    filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                // set filter
                entity.SetQueryFilter(lambdaExpression);
            }

        #region Builders 
        //channel user one to many
            modelBuilder.Entity<Channel>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            //comment video one to many
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Video)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.VideoId);

            //comment channel one to many
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Channel)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.ChannelId);

            //history channel one to many
            modelBuilder.Entity<History>()
                .HasOne(h => h.Channel)
                .WithMany(c => c.Histories)
                .HasForeignKey(h => h.ChannelId);

            //history video one to many
            modelBuilder.Entity<History>()
                .HasOne(h => h.Video)
                .WithMany()
                .HasForeignKey(h => h.VideoId);

            //subscription channel one to many
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Channel)
                .WithMany(c => c.Subscribers)
                .HasForeignKey(s => s.ChannelId);

            //subscription channel one to many
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Subscriber)
                .WithMany(c => c.Subscriptions)
                .HasForeignKey(s => s.SubscriberId);

            //playlist channel one to many
            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.Channel)
                .WithMany(c => c.Playlists)
                .HasForeignKey(p => p.ChannelId);

            //playlist video many to many
            modelBuilder.Entity<PlaylistVideo>()
                .HasKey(pv => new { pv.PlaylistId, pv.VideoId });
            modelBuilder.Entity<PlaylistVideo>()
                .HasOne(pv => pv.Playlist)
                .WithMany(p => p.PlaylistVideos)
                .HasForeignKey(pv => pv.PlaylistId);
            modelBuilder.Entity<PlaylistVideo>()
                .HasOne(pv => pv.Video)
                .WithMany(v => v.PlaylistVideos)
                .HasForeignKey(pv => pv.VideoId);

            //video category one to many
            modelBuilder.Entity<Video>()
                .HasOne(v => v.Category)
                .WithMany(c => c.Videos)
                .HasForeignKey(v => v.CategoryId);

            //video channel one to many
            modelBuilder.Entity<Video>()
                .HasOne(v => v.Channel)
                .WithMany(c => c.Videos)
                .HasForeignKey(v => v.ChannelId);
            #endregion
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

    // private void HandleDependent(EntityEntry entry)
    // {
    //     // ...
    // }
    //
    // private void ProcessEntities()
    // {
    //     foreach (var entry in ChangeTracker.Entries())
    //     {
    //         foreach (var navigationEntry in entry.Navigations
    //                      .Where(n => !n.Metadata.IsDependentToPrincipal()))
    //         {
    //             if (navigationEntry is CollectionEntry collectionEntry)
    //             {
    //                 foreach (var dependentEntry in collectionEntry.CurrentValue)
    //                 {
    //                     HandleDependent(Entry(dependentEntry));
    //                 }
    //             }
    //             else
    //             {
    //                 var dependentEntry = navigationEntry.CurrentValue;
    //                 if (dependentEntry != null)
    //                 {
    //                     HandleDependent(Entry(dependentEntry));
    //                 }
    //             }
    //         }
    //     }
    // }
}