﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToRead.Library.Services;

namespace ToRead.Library.ViewModels;

/// <summary>
/// 初始化页面VM类
/// </summary>
// TODO 基础框架是有了,需要重构 调教 , 中等难度
public class InitializationPageViewModel : ObservableObject
{
    private IToReadItemStorage _toreadStorage;

    private IToReadFavoriteStorage _favoriteStorage;

    private IInitializationNavigationService _initializationNavigationService;

    public InitializationPageViewModel(IToReadItemStorage toreadStorage,
        IToReadFavoriteStorage favoriteStorage,
        IInitializationNavigationService initializationNavigationService)
    {
        _toreadStorage = toreadStorage;
        _favoriteStorage = favoriteStorage;
        _initializationNavigationService = initializationNavigationService;
        _lazyLoadedCommand =
            new Lazy<AsyncRelayCommand>(
                new AsyncRelayCommand(LoadedCommandFunction));
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    private string _status = string.Empty;

    public AsyncRelayCommand LoadedCommand => _lazyLoadedCommand.Value;

    private Lazy<AsyncRelayCommand> _lazyLoadedCommand;

    public async Task LoadedCommandFunction()
    {
        if (!_toreadStorage.IsInitialized)
        {
            Status = "正在初始化待读数据库";
            await _toreadStorage.InitializeAsync();
        }

        if (!_favoriteStorage.IsInitialized)
        {
            Status = "正在初始化收藏数据库";
            await _favoriteStorage.InitializeAsync();
        }

        Status = "所有初始化已完成";
        await Task.Delay(1000);

        _initializationNavigationService.NavigateToAppShell();
    }
}