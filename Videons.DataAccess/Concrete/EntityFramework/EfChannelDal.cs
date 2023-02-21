using Videons.Core.DataAccess.EntityFramework;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfChannelDal : EfEntityRepositoryBase<Channel, VideoAppContext>, IChannelDal
{
    public EfChannelDal(VideoAppContext context) : base(context)
    {
    }
}