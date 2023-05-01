using SQLite;

namespace ToRead.Library.Models;

/// <summary>
/// 待读标签
/// </summary>
/// TODO 需要修改  简单级
[Table("toreadtag")]
public class ToReadTag
{
    [Column("tagid")] public int ToReadTagId { get; set; }
    [Column("itemid")] public string ItemId { get; set; }
    [Column("tagname")] public int TagName { get; set; }
}