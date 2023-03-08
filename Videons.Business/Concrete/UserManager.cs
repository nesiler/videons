using Videons.Business.Abstract;
using Videons.Core.Entities;
using Videons.Core.Entities.Concrete;
using Videons.Core.Utilities.Results;
using Videons.Core.Utilities.Security.Hashing;
using Videons.DataAccess.Abstract;
using Videons.Entities.Concrete;
using Videons.Entities.DTOs;

namespace Videons.Business.Concrete;

public class UserManager : IUserService
{
    private readonly IChannelDal _channelDal;
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal, IChannelDal channelDal)
    {
        _userDal = userDal;
        _channelDal = channelDal;
    }

    public User GetById(Guid id)
    {
        return _userDal.Get(u => u.Id == id);
    }

    public User GetByEmail(string email)
    {
        return _userDal.Get(u => u.Email == email);
    }

    public IResult Add(User user)
    {
        if(_userDal.Add(user) == null) return new ErrorResult("User cannot created!");
        
        var channel = new Channel
        {
            Name = user.FirstName + " " + user.LastName,
            Slug = user.FirstName + "-" + user.LastName,
            Description = $"My name is {user.FirstName} {user.LastName} and I'm a Videons user.",
            Verified = false,
            UserId = user.Id
        };
        if(_channelDal.Add(channel) == null) return new ErrorResult("Channel cannot created!");

        return new SuccessResult("User created.");
    }

    public List<OperationClaim> GetClaims(User user)
    {
        return _userDal.GetClaims(user);
    }

    public IResult ChangePassword(Guid userId, ChangePasswordDto changePasswordDto)
    {
        var user = GetById(userId);
        if (user == null) return new ErrorResult("User not found!");

        if (!HashingHelper.VerifyPasswordHash(changePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            return new ErrorResult("Current password is incorrect!");

        HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword, out var passwordHash, out var passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        return _userDal.Update(user)
            ? new SuccessResult("Password updated.")
            : new ErrorResult("Password cannot updated");
    }

    public IResult DeleteAccount(UserForLoginDto userForLoginDto)
    {
        var user = GetByEmail(userForLoginDto.Email);
        if (user == null) return new ErrorResult("User not found!");

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            return new ErrorResult("Password is incorrect!");

        return _userDal.Delete(user)
            ? new SuccessResult("Account deleted.")
            : new ErrorResult("Account cannot deleted!");
    }
    public IResult UpdateProfile(Guid userId, UserUpdateDto userUpdateDto)
    {
        User user = GetById(userId);
        
        if (user == null) return new ErrorResult("User not found!");
        
        user.FirstName = userUpdateDto.FirstName;
        user.LastName = userUpdateDto.LastName;

        // foreach (var property in user.GetType().GetProperties())
        // {
        //     if (property.GetValue(user) != null)
        //     {
        //         property.SetValue(user, property.GetValue(user));
        //     }
        // }

        return _userDal.Update(user)
            ? new SuccessResult("User updated.")
            : new ErrorResult("User cannot updated!");
    }
}