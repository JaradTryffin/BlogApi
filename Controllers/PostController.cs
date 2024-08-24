using BlogApplication.Models;
using BlogApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController:ControllerBase
{
    private readonly IPostRepository _postRepository;

    public PostController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var post = await _postRepository.GetAllPostsAsync();
        return Ok(post);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(Guid id)
    {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(Post post)
    {
        var createdPost = await _postRepository.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(Guid id, Post post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }
        await _postRepository.UpdatePostAsync(post);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        await _postRepository.DeletePostAsync(id);
        return NoContent();
    }
}