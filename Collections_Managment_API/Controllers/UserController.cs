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
[Route("[controller]")]
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
        return Ok(_userService.GetAll());
    }

    [HttpGet("{skip:int}/{take:int}")]
    public ActionResult<List<UserModel>> GetRange(int skip, int take)
    {
        return Ok(_userService.GetRange(skip, take));
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
        var user = await _userService.Create(registerModel);
        
        if(user == null)
            return BadRequest("This username is already in use");
        
        return Ok(user);
    }
    
    [HttpPut("")]
    public async Task<ActionResult<UserModel>> Update(UpdateModel updateModel)
    {
        var user = await _userService.Update(updateModel);
        return Ok(user);
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

        return Ok(user);
    }
}