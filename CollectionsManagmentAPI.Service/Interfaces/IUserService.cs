using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface IUserService
{
    List<UserModel> GetAll();
    List<UserModel> GetRange(int skip, int take);
    Task<UserEntity> GetById(int id);
    Task Create(UserEntity user);
    Task<UserModel> Update(UpdateModel updateModel);
    Task<bool> Delete(int id);
    UserEntity SearchByLogin(string login);
}