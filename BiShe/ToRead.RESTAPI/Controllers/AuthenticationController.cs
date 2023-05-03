using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToRead.RESTAPI.Model;
using ToRead.RESTAPI.Models;
using ToRead.RESTAPI.Services;

namespace ToRead.RESTAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtService _jwtService;

    public AuthenticationController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        JwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    /// <summary>
    /// 添加 [FromBody] 特性来将请求主体反序列化为 RegisterModel 对象
    /// 处理 POST /api/auth/register 路径上的请求，并从请求正文中获取 JSON 数据并将其转换成 RegisterModel 对象
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<string>> Register([FromBody] RegisterModel model)
    {
        // Validate email and password
        if (string.IsNullOrWhiteSpace(model.Email) || !IsValidEmail(model.Email))
        {
            return BadRequest("Invalid email address");
        }

        if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < 6)
        {
            return BadRequest("Password must be at least 6 characters long");
        }

        // Create user account
        var user = new User { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        // Check if user was created successfully
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.FirstOrDefault()?.Description);
        }

        var token = _jwtService.GenerateToken(user);
        return Ok(token);
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
    {
        // Verify email and password
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null) return BadRequest("Invalid email or password.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded) return BadRequest("Invalid email or password.");
        // Generate JWT token
        var token = _jwtService.GenerateToken(user);
        return Ok(token);
    }
}