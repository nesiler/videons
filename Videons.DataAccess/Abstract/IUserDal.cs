using Videons.Core.DataAccess;
using Videons.Core.Entities.Concrete;

namespace Videons.DataAccess.Abstract;

public interface IUserDal : IEntityRepository<User>
{
    List<OperationClaim> GetClaims(User user);
}