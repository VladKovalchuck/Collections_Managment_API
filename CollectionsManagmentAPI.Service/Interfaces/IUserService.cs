using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface IUserService
{
    IQueryable<UserEntity> GetAll();
    IQueryable<UserEntity> GetRange(int skip, int take);
    Task<UserEntity> GetById(int id);
    Task Create(UserEntity user);
    Task Update(UserEntity user);
    Task<bool> Delete(int id);
    UserEntity SearchByLogin(string login);
}