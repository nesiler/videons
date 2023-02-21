using VideoApp.DataAccess.Abstract;
using Videons.Core.DataAccess.EntityFramework;
using Videons.Entities.Concrete;
using Videons.DataAccess.Abstract;

namespace VideoApp.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, VideoAppContext>, ICategoryDal
    {
        public EfCategoryDal(VideoAppContext context) : base(context)
        {
        }
    }
}