using CollectionsManagmentAPI.Entity;
using Microsoft.AspNetCore.Http;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface ITokenService
{
    Task<UserEntity> GetUserFromToken();
}