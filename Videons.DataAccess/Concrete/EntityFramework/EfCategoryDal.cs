using Videons.Core.DataAccess.EntityFramework;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfCategoryDal : EfEntityRepositoryBase<Category, VideoAppContext>, ICategoryDal
{
    public EfCategoryDal(VideoAppContext context) : base(context)
    {
    }
}