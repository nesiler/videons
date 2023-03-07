using Videons.Business.Abstract;
using Videons.Core.Entities;
using Videons.Core.Utilities.Results;
using Videons.Core.Utilities.Security.Hashing;
using Videons.Core.Utilities.Security.Jwt;
using Videons.Entities.DTOs;

namespace Videons.Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly ITokenHelper _tokenHelper;
    private readonly IUserService _userService;
    private readonly IChannelService _channelService;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, IChannelService channelService)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
        _channelService = channelService;
    }

    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
    {
        HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out var passwordHash, out var passwordSalt);

        var user = new User
        {
            FirstName = userForRegisterDto.FirstName,
            LastName = userForRegisterDto.LastName,
            Email = userForRegisterDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true
        };
        // return _channelService.RegisterChannel(user).Success
            return _userService.Add(user).Success
            ? new SuccessDataResult<User>(user, "Registration successful")
            : new ErrorDataResult<User>(null, "Registration failed");
    }

    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
        if (userToCheck == null) return new ErrorDataResult<User>(null, "User not found");

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,
                userToCheck.PasswordSalt)) return new ErrorDataResult<User>(null, "Invalid password");

        if (userToCheck.Status == false) return new ErrorDataResult<User>(null, "User not confirmed");

        return new SuccessDataResult<User>(userToCheck, "Login successful");
    }

    public IResult UserExists(string email)
    {
        if (_userService.GetByEmail(email) != null) return new SuccessResult("Already exist");

        return new ErrorResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = _userService.GetClaims(user);
        var accessToken = _tokenHelper.CreateToken(user, claims);
        return new SuccessDataResult<AccessToken>(accessToken, "Access token is created.");
    }
}