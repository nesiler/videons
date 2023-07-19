using Videons.Core.Entities;
using Videons.Core.Entities.Concrete;
using Videons.Core.Utilities.Results;
using Videons.Entities.DTOs;

namespace Videons.Business.Abstract;

public interface IUserService
{
    User GetById(Guid id);
    User GetByEmail(string email);
    List<OperationClaim> GetClaims(User user);
    IResult Add(User user);
    IResult ChangePassword(Guid userId, ChangePasswordDto changePasswordDto);
    IResult DeleteAccount(UserForLoginDto userForLoginDto);
    IResult UpdateProfile(Guid userId, UserUpdateDto userUpdateDto);
    IResult AdminRemoveUser(Guid userId);
}