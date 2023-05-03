using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using ToRead.Library.Models;
using ToRead.Library.Services;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace ToRead.Library.ViewModels;

public class FavoritePageViewModel : ObservableObject
{
    private IFavoriteStorage _favoriteStorage;

    private IToReadItemStorage _poetryStorage;

    IContentNavigationService _contentNavigationService;

    public FavoritePageViewModel(IFavoriteStorage favoriteStorage,
        IToReadItemStorage poetryStorage,
        IContentNavigationService contentNavigationService)
    {
        _favoriteStorage = favoriteStorage;
        _poetryStorage = poetryStorage;
        _contentNavigationService = contentNavigationService;

        _favoriteStorage.Updated += FavoriteStorageOnUpdated;

        _lazyLoadedCommand =
            new Lazy<AsyncRelayCommand>(
                new AsyncRelayCommand(LoadedCommandFunction));
        _layzPoetryTappedCommand = new Lazy<AsyncRelayCommand<ToReadItemFavorite>>(
            new AsyncRelayCommand<ToReadItemFavorite>(PoetryTappedCommandFunction));
    }

    public ObservableRangeCollection<ToReadItemFavorite> PoetryFavoriteCollection { get; } = new();

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private bool _isLoading;

    public AsyncRelayCommand LoadedCommand => _lazyLoadedCommand.Value;

    private Lazy<AsyncRelayCommand> _lazyLoadedCommand;

    public async Task LoadedCommandFunction()
    {
        IsLoading = true;

        PoetryFavoriteCollection.Clear();
        var favoriteList = await _favoriteStorage.GetFavoritesAsync();

        PoetryFavoriteCollection.AddRange((await Task.WhenAll(
            favoriteList.Select(p => Task.Run(async () => new ToReadItemFavorite
            {
                Poetry = await _poetryStorage.GetToReadItemAsync(p.PoetryId),
                Favorite = p
            })))).ToList());

        IsLoading = false;
    }

    public AsyncRelayCommand<ToReadItemFavorite> PoetryTappedCommand =>
        _layzPoetryTappedCommand.Value;

    private Lazy<AsyncRelayCommand<ToReadItemFavorite>> _layzPoetryTappedCommand;

    public async Task
        PoetryTappedCommandFunction(ToReadItemFavorite poetryFavorite) =>
        await _contentNavigationService.NavigateToAsync(
            ContentNavigationConstant.FavoriteDetailPage,
            poetryFavorite.Poetry);


    private async void FavoriteStorageOnUpdated(object? sender,
        FavoriteStorageUpdatedEventArgs e)
    {
        var favorite = e.UpdatedFavorite;
        PoetryFavoriteCollection.Remove(
            PoetryFavoriteCollection.FirstOrDefault(p =>
                p.Favorite.PoetryId == favorite.PoetryId));

        if (!favorite.IsFavorite)
        {
            return;
        }

        var poetryFavorite = new ToReadItemFavorite
        {
            Poetry = await _poetryStorage.GetToReadItemAsync(favorite.PoetryId),
            Favorite = favorite
        };

        var index = PoetryFavoriteCollection.IndexOf(
            PoetryFavoriteCollection.FirstOrDefault(p =>
                p.Favorite.Timestamp < favorite.Timestamp));
        if (index < 0)
        {
            index = PoetryFavoriteCollection.Count;
        }

        PoetryFavoriteCollection.Insert(index, poetryFavorite);
    }
}