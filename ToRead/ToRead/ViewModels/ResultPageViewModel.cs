using System.Linq.Expressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheSalLab.MauiInfiniteScrolling;
using ToRead.Constants;
using ToRead.Library.Models;
using ToRead.Library.Services;

namespace ToRead.ViewModels;

public class ResultPageViewModel : ObservableObject, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query) =>
        Where = (Expression<Func<ToReadItem, bool>>)query["parameter"];

    private IContentNavigationService _contentNavigationService;

    public ResultPageViewModel(IToReadItemStorage poetryStorage,
        IContentNavigationService contentNavigationService)
    {
        _contentNavigationService = contentNavigationService;
        _lazyNavigatedToCommand =
            new Lazy<AsyncRelayCommand>(
                new AsyncRelayCommand(NavigatedToCommandFunction));
        _lazyPoetryTappedCommand =
            new Lazy<AsyncRelayCommand<ToReadItem>>(
                new AsyncRelayCommand<ToReadItem>(PoetryTappedCommandFunction));

        PoetryCollection = new MauiInfiniteScrollCollection<ToReadItem>
        {
            OnCanLoadMore = () => _canLoadMore,
            OnLoadMore = async () =>
            {
                Status = Loading;
                var poetries = (await poetryStorage.GetPoetriesAsync(Where,
                    PoetryCollection.Count, PageSize)).ToList();
                Status = "";

                if (poetries.Count < PageSize)
                {
                    _canLoadMore = false;
                    Status = NoMoreResult;
                }

                if (PoetryCollection.Count == 0 && poetries.Count == 0)
                {
                    Status = NoResult;
                }

                return poetries;
            }
        };
    }

    public MauiInfiniteScrollCollection<ToReadItem> PoetryCollection { get; }

    private bool _canLoadMore;

    public const int PageSize = 20;

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    private string _status;

    public const string Loading = "正在载入";

    public const string NoResult = "没有满足条件的结果";

    public const string NoMoreResult = "没有更多结果";

    public Expression<Func<ToReadItem, bool>> Where
    {
        get => _where;
        set => _isNewQuery = SetProperty(ref _where, value);
    }

    private Expression<Func<ToReadItem, bool>> _where;

    private bool _isNewQuery;

    public AsyncRelayCommand NavigatedToCommand =>
        _lazyNavigatedToCommand.Value;

    private Lazy<AsyncRelayCommand> _lazyNavigatedToCommand;

    public async Task NavigatedToCommandFunction()
    {
        if (!_isNewQuery)
        {
            return;
        }

        _isNewQuery = false;

        PoetryCollection.Clear();
        _canLoadMore = true;
        await PoetryCollection.LoadMoreAsync();
    }

    public AsyncRelayCommand<ToReadItem> PoetryTappedCommand =>
        _lazyPoetryTappedCommand.Value;

    private Lazy<AsyncRelayCommand<ToReadItem>> _lazyPoetryTappedCommand;

    public async Task PoetryTappedCommandFunction(ToReadItem poetry) =>
        await _contentNavigationService.NavigateToAsync(
            ContentNavigationConstant.DetailPage, poetry);
}