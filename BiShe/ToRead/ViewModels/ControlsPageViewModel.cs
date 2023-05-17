using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace ToRead.ViewModels;

public class ControlsPageViewModel : ObservableObject
{
    /// <summary>
    /// 结果
    /// </summary>
    private string _result;

    /// <summary>
    /// 提供机制进行数据的处理
    /// </summary>
    public string Result
    {
        get => _result;
        set => SetProperty(ref _result, value);
    }

    /// <summary>
    /// Hello命令
    /// 通过 OpenSilver.MvvmLightLibs进行导入,是最原始的MVVM创建方式
    /// </summary>
    private RelayCommand _helloCommand;

    public RelayCommand HelloCommand => _helloCommand ?? new RelayCommand(() => Result = "HelloWorld!");
    //后续需要将 View 和ViewModel 关联起来
}