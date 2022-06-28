using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.Service.Interfaces;

public interface IUserService
{
    List<UserModel> GetAll();
    List<UserModel> GetRange(int skip, int take);
    Task<UserEntity> GetById(int id);
    Task<UserModel> Create(RegisterModel registerModel);
    Task<UserModel> Update(UpdateModel updateModel);
    Task<bool> Delete(int id);
    UserModel SearchByLogin(string login);
}