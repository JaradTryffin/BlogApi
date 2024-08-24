using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Repositories;

public class PostRepository:IPostRepository
{
    private readonly BlogContext _context;

    public PostRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await _context.Posts.Include(p => p.User).ToListAsync();
    }

    public async Task<Post> GetPostByIdAsync(Guid id)
    {
        return await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Post> UpdatePostAsync(Post post)
    {
        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task DeletePostAsync(Guid id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post != null)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}