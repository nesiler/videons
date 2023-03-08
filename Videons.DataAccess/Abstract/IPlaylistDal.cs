using Videons.Core.DataAccess;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Abstract;

public interface IPlaylistDal : IEntityRepository<Playlist>
{
    public bool AddVideoToPlaylist(PlaylistVideo playlistVideo);
}