using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace Collections_Managment_API.Controllers;

[ApiController]
[SwaggerTag("User")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IIdentityService _identityService;

    public UserController(IUserService userService, IIdentityService identityService)
    {
        _userService = userService;
        _identityService = identityService;
    }
    
    [HttpGet("getUser/{id}")]
    public async Task<UserEntity> GetById(int id) 
    {
        var user = await _userService.GetById(id);
        return user;
    }

    [HttpPost("createUser")]
    public async Task<ActionResult<UserEntity>> Create(RegisterModel registerModel)
    {
        UserEntity user = await _userService.SearchByLogin(registerModel.Username);
        if (user != null)
        {
            return BadRequest("this username is already in use");
        }
        
        _identityService.CreatePasswordHash(registerModel.Password, out byte[] passwordHash);

        user = new UserEntity()
        {
            PasswordHash = passwordHash,
            Username = registerModel.Username, 
            EmailAddress = registerModel.EmailAddress, 
            FirstName = registerModel?.FirstName, 
            LastName = registerModel?.LastName
        };
        await _userService.Create(user);
        return Ok(user);
    }
    
    [HttpPut("updateUser")]
    public async Task<ActionResult<UserEntity>> Update(UpdateModel updateModel)
    {
        var user = await _userService.GetById(updateModel.Id);
        user.Username = updateModel.Username;
        user.EmailAddress = updateModel.EmailAddress;
        user.FirstName = updateModel.FirstName;
        user.LastName = updateModel.LastName;
        
        await _userService.Update(user);
        return Ok(user);
    }
    
    [HttpDelete("deleteUser")]
    public async Task<bool> Delete(int id)
    {
        return await _userService.Delete(id);
    }

    [HttpGet("searchUser")]
    public async Task<ActionResult<UserEntity>> SearchByLogin(string login)
    {
        var user = await _userService.SearchByLogin(login);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}