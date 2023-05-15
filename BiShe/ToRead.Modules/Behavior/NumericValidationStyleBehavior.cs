namespace ToRead.Modules.Behavior;

public class NumericValidationStyleBehavior : Behavior<Entry>
{
    #region 通过样式使用 .NET MAUI 行为

    public static readonly BindableProperty AttachBehaviorProperty =
        BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(NumericValidationStyleBehavior),
            false, propertyChanged: OnAttachBehaviorChanged);

    public static bool GetAttachBehavior(BindableObject view)
    {
        return (bool)view.GetValue(AttachBehaviorProperty);
    }

    public static void SetAttachBehavior(BindableObject view, bool value)
    {
        view.SetValue(AttachBehaviorProperty, value);
    }

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
            entry.Behaviors.Add(new NumericValidationStyleBehavior());
        }
        else
        {
            Microsoft.Maui.Controls.Behavior toRemove =
                entry.Behaviors.FirstOrDefault(b => b is NumericValidationStyleBehavior);
            if (toRemove != null)
            {
                entry.Behaviors.Remove(toRemove);
            }
        }
    }

    #endregion


    #region .NET MAUI Behaviors 部分

    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnEntryTextChanged;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        //默认方法
        entry.TextChanged -= OnEntryTextChanged;
        base.OnDetachingFrom(entry);
        //方法一
        Microsoft.Maui.Controls.Behavior toRemove =
            entry.Behaviors.FirstOrDefault(b => b is NumericValidationStyleBehavior);
        if (toRemove != null)
        {
            entry.Behaviors.Remove(toRemove);
        }

        // 方法二
        entry.Behaviors.Clear();
    }

    void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
        double result;
        bool isValid = double.TryParse(args.NewTextValue, out result);
        ((Entry)sender).TextColor = isValid ? Colors.Black : Colors.Red;
    }

    #endregion
}