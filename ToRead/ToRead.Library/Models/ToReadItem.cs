using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace ToRead.Library.Models;

#region 注释内容

/*
 * 使用嵌入式数据库SQLite的ORM工具 CodeFirst 生成数据库;
 * 待读清单单元项目的基础字段 
 */

#endregion

[Table("toreadlist")]
public class ToReadItem : ObservableObject
{
    /// <summary>
    /// 待读清单Id,主键
    /// </summary>
    [Column("itemid")]
    public int ToReadItemId { get; set; }

    #region 内容主体

    /// <summary>
    /// 标题,待读清单.
    /// </summary>
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    [Column("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 预览字段.私有
    /// </summary>
    private string? _snippet;

    /// <summary>
    /// 预览,内容
    /// </summary>
    [SQLite.Ignore]
    public string Snippet => _snippet ?? Content.Split('.')[0].Replace("\r\n", "");

    /// <summary>
    /// 来源
    /// </summary>
    [Column("source")]
    public string Source { get; set; } = string.Empty;

    #endregion

    #region 状态字段

    /// <summary>
    /// 重要状态, true为重要
    /// </summary>
    [Column("prominence")]
    public bool IsProminence { get; set; } = false;

    /// <summary>
    /// 完成状态,true为完成
    /// </summary>
    [Column("accomplish")]
    public bool IsAccomplish { get; set; } = false;

    /// <summary>
    /// 删除状态. true为已删除
    /// </summary>
    [Column("isdelete")]
    public bool IsDelete { get; set; } = false;

    /// <summary>
    /// 作废状态. true为已作废
    /// </summary>
    [Column("invalid")]
    public bool IsInvalid { get; set; } = false;

    #endregion
}