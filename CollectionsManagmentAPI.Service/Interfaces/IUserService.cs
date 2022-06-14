using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface IUserService
{
    Task<UserEntity> GetById(int id);
    Task Create(UserEntity user);
    Task Update(UserEntity user);
    Task<bool> Delete(int id);
}