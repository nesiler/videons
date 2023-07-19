using Videons.Core.DataAccess.EntityFramework;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfHistoryDal : EfEntityRepositoryBase<History, VideonsContext>, IHistoryDal
{
    public EfHistoryDal(VideonsContext context) : base(context)
    {
    }
}