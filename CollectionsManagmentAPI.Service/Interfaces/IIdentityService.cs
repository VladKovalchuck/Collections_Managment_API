using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface IIdentityService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    string CreateToken(UserEntity user);
}