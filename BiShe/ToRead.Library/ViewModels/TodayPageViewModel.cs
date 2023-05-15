using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeviceAPI.Services;
using MvvmHelpers.Commands;
using ToRead.Models;
using ToRead.Services;
using Xamarin.Essentials;

namespace ToRead.ViewModels;

public class TodayPageViewModel : ObservableObject
{
    private ITodayImageService _todayImageService;

    private ITodayPoetryService _todayPoetryService;

    private IContentNavigationService _contentNavigationService;

    private IRootNavigationService _rootNavigationService;

    private IBrowserService _browserService;

    private TodayImage? _todayImage;

    private TodayPoetry? _todayPoetry;

    //-----下载功能 需要与 Poetry类交互,调用PoetryStroage类的AddPoetryAsync方法
    /*private readonly IPreferenceStorage _preferenceStorage;
    private PoetryStorage _poetryStorage;*/
    private bool _isLoading;

    public TodayPageViewModel(ITodayImageService todayImageService,
        ITodayPoetryService todayPoetryService,
        IContentNavigationService contentNavigationService,
        IRootNavigationService rootNavigationService,
        IBrowserService browserService)
    {
        _todayImageService = todayImageService;
        _todayPoetryService = todayPoetryService;
        _contentNavigationService = contentNavigationService;
        _rootNavigationService = rootNavigationService;
        _browserService = browserService;
        _lazyLoadedCommand =
            new Lazy<RelayCommand>(new RelayCommand(LoadedCommandFunction));
        _lazyShowDetailCommand =
            new Lazy<AsyncRelayCommand>(
                new AsyncRelayCommand(ShowDetailCommandFunction));
        _lazyJinrishiciCommand =
            new Lazy<AsyncRelayCommand>(
                new AsyncRelayCommand(JinrishiciCommandFunction));
        _lazyCopyrightCommand =
            new Lazy<AsyncRelayCommand>(
                new AsyncRelayCommand(CopyrightCommandFunction));
        _lazyQueryCommand =
            new Lazy<AsyncRelayCommand>(
                new AsyncRelayCommand(QueryCommandFunction));
        /*_lazyInsertCommand =
            new Lazy<AsyncRelayCommand>(
                new AsyncRelayCommand(ExecuteInsertCommand));*/

        ShareCommand = new Command(ExecuteShareCommand);
        //DownloadCommand = new Command(ExecuteInsertCommand);
    }

    public TodayImage? TodayImage
    {
        get => _todayImage;
        set => SetProperty(ref _todayImage, value);
    }

    public TodayPoetry? TodayPoetry
    {
        get => _todayPoetry;
        set => SetProperty(ref _todayPoetry, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public RelayCommand LoadedCommand => _lazyLoadedCommand.Value;

    private readonly Lazy<RelayCommand> _lazyLoadedCommand;

    public void LoadedCommandFunction()
    {
        Task.Run(async () =>
        {
            TodayImage = await _todayImageService.GetTodayImageAsync();

            var updateResult = await _todayImageService.CheckUpdateAsync();
            if (updateResult.HasUpdate)
            {
                TodayImage = updateResult.TodayImage;
            }
        });

        Task.Run(async () =>
        {
            IsLoading = true;
            TodayPoetry = await _todayPoetryService.GetTodayPoetryAsync();
            IsLoading = false;
            _objectValue = "https://www.jinrishici.com";
        });
    }

    public AsyncRelayCommand ShowDetailCommand => _lazyShowDetailCommand.Value;

    private readonly Lazy<AsyncRelayCommand> _lazyShowDetailCommand;

    public async Task ShowDetailCommandFunction() =>
        await _contentNavigationService.NavigateToAsync(
            ContentNavigationConstant.TodayDetailPage);


    public AsyncRelayCommand JinrishiciCommand => _lazyJinrishiciCommand.Value;

    private readonly Lazy<AsyncRelayCommand> _lazyJinrishiciCommand;

    public async Task JinrishiciCommandFunction() =>
        await _browserService.OpenAsync("https://www.jinrishici.com");

    public AsyncRelayCommand CopyrightCommand => _lazyCopyrightCommand.Value;

    private readonly Lazy<AsyncRelayCommand> _lazyCopyrightCommand;

    public async Task CopyrightCommandFunction() =>
        await _browserService.OpenAsync(TodayImage.CopyrightLink);

    public AsyncRelayCommand QueryCommand => _lazyQueryCommand.Value;

    private readonly Lazy<AsyncRelayCommand> _lazyQueryCommand;

    //---- 添加下载的Command
    /*public AsyncRelayCommand InsertCommand => _lazyInsertCommand.Value;

    private readonly Lazy<AsyncRelayCommand> _lazyInsertCommand;*/

    public async Task QueryCommandFunction() =>
        await _rootNavigationService.NavigateToAsync(
            RootNavigationConstant.QueryPage,
            new PoetryQuery
            {
                Author = TodayPoetry.Author, Name = TodayPoetry.Name
            });


    // -------------- 2023年5月16日01:43:27 Command添加 

    private object _objectValue;

    public object ObjectValue
    {
        get => _objectValue;
        set => SetProperty(ref _objectValue, value);
    }

    public Command ShareCommand { get; }

    private async void ExecuteShareCommand()
    {
        if (ObjectValue != null)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = ObjectValue.ToString(),
                Title = "Share Object Value"
            });
        }
    }

    /*public Command DownloadCommand { get; }

    public async void ExecuteInsertCommand(object o)
    {
        await _poetryStorage.AddPoetryAsync(_todayPoetry);
    }*/
}