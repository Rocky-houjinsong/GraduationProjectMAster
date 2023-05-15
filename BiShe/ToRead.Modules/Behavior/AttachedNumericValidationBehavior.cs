namespace ToRead.Modules.Behavior;

/// <summary>
/// 不合法数字判定
/// </summary>
public static class AttachedNumericValidationBehavior
{
    //名为 AttachBehavior 的附加属性
    public static readonly BindableProperty AttachBehaviorProperty =
        BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(AttachedNumericValidationBehavior),
            false, propertyChanged: OnAttachBehaviorChanged);

    public static bool GetAttachBehavior(BindableObject view)
    {
        return (bool)view.GetValue(AttachBehaviorProperty);
    }

    public static void SetAttachBehavior(BindableObject view, bool value)
    {
        view.SetValue(AttachBehaviorProperty, value);
    }

    //该附加属性注册属性值更改时执行的 OnAttachBehaviorChanged 方法
    static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
    {
        Entry entry = view as Entry;
        if (entry == null)
        {
            return;
        }

        bool attachBehavior = (bool)newValue;
        if (attachBehavior)
        {
            entry.TextChanged += OnEntryTextChanged;
        }
        else
        {
            entry.TextChanged -= OnEntryTextChanged;
        }
    }

    /// <summary>
    /// 行为的核心功能由 OnEntryTextChanged 方法提供，
    /// 该方法分析 在 中Entry输入的值，如果值不是 double，则将 属性设置为TextColor红色。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
        double result;
        bool isValid = double.TryParse(args.NewTextValue, out result);
        ((Entry)sender).TextColor = isValid ? Colors.Black : Colors.Red;
    }
}