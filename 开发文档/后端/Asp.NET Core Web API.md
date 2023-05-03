## 新建项目  ToRead.RESTAPI ,启用OPENAPI ，即SWAGGER

----

如果您在 Visual Studio 中使用的是中文版，那么“启用 Swagger”选项对应的是“启用 OpenAPI 支持”（Enable OpenAPI support）。您可以按照以下步骤进行操作：

1. 在 Visual Studio 中新建一个 [ASP.NET](http://asp.net/) Core Web API 项目。
2. 在创建项目的向导中，选择“空”模板，并点击“创建”按钮。
3. 在项目创建完成后打开“NuGet 包管理器控制台”（Tools -> NuGet Package Manager -> Package Manager Console）。
4. 在“NuGet 包管理器控制台”中运行以下命令：`Install-Package Swashbuckle.AspNetCore -Version 5.6.3`
5. 在项目中打开“Startup.cs”文件，在 `ConfigureServices` 方法中添加以下代码：

```
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
```

1. 在 `Configure` 方法中添加以下代码：

```
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
```

1. 运行项目，在浏览器中访问 `http://localhost:<port>/swagger` 即可查看 Swagger 文档。

请注意，以上代码示例中的 `Title` 和 `Version` 可以根据您的实际需求进行更改。

上述代码会注册一个 Swagger 文档，并将 Swagger UI 集成到应用程序中，您可以通过 `http://localhost:<port>/swagger` 访问 Swagger UI。其中 `<port>` 是您的应用程序监听的端口号。

需要注意的是，上面的代码片段中使用的命名空间是 `ToRead.RESTAPI`。如果您的项目命名空间不同，请将代码中的命名空间替换为您项目的实际命名空间

> 运行成功 , https://localhost:7264/swagger/index.html

---

## 编写模型类和数据库上下文

编写一些模型类和数据库上下文类，用于表示论坛中的帖子、用户和评论等数据，以及与数据库进行交互。例如

```
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string AuthorId { get; set; }
    public User Author { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string AuthorId { get; set; }
    public User Author { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
}

public class User : IdentityUser
{
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}

```

在上面的示例中，我们定义了三个模型类：Post 表示帖子，Comment 表示评论，User 表示用户。每个 Post 包含一个或多个 Comment，每个 Comment 属于一个 Post，并且都有一个 AuthorId 属性指向其作者的 ID。每个 User 包含一个或多个 Post。

接下来，我们需要创建一个 ApplicationDbContext 数据库上下文，用于与数据库进行交互，例如：

```
public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

```

在上面的示例中，我们继承了 IdentityDbContext<User> 类，表示使用 [ASP.NET](http://asp.net/) Core Identity 进行认证授权。另外，我们还分别添加了两个 DbSet<Post> 和 DbSet<Comment> 属性，表示在数据库中创建 Post 和 Comment 表。在 OnModelCreating() 方法中，我们使用 Fluent API 配置了一些实体之间的关系，例如一个 Post 只能有一个 Author，但一个 User 可以有多个 Post，一个 Comment 必须属于一个 Post，等等。

---

## 3. 编写控制器类

接下来，我们需要编写一些控制器类，用于处理 HTTP 请求，并调用相应的业务逻辑。例如：

```
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

```

在上面的示例中，我们定义了一个名为 PostsController 的控制器类，包含了一些常见的 CRUD 操作方法：GetPosts()、GetPost()、CreatePost()、UpdatePost() 和 DeletePost()。这些方法都用来处理 HTTP GET、POST、PUT 和 DELETE 请求，并返回相应的数据结果。

例如，GetPosts() 方法表示获取所有帖子信息的请求，它使用 Entity Framework Core 中的 Include() 方法来加载每个帖子的作者信息，并返回 Ok(posts) 结果。CreatePost() 方法表示创建帖子的请求，它检查 AuthorId 是否有效，然后将新帖子添加到数据库并返回 CreatedAtAction(nameof(GetPost), new { id = [post.Id](http://post.id/) }, post) 结果。UpdatePost() 方法表示更新帖子的请求，它根据传入的帖子 ID 查找数据库中对应的帖子，并更新其标题和内容。DeletePost() 方法表示删除帖子的请求，它根据传入的帖子 ID 查找数据库中对应的帖子，并删除它。

---

## 4. 编写身份验证和授权逻辑

接下来，我们需要编写一些身份验证和授权逻辑，用于保护论坛中的数据和操作。例如：

```
public class JwtService
{
    private readonly string _secretKey;
    private readonly SymmetricSecurityKey _signingKey;

    public JwtService(IConfiguration configuration)
    {
        _secretKey = configuration.GetValue<string>("JwtSettings:SecretKey");
        _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(7);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

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

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(RegisterModel model)
    {
        var user = new User { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        var token = _jwtService.GenerateToken(user);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null) return BadRequest("Invalid email or password.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded) return BadRequest("Invalid email or password.");

        var token = _jwtService.GenerateToken(user);
        return Ok(token);
    }
}

```

在上面的示例中，我们定义了一个名为 JwtService 的服务类，用于生成 JSON Web Token（JWT）令牌，并将其添加到每个请求的 Authorization 请求头中。我们还定义了一个名为 AuthenticationController 的控制器类，包含了两个方法：Register() 和 Login()。

例如，Register() 方法表示注册新用户的请求，它创建一个新的 User 对象，并使用 UserManager 类将其保存到数据库中。如果保存成功，它将返回一个 JWT 令牌给客户端。Login() 方法表示用户登录的请求，它首先查找数据库中是否存在该用户，然后使用 SignInManager 类进行密码验证。如果验证成功，它将返回一个 JWT 令牌给客户端。

----

## 5. 配置 Entity Framework Core

接下来，我们需要配置 Entity Framework Core，使其能够连接到数据库并执行操作。在 appsettings.json 文件中添加以下配置：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ForumDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },

  "JwtSettings": {
    "SecretKey": "your-secret-key"
  },

  "Swagger": {
    "EndpointUrl": "/swagger/v1/swagger.json",
    "ApiKey": ""
  },

  "AllowedHosts": "*"
}

```

在上面的示例中，我们定义了一个名为 DefaultConnection 的数据库连接字符串，用于连接到本地 SQL Server Express 实例，并打开 MultipleActiveResultSets 选项。我们还添加了一个名为 JwtSettings 的配置节，用于存储 JWT 加密密钥。

----

## 6. 配置 Swagger 和 CORS

在 Startup.cs 文件中添加以下代码：

```c#
using Microsoft.OpenApi.Models;

namespace ToRead.RESTAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            //配置 Swagger 和 CORS，以便于测试和调试 API 接口
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Forum API", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };
                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            //添加
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //添加匿名函数,启用
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //配置CORS 
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.MapControllers();

            app.Run();
        }
    }
}
```

> 安装 System.IdentityModel.Tokens.Jwt  包 

在上面的示例中，我们使用 AddSwaggerGen() 方法添加 Swagger 文档生成器，并定义了一个名为 "v1" 的文档。我们还定义了一个 JWT 安全方案，并将其添加到了 Swagger 配置中。

我们还使用 AddCors() 方法添加 CORS 策略，并允许所有来源、所有方法和所有标头。最后，我们在 Configure() 方法中使用 UseSwagger() 方法和 UseSwaggerUI() 方法启用了 Swagger 文档和 UI，并使用 UseCors() 方法启用了 CORS 中间件。

---

登录和注册 

在给出验证邮箱密码是否正确的实现之前，请先注意一下代码中 `FindByNameAsync` 方法的使用。这个方法是用来根据用户名进行查找用户的，但在我们的示例中，它被用来根据电子邮件地址查找用户。因此，你需要将其修改为 `FindByEmailAsync` 方法，以便按照预期进行工作。

以下是一个基于 `_signInManager.PasswordSignInAsync()` 方法的示例，用来验证用户的电子邮件和密码是否正确：

```
[HttpPost("login")]
public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
{
    // Verify email and password
    var user = await _userManager.FindByEmailAsync(model.Email);
    if (user == null) 
    {
        return BadRequest("Invalid email or password.");
    }

    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);
    if (!result.Succeeded) 
    {
        return BadRequest("Invalid email or password.");
    }

    // Generate JWT token
    var token = _jwtService.GenerateToken(user);

    return Ok(token);
}

