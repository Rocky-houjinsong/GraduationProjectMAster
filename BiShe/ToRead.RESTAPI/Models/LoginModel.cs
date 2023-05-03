namespace ToRead.RESTAPI.Models;

/*在 ASP.NET Core Web API 的控制器中，你可以在方法参数列表中使用这个类来接收 POST 请求的 JSON 数据。例如，在 Login 方法中，我们可以通过添加 FromBody 特性来将请求主体反序列化为 LoginModel 对象
 当客户端向 /api/auth/login 发送 POST 请求时，Web API 会自动将请求主体的 JSON 数据反序列化为 LoginModel 对象，并传递给 Login 方法作为参数*/
/// <summary>
/// Username 和 Password，分别代表用户输入的用户名和密码
/// </summary>
public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}