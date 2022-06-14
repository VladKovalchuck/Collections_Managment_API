using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace Collections_Managment_API.Controllers;

[ApiController]
[SwaggerTag("User")]
public class UserTestController : Controller
{
    private readonly IUserService _userService;

    public UserTestController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("getUser/{id}")]
    public async Task<UserEntity> GetById(int id) 
    {
        var user = await _userService.GetById(id);
        return user;
    }

    [HttpPost("createUser/")]
    public async Task<ActionResult<UserEntity>> Create(string username, string emailaddress, string password, string firstname, string surname)
    {
        UserEntity user = new UserEntity()
        {
            Username = username, EmailAddress = emailaddress, Password = password, FirstName = firstname,
            Surname = surname
        };
        if (user == null)
        {
            return BadRequest();
        }
        
        await _userService.Create(user);
        return Ok(user);
    }
    
    [HttpPut("updateUser/")]
    public async Task<ActionResult<UserEntity>> Update(int id, string username, string firstname, string surname)
    {
        var userOld = await _userService.GetById(id);
        if (userOld == null)
        {
            return BadRequest();
        }

        UserEntity user = new UserEntity()
        {
            Id = id, Username = username, FirstName = firstname, Surname = surname, EmailAddress = userOld.EmailAddress,
            Password = userOld.Password
        };

        await _userService.Update(user);
        return Ok(user);
    }
    
    [HttpDelete("deleteUser/")]
    public async Task<bool> Delete(int id)
    {
        return await _userService.Delete(id);
    }

    [HttpGet("searchUser/")]
    public async Task<ActionResult<UserEntity>> SearchByLogin(string login)
    {
        var user = await _userService.SearchByLogin(login);
        if (user == null)
        {
            return BadRequest();
        }

        return Ok(user);
    }
}