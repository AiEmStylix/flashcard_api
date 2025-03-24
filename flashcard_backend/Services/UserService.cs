using AutoMapper;
using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;

namespace flashcard_backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    {
        var user = _mapper.Map<UserModel>(userDto);
        var createdUser = await _userRepository.CreateUserAsync(user);
        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto userDto)
    {
        var user = _mapper.Map<UserModel>(userDto);
        var updatedUser = await _userRepository.UpdateUserAsync(id, user);
        return _mapper.Map<UserDto>(updatedUser);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }
}