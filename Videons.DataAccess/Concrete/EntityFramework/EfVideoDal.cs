using Microsoft.EntityFrameworkCore;
using Videons.Core.DataAccess.EntityFramework;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfVideoDal : EfEntityRepositoryBase<Video, VideonsContext>, IVideoDal
{
    public EfVideoDal(VideonsContext context) : base(context)
    {
    }

    public bool Watch(Guid videoId)
    {
        var video = Context.Videos.FirstOrDefault(v => v.Id == videoId);
        video.Views++;

        var entry = Context.Entry(video);
        entry.State = EntityState.Modified;

        return Context.SaveChanges() > 0;
    }
}