using Videons.Core.DataAccess.EntityFramework;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfPlaylistDal : EfEntityRepositoryBase<Playlist, VideonsContext>, IPlaylistDal
{
    public EfPlaylistDal(VideonsContext context) : base(context)
    {
    }
}