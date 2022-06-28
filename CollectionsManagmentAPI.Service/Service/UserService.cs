using CollectionsManagmentAPI.DataAccess;
using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.DataAccess.Repositories;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Entity.Enums;
using CollectionsManagmentAPI.Entity.Extensions;
using CollectionsManagmentAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManagmentAPI.Service.Service;

public class UserService : IUserService
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;

    public UserService(IRepository<UserEntity> userRepository, IIdentityService identityService, IUserService userService)
    {
        _userRepository = userRepository;
        _identityService = identityService;
        _userService = userService;
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
    
    public async Task<UserModel> Create(RegisterModel registerModel)
    {
        var userForCheck = _userService.SearchByLogin(registerModel.Username);
        if (userForCheck != null)
        {
            return null;
        }
        
        _identityService.CreatePasswordHash(registerModel.Password, out byte[] passwordHash);
        
        var user = new UserEntity()
        {
            PasswordHash = passwordHash,
            Username = registerModel.Username, 
            EmailAddress = registerModel.EmailAddress, 
            Role = Roles.User,
            FirstName = registerModel?.FirstName, 
            LastName = registerModel?.LastName
        };
        
        await _userRepository.Create(user);

        return user.ConvertToUserModel();
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

    public UserModel SearchByLogin(string login)
    {
        return _userRepository.GetAll().FirstOrDefault(u => u.Username == login).ConvertToUserModel();
    }
}