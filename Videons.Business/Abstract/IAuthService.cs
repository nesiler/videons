using System.Collections.Generic;
using VideoApp.Entities.DTOs;
using Videons.Core.Entities.Concrete;
using Videons.Core.Utilities.Results;
using Videons.Core.Utilities.Security.Jwt;
using Videons.Entities.DTOs;

namespace VideoApp.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}