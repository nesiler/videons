using Videons.Core.Entities.Concrete;

namespace Videons.Core.Utilities.Security.Jwt;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
}