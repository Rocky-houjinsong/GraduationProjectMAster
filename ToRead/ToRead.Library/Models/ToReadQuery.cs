namespace ToRead.Library.Models;

public class ToReadQuery
{
    /// <summary>
    /// 标题查询
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 标签查询
    /// </summary>
    public string TagName { get; set; }

    /// <summary>
    /// 状态查询
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// 内容查询
    /// </summary>
    public string Content { get; set; }
}