using Videons.Core.DataAccess.EntityFramework;
using Videons.Core.Entities;
using Videons.Core.Entities.Concrete;
using Videons.DataAccess.Abstract;

namespace Videons.DataAccess.Concrete.EntityFramework;

public class EfUserDal : EfEntityRepositoryBase<User, VideonsContext>, IUserDal
{
    // private readonly IChannelDal _channelDal;

    public EfUserDal(VideonsContext context) : base(context)
    {
    }

    public List<OperationClaim> GetClaims(User user)
    {
        var result = from operationClaim in Context.OperationClaims
            join userOperationClaim in Context.UserOperationClaims
                on operationClaim.Id equals userOperationClaim.OperationClaimId
            where userOperationClaim.UserId == user.Id
            select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
        return result.ToList();
    }

    // public bool CreateNewUser(User user)
    // {
    //     user.CreatedAt = DateTime.Now.ToUniversalTime();
    //
    //     var newUserEntry = Context.Entry(user);
    //     newUserEntry.State = EntityState.Added;
    //
    //     var channel = new Channel
    //     {
    //         Name = user.FirstName + " " + user.LastName,
    //         Verified = false,
    //         UserId = user.Id
    //     };
    //
    //     return _channelDal.Add(channel);
    // }
}