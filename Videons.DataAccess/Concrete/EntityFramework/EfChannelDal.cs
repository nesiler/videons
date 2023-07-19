using Videons.Core.DataAccess.EntityFramework;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfChannelDal : EfEntityRepositoryBase<Channel, VideonsContext>, IChannelDal
{
    public EfChannelDal(VideonsContext context) : base(context)
    {
    }
}