```

在上述示例中，我们首先调用了 `_userManager.FindByEmailAsync()` 方法来查找电子邮件地址对应的用户对象。如果查找失败，则返回 `BadRequest` 响应。否则，我们调用了 `_signInManager.PasswordSignInAsync()` 方法来验证用户输入的密码是否正确。如果验证失败，则也返回 `BadRequest` 响应。否则，我们使用 JWT 服务生成一个 JWT 令牌，并将其作为 `Ok` 响应的内容返回。

需要注意的是，这里使用了 `user.UserName` 作为用户名参数传递给 `_signInManager.PasswordSignInAsync()` 方法。在 [ASP.NET](http://asp.net/) Core Identity 中，电子邮件地址通常也用作用户名，因此可以将用户名和电子邮件地址视为等价，但这并非一定如此，具体情况需要根据你的应用程序需求而定。



----

登录注册  

在默认情况下，[ASP.NET](http://asp.net/) Core Identity 使用电子邮件地址作为用户的用户名，并且密码是由 Identity 框架中的密码哈希算法生成的散列值。因此，在用户注册后，他们的电子邮件和密码将被存储在数据库中，并且可以用于登录。

例如，在 `Register` 方法中，我们使用以下代码创建新用户：

```C#
var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
var result = await _userManager.CreateAsync(user, model.Password);
```

在上述代码中，我们使用电子邮件地址作为用户名，并将密码作为参数传递给 `_userManager.CreateAsync()` 方法。这个方法将使用 [ASP.NET](http://asp.net/) Core Identity 中提供的密码哈希算法来生成密码的散列值，并将其与用户名一起存储在数据库中。

接下来，在 `Login` 方法中，我们使用以下代码验证用户的电子邮件和密码是否正确：

```
var user = await _userManager.FindByEmailAsync(model.Email);
if (user == null) 
{
    return BadRequest("Invalid email or password.");
}

