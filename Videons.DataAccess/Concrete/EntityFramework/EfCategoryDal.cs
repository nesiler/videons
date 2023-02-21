using Videons.Core.DataAccess.EntityFramework;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfCategoryDal : EfEntityRepositoryBase<Category, VideonsContext>, ICategoryDal
{
    public EfCategoryDal(VideonsContext context) : base(context)
    {
    }
}