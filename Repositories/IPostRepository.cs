using BlogApplication.Models;

namespace BlogApplication.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllPostsAsync();
    Task<Post> GetPostByIdAsync(Guid id);
    Task<Post> CreatePostAsync(Post post);
    Task<Post> UpdatePostAsync(Post post);
    Task DeletePostAsync(Guid id);
}