var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);
if (!result.Succeeded) 
{
    return BadRequest("Invalid email or password.");
}

```

在上述代码中，我们首先使用 `FindByEmailAsync()` 方法查找电子邮件对应的用户对象，然后调用 `_signInManager.PasswordSignInAsync()` 方法来验证用户输入的密码是否正确。如果验证成功，则返回 JWT 令牌以允许用户访问受保护的资源。

因此，在默认情况下，用户可以使用他们在注册过程中提供的电子邮件地址和密码进行登录。

## 7. 运行应用程序

完成以上步骤后，现在可以运行应用程序并测试 API 接口了。可以使用 Postman 或其他 HTTP 客户端来发送请求，并通过 Swagger UI 来查看 API 文档。

例如，可以使用 POST 请求来注册新用户：以下是使用 Postman 发送 POST 请求的示例：

1. 打开 Postman 应用程序。
2. 选择 “New Request”。
3. 设置请求方法为 “POST”，并将请求 URL 设置为 `http://localhost:5000/api/auth/register`。
4. 在请求正文中添加 JSON 数据，例如：

```
{
    "Email": "test@example.com",
    "Password": "password123",
    "ConfirmPassword": "password123"
}

```

1. 单击 “Send” 按钮发送请求，查看响应结果。

你可以使用类似的方式来测试其他的 API 接口，只需要将请求方法和请求 URL 设置正确，然后添加任何必要的头信息和请求正文即可

----

对于帖子的增删改查评论测试API接口，可以采用以下方式进行测试：

1. 接口测试工具

可以使用接口测试工具对API接口进行测试，例如Postman、Insomnia等工具。通过构造请求和验证响应，对API接口进行测试。

1. 单元测试

在开发过程中，可以采用单元测试的方式对每个API接口进行测试。单元测试是指对软件中的最小可测试部分进行验证和测试，通常只涉及到一个函数或一个模块。通过针对每个API接口编写测试用例，对API进行验证和测试。

1. 集成测试

集成测试是指对系统不同部分之间的交互进行测试，包括对API接口之间的交互进行测试。通过模拟系统实际运行环境，对API接口进行测试。

在API接口测试过程中，需要关注以下几个方面：

1. 请求参数：确保API接口能够正确处理各种请求参数，如参数类型、参数范围、参数缺失等情况。
2. 响应结果：验证API接口返回的结果是否符合预期。
3. 异常情况：测试API接口的异常处理，如网络故障、服务器错误等情况下的响应。
4. 安全性：测试API接口的安全性，防止未授权用户访问API接口。

通过以上测试方式，可以有效保证API接口的质量和稳定性

首先通过 `WebApplication.CreateBuilder` 方法创建了一个 `WebApplicationBuilder` 实例，并将该实例存储在变量 `builder` 中。然后，在调用 `builder.Services.AddDbContext<AppDbContext>` 方法添加 DbContext 服务时，我们使用了 `options.UseSqlServer` 方法来指定使用 SqlServer 数据库，并通过引用 `Microsoft.EntityFrameworkCore.SqlServer` 包来获得该方法的定义

> 引入 Microsoft.EntityFrameworkCore.SqlServer 包 



在一个应用程序中，通常用户（或者说登录账户）是使用 User 类型的实例来表示和管理的。User 类型通常包括一些基本属性，例如用户名、密码、电子邮件地址等，并且通常还包括一些与权限和角色相关的属性，在身份验证和授权过程中起着重要的作用。

在 [ASP.NET](http://asp.net/) Core 应用程序中，可以通过定义 User 类型和 Identity 系统来管理用户身份验证和授权。Identity 系统提供了许多有用的功能，例如用户注册、用户登录、用户注销、密码重置等，同时还支持各种常见的身份验证协议和方案，例如 JWT、OAuth2 等。

在应用程序中，当用户成功登录时，通常会将其凭据信息保存到某个地方，如一个 Cookie 或者 JWT 访问令牌中。接下来，当用户发送请求时，系统会验证该令牌是否有效，并根据令牌中携带的信息进行适当的授权操作。如果用户没有提供有效的凭据信息，那么系统可能会拒绝其请求并返回相应的错误消息。

需要注意的是，User 类型和登录账户之间存在一定的关系，但它们并不是同一个概念。User 类型主要用于表示和管理用户信息，而登录账户则是用户在应用程序中进行身份验证时所使用的凭据信息。这些凭据信息可能包括用户名、密码、电子邮件地址等，具体取决于应用程序的设计和实现

---

以上测试成功了 论坛访问  ,注册登录功能 ;

今天工作就是  要将 调试原有程序,修改 融合到本次的程序中;

