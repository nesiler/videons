using Microsoft.EntityFrameworkCore;
using Videons.Core.DataAccess.EntityFramework;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfPlaylistDal : EfEntityRepositoryBase<Playlist, VideonsContext>, IPlaylistDal
{
    public EfPlaylistDal(VideonsContext context) : base(context)
    {
    }

    public bool AddVideoToPlaylist(PlaylistVideo playlistVideo)
    {
        var addedEntity = Context.Entry(playlistVideo);
        addedEntity.State = EntityState.Added;
        return Context.SaveChanges() > 0;
    }
}