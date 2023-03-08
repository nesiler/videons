using Videons.Core.Entities;

namespace Videons.Entities.Concrete;

public class Channel : EntityBase
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public string ImagePath { get; set; } = "";
    public bool Verified { get; set; } = false;
    public ICollection<Video> Videos { get; set; } = new HashSet<Video>();
    public ICollection<History> Histories { get; set; } = new HashSet<History>();
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public ICollection<Subscription> Subscribers { get; set; } = new HashSet<Subscription>();
    public ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();
    public ICollection<Playlist> Playlists { get; set; } = new HashSet<Playlist>();
}