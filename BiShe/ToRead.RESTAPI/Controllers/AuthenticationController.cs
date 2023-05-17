using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToRead.RESTAPI.Data;
using ToRead.RESTAPI.Models;

namespace ToRead.RESTAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserContext _context;

    public AuthenticationController(UserContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 注册方法接收一个用户对象作参数，并将其添加到数据库中保存
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("Invalid client request");
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "User registered successfully!" });
    }

    /// <summary>
    /// 登录方法接收一个用户对象作参数，并使用Entity Framework Core从数据库中查找该用户。
    /// 找到，返回“用户登录成功！”；否则返回“无效的客户端请求”。
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var existingUser =
            await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

        if (existingUser == null)
        {
            return BadRequest("Invalid client request");
        }

        return Ok(new { Message = "User logged in successfully!" });
    }
}