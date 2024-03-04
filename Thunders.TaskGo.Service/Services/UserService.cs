using Thunders.TaskGo.Domain.DTO.User;
using Thunders.TaskGo.Domain.Entities;
using Thunders.TaskGo.Domain.Exceptions;
using Thunders.TaskGo.Infra.Repositories;

namespace Thunders.TaskGo.Service.Services;

public interface IUserService
{
    Task<IEnumerable<UserEntity>> GetUsersAsync();

    Task<UserEntity> GetUserByIdAsync(Guid userId);

    Task AddUsersAsync(NewUserDTO user);

    Task<UserEntity> UpdateUsersAsync(UpdateUserDTO user);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserEntity>> GetUsersAsync()
    {
        return await _userRepository.GetUsersAsync();
    }   

    public async Task<UserEntity> GetUserByIdAsync(Guid userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }

    public async Task AddUsersAsync(NewUserDTO user)
    {
        var newUser = new UserEntity()
        {
            Id = new Guid(),
            Name = user.Email,
            Password = user.Password
        };
        await _userRepository.AddAsync(newUser);
    }

    public async Task<UserEntity> UpdateUsersAsync(UpdateUserDTO user)
    {
        var userDb = await _userRepository.GetUserByIdAsync(user.Id);
        if(userDb != null)
            throw new BusinessException("Usuário não encontrado!");
        
        userDb.Password = user.Password;
        
        return await _userRepository.UpdateAsync(userDb);
    }
}
