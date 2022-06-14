using CollectionsManagmentAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using CollectionsManagmentAPI.Service.Interfaces;

namespace Collections_Managment_API.Controllers;

[ApiController]
public class UserIdentityController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;

    public UserIdentityController(IIdentityService identityService, IUserService userService)
    {
        _identityService = identityService;
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserEntity>> Register(string username, string email, string firstname, string surname, string password)
    {
        _identityService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        UserEntity user = new UserEntity()
        {
            PasswordHash = passwordHash, PasswordSalt = passwordSalt, Username = username, EmailAddress = email, FirstName = firstname, Surname = surname
        };

        await _userService.Create(user);
        
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginInfo loginInfo)
    {
        var user = await _userService.SearchByLogin(loginInfo.Login);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        if (!_identityService.VerifyPasswordHash(loginInfo.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        string token = _identityService.CreateToken(user);
        return Ok(token);
    }
    
}