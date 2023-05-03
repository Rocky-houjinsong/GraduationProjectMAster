using Microsoft.AspNetCore.Identity;

namespace ToRead.RESTAPI.Model
{
    /// <summary>
    /// 用户.
    /// </summary>
    /// <remarks>每个用户包含一个或多个Post</remarks>
    //确保 "ToRead.RESTAPI.Model.User" 类型继承自 "Microsoft.AspNetCore.Identity.IdentityUser" 类型
    public class User : IdentityUser
    {
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}