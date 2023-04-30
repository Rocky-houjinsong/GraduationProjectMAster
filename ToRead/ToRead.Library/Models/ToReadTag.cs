using SQLite;

namespace ToRead.Library.Models;

[Table("toreadtag")]
public class ToReadTag
{
    [Column("tagid")] public int ToReadTagId { get; set; }
    [Column("itemid")] public string ItemId { get; set; }
    [Column("tagname")] public int TagName { get; set; }
}