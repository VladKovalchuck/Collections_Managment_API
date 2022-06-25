using CollectionsManagmentAPI.DataAccess;
using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.DataAccess.Repositories;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Extensions;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManagmentAPI.Service.Service;

public class UserService : IUserService
{
    private readonly IRepository<UserEntity> _userRepository;
    
    public UserService(IRepository<UserEntity> userRepository)
    {
        _userRepository = userRepository;
    }

    public List<UserModel> GetAll()
    {
        var users = _userRepository.GetAll();
        List<UserModel> resultUsers = new List<UserModel>();
        foreach (var user in users)
        {
            resultUsers.Add(user.ConvertToUserModel());
        }

        return resultUsers;
    }

    public List<UserModel> GetRange(int skip, int take)
    {
        var users = _userRepository.GetAll().OrderBy(u => u.Id).Skip(skip).Take(take);
        List<UserModel> resultUsers = new List<UserModel>();
        foreach (var user in users)
        {
            resultUsers.Add(user.ConvertToUserModel());
        }

        return resultUsers;
    }
    
    public async Task<UserEntity> GetById(int id)
    {
        var user = await _userRepository.GetById(id);
        return user;
    }
    
    public async Task Create(UserEntity user)
    {
        await _userRepository.Create(user);
    }

    public async Task<UserModel> Update(UpdateModel updateModel)
    {
        var user = await _userRepository.GetById(updateModel.Id);
        
        user.Username = updateModel.Username;
        user.EmailAddress = updateModel.EmailAddress;
        user.Role = updateModel.Role;
        user.FirstName = updateModel.FirstName;
        user.LastName = updateModel.LastName;
        user.IsBlocked = updateModel.IsBlocked;
        
        await _userRepository.Update(user);
        return user.ConvertToUserModel();
    }

    public async Task<bool> Delete(int id)
    {
        bool result = await _userRepository.Delete(id);
        return result;
    }

    public UserEntity SearchByLogin(string login)
    {
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Username == login);
        return user;
    }
}