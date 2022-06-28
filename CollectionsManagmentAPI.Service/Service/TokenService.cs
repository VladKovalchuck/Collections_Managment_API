using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CollectionsManagmentAPI.Service.Service;

public class TokenService : ITokenService
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _accessor;
    public TokenService(IUserService userService, IHttpContextAccessor accessor)
    {
        _userService = userService;
        _accessor = accessor;
    }
    public async Task<UserEntity> GetUserFromToken()
    {
        var id = _accessor.HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == "Id");

        return await _userService.GetById(Int32.Parse(id.Value));
    }
}