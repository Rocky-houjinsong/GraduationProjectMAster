namespace ToRead.RESTAPI.Models;

/// <summary>
/// 用户输入的电子邮件地址、密码和确认密码
/// </summary>
public class RegisterModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}