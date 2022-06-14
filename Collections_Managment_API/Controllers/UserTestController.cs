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

    [HttpPost("createUser/{Username}&{EmailAddress}&{Password}&{FirstName}&{Surname}")]
    public async Task<ActionResult<UserEntity>> Create(UserEntity user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        _userService.Create(user);
        return Ok(user);
    }
    
}