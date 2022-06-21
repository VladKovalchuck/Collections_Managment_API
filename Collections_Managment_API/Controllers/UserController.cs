using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Enums;
using CollectionsManagmentAPI.Entity.Extensions;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Collections_Managment_API.Controllers;

[ApiController]
[Authorize (Roles = "Admin")]
[Route("User")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IIdentityService _identityService;

    public UserController(IUserService userService, IIdentityService identityService)
    {
        _userService = userService;
        _identityService = identityService;
    }

    [HttpGet("")]
    public ActionResult<List<UserModel>> GetAll()
    {
        var users = _userService.GetAll();

        List<UserModel> resultUsers = new List<UserModel>();
        foreach (var user in users)
        {
            resultUsers.Add(user.ConvertToUserModel());
        }
        return Ok(resultUsers);
    }

    [HttpGet("{skip:int}/{take:int}")]
    public ActionResult<List<UserModel>> GetRange(int skip, int take)
    {
        var users = _userService.GetRange(skip, take);
        
        List<UserModel> resultUsers = new List<UserModel>();
        foreach (var user in users)
        {
            resultUsers.Add(user.ConvertToUserModel());
        }
        
        return Ok(resultUsers);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserModel>> GetById(int id) 
    {
        var user = await _userService.GetById(id);
        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user.ConvertToUserModel());
    }

    [HttpPost("")]
    public async Task<ActionResult<UserModel>> Create(RegisterModel registerModel)
    {
        var user = _userService.SearchByLogin(registerModel.Username);
        if (user != null)
        {
            return BadRequest("This username is already in use");
        }
        
        _identityService.CreatePasswordHash(registerModel.Password, out byte[] passwordHash);

        user = new UserEntity()
        {
            PasswordHash = passwordHash,
            Username = registerModel.Username, 
            EmailAddress = registerModel.EmailAddress, 
            Role = Roles.User,
            FirstName = registerModel?.FirstName, 
            LastName = registerModel?.LastName
        };
        await _userService.Create(user);
        
        return Ok(user.ConvertToUserModel());
    }
    
    [HttpPut("")]
    public async Task<ActionResult<UserModel>> Update(UpdateModel updateModel)
    {
        var user = await _userService.GetById(updateModel.Id);
        
        user.Username = updateModel.Username;
        user.EmailAddress = updateModel.EmailAddress;
        user.Role = updateModel.Role;
        user.FirstName = updateModel.FirstName;
        user.LastName = updateModel.LastName;
        user.IsBlocked = updateModel.IsBlocked;
        
        await _userService.Update(user);
        
        return Ok(user.ConvertToUserModel());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<bool> Delete(int id)
    {
        return await _userService.Delete(id);
    }

    [HttpGet("{login}")]
    public ActionResult<UserModel> SearchByLogin(string login)
    {
        var user = _userService.SearchByLogin(login);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.ConvertToUserModel());
    }
}