using AutoMapper;
using Videons.Business.Abstract;
using Videons.Core.Utilities.Results;
using Videons.Entities.DTOs;

namespace Videons.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService, IMapper mapper)
    {
        _authService = authService;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var userExistResult = _authService.UserExists(userForRegisterDto.Email);
        if (userExistResult.Success) return BadRequest(userExistResult);

        var registerResult = _authService.Register(userForRegisterDto);
        if (!registerResult.Success) return BadRequest(registerResult);

        var tokenResult = _authService.CreateAccessToken(registerResult.Data);
        return Ok(tokenResult.Data);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
    {
        if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);


        var userToLoginResult = _authService.Login(userForLoginDto);
        if (!userToLoginResult.Success) return BadRequest(userToLoginResult.Message);

        var tokenResult = _authService.CreateAccessToken(userToLoginResult.Data);
        if (!tokenResult.Success) return BadRequest(tokenResult.Message);

        return Ok(tokenResult.Data);
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUser()
    {
        var currentUser = HttpContext.User;
        var email = currentUser.FindFirst(ClaimTypes.Email)?.Value;
        var user = _userService.GetByEmail(email);
        var userDto = _mapper.Map<CurrentUserDto>(user);
        return Ok(userDto);
    }

    [HttpPut("change-password")]
    [Authorize]
    public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var currentUser = HttpContext.User;
        var userId = new Guid(currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);

        if (changePasswordDto.NewPassword == string.Empty)
            return BadRequest(new ErrorResult("New password cannot be empty"));

        var result = _userService.ChangePassword(userId, changePasswordDto);
        return result.Success
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPut("update-profile")]
    [Authorize]
    public IActionResult UpdateProfile([FromBody] UserUpdateDto userUpdateDto)
    {
        var currentUser = HttpContext.User;
        var userId = new Guid(currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);

        var result = _userService.UpdateProfile(userId, userUpdateDto);
        return result.Success
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpDelete("delete-account")]
    [Authorize]
    public IActionResult DeleteAccount([FromBody] UserForLoginDto userForLoginDto)
    {
        var result = _userService.DeleteAccount(userForLoginDto);
        return result.Success
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpDelete("admin-remove-user/{userId}")]
    public IActionResult AdminRemoveUser(Guid userId)
    {
        var result = _userService.AdminRemoveUser(userId);
        return result.Success
            ? Ok(result)
            : BadRequest(result);
    }
}