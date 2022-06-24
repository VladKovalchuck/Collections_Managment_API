using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Service.Interfaces;
using CollectionsManagmentAPI.Service.Service;

namespace Collections_Managment_API;

public static class GetFromToken
{
    public static async Task<UserEntity> GetUserFromToken(HttpContext context, IUserService _userService)
    {
        var id = context.User.Claims
            .FirstOrDefault(x => x.Type == "Id");

        return await _userService.GetById(Int32.Parse(id.Value));
    }
}