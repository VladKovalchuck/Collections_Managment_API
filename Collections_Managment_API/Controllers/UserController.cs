using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Enums;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace Collections_Managment_API.Controllers;

[ApiController]
[Authorize (Roles = "Admin")]
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
    public async Task<ActionResult<UserModel>> GetById(int id) 
    {
        var user = await _userService.GetById(id);
        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(ConvertToUserModel(user));
    }

    [HttpPost("createUser")]
    public async Task<ActionResult<UserModel>> Create(RegisterModel registerModel)
    {
        var user = await _userService.SearchByLogin(registerModel.Username);
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
            RoleId = (int)Roles.User,
            FirstName = registerModel?.FirstName, 
            LastName = registerModel?.LastName
        };
        await _userService.Create(user);
        
        return Ok(ConvertToUserModel(user));
    }
    
    [HttpPut("updateUser")]
    public async Task<ActionResult<UserModel>> Update(UpdateModel updateModel)
    {
        var user = await _userService.GetById(updateModel.Id);
        user.Username = updateModel.Username;
        user.EmailAddress = updateModel.EmailAddress;
        user.RoleId = updateModel.RoleId;
        user.FirstName = updateModel.FirstName;
        user.LastName = updateModel.LastName;
        user.IsBlocked = updateModel.IsBlocked;
        
        await _userService.Update(user);
        
        return Ok(ConvertToUserModel(user));
    }
    
    [HttpDelete("deleteUser")]
    public async Task<bool> Delete(int id)
    {
        return await _userService.Delete(id);
    }

    [HttpGet("searchUser")]
    public async Task<ActionResult<UserModel>> SearchByLogin(string login)
    {
        var user = await _userService.SearchByLogin(login);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(ConvertToUserModel(user));
    }

    [HttpPut("blockUser")]
    public async Task<ActionResult<UserModel>> BlockUser(string login)
    {
        var user = await _userService.SearchByLogin(login);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        user.IsBlocked = true;
        
        await _userService.Update(user);
        
        return Ok(ConvertToUserModel(user));
    }

    private UserModel ConvertToUserModel(UserEntity user)
    {
        return new UserModel()
        {
            Id = user.Id,
            Username = user.Username,
            EmailAddress = user.EmailAddress,
            RoleId = user.RoleId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash,
            IsBlocked = user.IsBlocked
        };
    }
}