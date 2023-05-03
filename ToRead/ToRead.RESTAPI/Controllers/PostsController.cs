using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToRead.RESTAPI.Model;
using ToRead.RESTAPI.Services;

namespace ToRead.RESTAPI.Controllers;

/// <summary>
/// PostsController 的控制器类，包含了一些常见的 CRUD 操作方法：
/// GetPosts()、GetPost()、CreatePost()、UpdatePost() 和 DeletePost()。
/// 这些方法都用来处理 HTTP GET、POST、PUT 和 DELETE 请求，并返回相应的数据结果
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public PostsController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await _db.Posts.Include(p => p.Author).ToListAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _db.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(Post post)
    {
        var user = await _db.Users.FindAsync(post.AuthorId);
        if (user == null)
        {
            return BadRequest("Invalid author ID.");
        }

        post.Author = user;
        _db.Posts.Add(post);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, Post post)
    {
        if (id != post.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var dbPost = await _db.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);
        if (dbPost == null)
        {
            return NotFound();
        }

        dbPost.Title = post.Title;
        dbPost.Content = post.Content;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _db.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}