using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Repositories;

public class UserRepository:IUserRepository
{
    private readonly BlogContext _context;

    public UserRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.Include(u => u.Posts).ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        if (user.Posts == null)
        {
            user.Posts = new List<Post>();  // Initialize empty list if null
        }
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}