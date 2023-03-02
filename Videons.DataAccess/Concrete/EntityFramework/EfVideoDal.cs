using Videons.Core.DataAccess.EntityFramework;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfVideoDal : EfEntityRepositoryBase<Video, VideonsContext>, IVideoDal
{
    public EfVideoDal(VideonsContext context) : base(context)
    {
    }

    public Video Watch(Guid videoId)
    {
        throw new NotImplementedException();
    }
}