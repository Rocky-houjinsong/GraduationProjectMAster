using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace ToRead.Library.Models;

#region 收藏类作用

/*
 * 此处的收藏是 针对 论坛中他人的 待读清单项目, 推荐页中的内容进行收藏;
 * 而 使用者本人的编写的待读清单 的`收藏` 仅为 重要状态 ;
 *
 * 为何做区分 ? 收藏的目的是 日后观看 ,可能加入到 自己的待读清单中  可能是吃灰 ,只收藏不观看 
 * 重要状态 是 在自己的待读清单中做划分,
 * 所以说 ,需要 先收藏,才能做 重要状态的划分 ;
 */

#endregion

//TODO 修改 数据库   判定 是否收藏就行了 ,不要很复杂  简单  直接部署
[Table("toreadfavorite")]
public class ToReadFavorite : ObservableObject
{
    /// <summary>
    /// 待读清单Id,主键
    /// </summary>
    [Column("itemid")]
    public int ToReadItemId { get; set; }

    private bool _isFavorite;

    [Column("isfavorite")]
    public virtual bool IsFavorite
    {
        get => _isFavorite;
        set => SetProperty(ref _isFavorite, value);
    }

    public long Timestamp { get; set; }
}