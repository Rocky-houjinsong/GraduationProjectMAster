using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToReadAPI.Controllers;

//编写代码来处理用户请求以及协调模型和视图之间的交互
//在控制器类中定义一些动作方法来处理HTTP GET或POST请求，并使用模型来从数据库或其他数据源中检索数据。在这些动作方法中，您可以调用视图来呈现响应，并将数据传递给视图以供显示
public class ForumController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}