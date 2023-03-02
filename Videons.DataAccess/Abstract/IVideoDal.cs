using Videons.Core.DataAccess;
using Videons.Entities.Concrete;

namespace Videons.DataAccess.Abstract;

public interface IVideoDal : IEntityRepository<Video>
{
    public Video Watch(Guid videoId);
}