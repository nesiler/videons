using Videons.Core.Entities.Concrete;
using Videons.Core.Utilities.Results;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Abstract;

public interface IUserService
{
    User GetById(Guid id);
    User GetByEmail(string email);
    IResult Add(User user);
    List<OperationClaim> GetClaims(User user);
    IResult ChangePassword(Guid userId, ChangePasswordDto changePasswordDto);
    IResult DeleteAccount(UserForLoginDto userForLoginDto);
}