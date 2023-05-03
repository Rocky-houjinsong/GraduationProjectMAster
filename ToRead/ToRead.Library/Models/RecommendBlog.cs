namespace ToRead.Library.Models;

/// <summary>
/// 今日推荐文章类
/// </summary>
//TODO 待修改,简单
public class RecommendBlog
{
    /*#region 接口层级

    public string Name { get; set; }
    public string Content { get; set; }

    /// <summary>
    /// 来源.
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// 发布者
    /// </summary>
    public string Publisher { get; set; }

    /// <summary>
    /// 标签组.
    /// </summary>
    public string[] Tags { get; set; }

    /// <summary>
    /// 是否原创,true为是
    /// </summary>
    public bool Originality { get; set; }

    #endregion*/

    public string Snippet { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Dynasty { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;
}