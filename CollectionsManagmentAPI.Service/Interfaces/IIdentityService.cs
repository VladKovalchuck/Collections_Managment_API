using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface IIdentityService
{
    void CreatePasswordHash(string password, out byte[] passwordHash);
    bool VerifyPasswordHash(string password, byte[] passwordHash);
    string CreateToken(UserModel user);
}