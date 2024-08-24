using BlogApplication.Models;

namespace BlogApplication.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user); 
    Task DeleteUserAsync(Guid userId);
}