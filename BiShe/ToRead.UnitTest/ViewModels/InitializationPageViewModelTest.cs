using ToRead.Services;
using ToRead.ViewModels;
using Moq;
using Xunit;

namespace ToRead.UnitTest.ViewModels;

public class InitializationPageViewModelTest {
    [Fact]
    public async Task LoadedCommandFunction_NotInitialized() {
        var poetryStorageMock = new Mock<IPoetryStorage>();
        poetryStorageMock.Setup(p => p.IsInitialized).Returns(false);
        var mockPoetryStorage = poetryStorageMock.Object;

        var favoriteStorageMock = new Mock<IFavoriteStorage>();
        favoriteStorageMock.Setup(p => p.IsInitialized).Returns(false);
        var mockFavoriteStorage = favoriteStorageMock.Object;

        var initializationNavigationServiceMock =
            new Mock<IInitializationNavigationService>();
        var mockInitializationNavigationService =
            initializationNavigationServiceMock.Object;

        var initializationPageViewModel = new InitializationPageViewModel(
            mockPoetryStorage, mockFavoriteStorage,
            mockInitializationNavigationService);

        var statusList = new List<string>();
        initializationPageViewModel.PropertyChanged += (sender, args) => {
            if (args.PropertyName ==
                nameof(InitializationPageViewModel.Status)) {
                statusList.Add(initializationPageViewModel.Status);
            }
        };

        await initializationPageViewModel.LoadedCommandFunction();
        poetryStorageMock.Verify(p => p.IsInitialized, Times.Once);
        poetryStorageMock.Verify(p => p.InitializeAsync(), Times.Once);
        favoriteStorageMock.Verify(p => p.IsInitialized, Times.Once);
        favoriteStorageMock.Verify(p => p.InitializeAsync(), Times.Once);
        initializationNavigationServiceMock.Verify(p => p.NavigateToAppShell(),
            Times.Once);
        Assert.Equal(3, statusList.Count);
    }

    [Fact]
    public async Task LoadedCommandFunction_Initialized() {
        var poetryStorageMock = new Mock<IPoetryStorage>();
        poetryStorageMock.Setup(p => p.IsInitialized).Returns(true);
        var mockPoetryStorage = poetryStorageMock.Object;

        var favoriteStorageMock = new Mock<IFavoriteStorage>();
        favoriteStorageMock.Setup(p => p.IsInitialized).Returns(true);
        var mockFavoriteStorage = favoriteStorageMock.Object;

        var initializationNavigationServiceMock =
            new Mock<IInitializationNavigationService>();
        var mockInitializationNavigationService =
            initializationNavigationServiceMock.Object;


        var initializationPageViewModel = new InitializationPageViewModel(
            mockPoetryStorage, mockFavoriteStorage,
            mockInitializationNavigationService);

        var statusList = new List<string>();
        initializationPageViewModel.PropertyChanged += (sender, args) => {
            if (args.PropertyName ==
                nameof(InitializationPageViewModel.Status)) {
                statusList.Add(initializationPageViewModel.Status);
            }
        };

        await initializationPageViewModel.LoadedCommandFunction();
        poetryStorageMock.Verify(p => p.IsInitialized, Times.Once);
        poetryStorageMock.Verify(p => p.InitializeAsync(), Times.Never);
        favoriteStorageMock.Verify(p => p.IsInitialized, Times.Once);
        favoriteStorageMock.Verify(p => p.InitializeAsync(), Times.Never);
        initializationNavigationServiceMock.Verify(p => p.NavigateToAppShell(),
            Times.Once);
        Assert.Single(statusList);
    }
}