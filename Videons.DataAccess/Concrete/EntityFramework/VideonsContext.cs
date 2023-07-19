using Bogus;
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
        var categoryFaker = new Faker<Category>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First());

        var categories = categoryFaker.Generate(5);

        //genreate seed data with bogus with id
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.PasswordSalt, f => f.Random.Bytes(32))
            .RuleFor(u => u.PasswordHash, f => f.Random.Bytes(32))
            .RuleFor(u => u.Status, f => f.Random.Bool())
            .RuleFor(u => u.LastLogin, f => f.Date.Past().ToUniversalTime());

        var users = userFaker.Generate(5);

        var channelFaker = new Faker<Channel>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Name, f => f.Company.CompanyName())
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
            .RuleFor(c => c.UserId, f => f.PickRandom(users).Id);

        var channels = channelFaker.Generate(5);

        var videoFaker = new Faker<Video>()
            .RuleFor(v => v.Id, f => f.Random.Guid())
            .RuleFor(v => v.Title, f => f.Lorem.Sentence())
            .RuleFor(v => v.Description, f => f.Lorem.Paragraph())
            .RuleFor(v => v.StreamId, f => f.Random.Guid().ToString())
            .RuleFor(v => v.Visibility, f => f.PickRandom<VideoVisibility>())
            .RuleFor(v => v.ChannelId, f => f.PickRandom(channels).Id)
            .RuleFor(v => v.CategoryId, f => f.PickRandom(categories).Id)
            .RuleFor(v => v.CreatedAt, f => f.Date.Past().ToUniversalTime())
            .RuleFor(v => v.UpdatedAt, f => f.Date.Past().ToUniversalTime())
            .RuleFor(v => v.PublishDate, f => f.Date.Past().ToUniversalTime())
            .RuleFor(v => v.Views, f => f.Random.UInt());

        var videos = videoFaker.Generate(5);

        //subscription
        var subscriptionFaker = new Faker<Subscription>()
            .RuleFor(s => s.Id, f => f.Random.Guid())
            .RuleFor(s => s.ChannelId, f => f.PickRandom(channels).Id)
            .RuleFor(s => s.SubscriberId, f => f.PickRandom(channels).Id)
            .RuleFor(s => s.Notification, f => f.Random.Bool());

        var subscriptions = subscriptionFaker.Generate(5);

        //history
        var historyFaker = new Faker<History>()
            .RuleFor(h => h.Id, f => f.Random.Guid())
            .RuleFor(h => h.ChannelId, f => f.PickRandom(channels).Id)
            .RuleFor(h => h.VideoId, f => f.PickRandom(videos).Id)
            .RuleFor(h => h.Time, f => f.Random.Short());

        var histories = historyFaker.Generate(5);

        //playlist
        var playlistFaker = new Faker<Playlist>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.Name, f => f.Lorem.Sentence())
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
            .RuleFor(p => p.ChannelId, f => f.PickRandom(channels).Id)
            .RuleFor(p => p.Visibility, f => f.PickRandom<PlaylistVisibility>());

        var playlists = playlistFaker.Generate(5);

        modelBuilder.Entity<Category>().HasData(categories);
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Channel>().HasData(channels);
        modelBuilder.Entity<Video>().HasData(videos);
        modelBuilder.Entity<Subscription>().HasData(subscriptions);
        modelBuilder.Entity<History>().HasData(histories);
        modelBuilder.Entity<Playlist>().HasData(playlists);

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

        //channel user one to many
        modelBuilder.Entity<Channel>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        //comment video one to many
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Video)
            .WithMany(d => d.Comments)
            .HasForeignKey(c => c.VideoId)
            .OnDelete(DeleteBehavior.Cascade);

        //comment channel one to many
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Channel)
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        //history channel one to many
        modelBuilder.Entity<History>()
            .HasOne(h => h.Channel)
            .WithMany(c => c.Histories)
            .HasForeignKey(h => h.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        //history video one to many
        modelBuilder.Entity<History>()
            .HasOne(h => h.Video)
            .WithMany()
            .HasForeignKey(h => h.VideoId)
            .OnDelete(DeleteBehavior.Cascade);

        //subscription channel one to many
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Channel)
            .WithMany(c => c.Subscribers)
            .HasForeignKey(s => s.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        //subscription channel one to many
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Subscriber)
            .WithMany(c => c.Subscriptions)
            .HasForeignKey(s => s.SubscriberId)
            .OnDelete(DeleteBehavior.Cascade);

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
            .HasForeignKey(pv => pv.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);
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
            .HasForeignKey(v => v.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    //soft delete override
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