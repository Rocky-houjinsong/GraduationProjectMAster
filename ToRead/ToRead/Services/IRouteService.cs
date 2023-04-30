namespace ToRead.Services;

#region 路由接口

/*
 * 将导航中 开发者角度的 页面跳转逻辑  转化为 用户角度的 使用逻辑
 *
 * 涉及到  导航栈的知识内容
 */

#endregion

/// <summary>
/// 路由接口
/// </summary>
public interface IRouteService
{
    string GetRoute(string pageKey);
}