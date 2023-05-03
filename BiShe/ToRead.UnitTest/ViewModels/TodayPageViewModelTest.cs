using ToRead.Models;
using ToRead.Services;
using ToRead.ViewModels;
using Moq;
using Xunit;

namespace ToRead.UnitTest.ViewModels;

public class TodayPageViewModelTest {
    [Fact]
    public async Task LoadedCommandFunction_ImageUpdated() {
        var oldTodayImageToReturn = new TodayImage();
        var newTodayImageToReturn = new TodayImage();
        var updateResultToReturn = new TodayImageServiceCheckUpdateResult {
            HasUpdate = true, TodayImage = newTodayImageToReturn
        };

        var todayImageServiceMock = new Mock<ITodayImageService>();
        todayImageServiceMock.Setup(p => p.GetTodayImageAsync())
            .ReturnsAsync(oldTodayImageToReturn);
        todayImageServiceMock.Setup(p => p.CheckUpdateAsync())
            .ReturnsAsync(updateResultToReturn);
        var mockImageService = todayImageServiceMock.Object;

        var todayPoetryToReturn = new TodayPoetry();
        var todayPoetryServiceMock = new Mock<ITodayPoetryService>();
        todayPoetryServiceMock.Setup(p => p.GetTodayPoetryAsync())
            .ReturnsAsync(todayPoetryToReturn);
        var mockTodayPoetryService = todayPoetryServiceMock.Object;

        var todayPageViewModel = new TodayPageViewModel(mockImageService,
            mockTodayPoetryService, null, null, null);
        var todayImageList = new List<TodayImage>();
        var isLoadingList = new List<bool>();
        todayPageViewModel.PropertyChanged += (sender, args) => {
            switch (args.PropertyName) {
                case nameof(TodayPageViewModel.TodayImage):
                    todayImageList.Add(todayPageViewModel.TodayImage);
                    break;
                case nameof(TodayPageViewModel.IsLoading):
                    isLoadingList.Add(todayPageViewModel.IsLoading);
                    break;
            }
        };

        todayPageViewModel.LoadedCommandFunction();
        while (todayImageList.Count != 2 || isLoadingList.Count != 2) {
            await Task.Delay(100);
        }

        Assert.Same(oldTodayImageToReturn, todayImageList[0]);
        Assert.Same(newTodayImageToReturn, todayImageList[1]);
        Assert.True(isLoadingList[0]);
        Assert.False(isLoadingList[1]);
        Assert.Same(todayPoetryToReturn, todayPageViewModel.TodayPoetry);

        todayImageServiceMock.Verify(p => p.GetTodayImageAsync(), Times.Once);
        todayImageServiceMock.Verify(p => p.CheckUpdateAsync(), Times.Once);
        todayPoetryServiceMock.Verify(p => p.GetTodayPoetryAsync(), Times.Once);
    }

    [Fact]
    public async Task LoadedCommandFunction_ImageNotUpdated() {
        var oldTodayImageToReturn = new TodayImage();
        var updateResultToReturn = new TodayImageServiceCheckUpdateResult {
            HasUpdate = false
        };

        var todayImageServiceMock = new Mock<ITodayImageService>();
        todayImageServiceMock.Setup(p => p.GetTodayImageAsync())
            .ReturnsAsync(oldTodayImageToReturn);
        todayImageServiceMock.Setup(p => p.CheckUpdateAsync())
            .ReturnsAsync(updateResultToReturn);
        var mockImageService = todayImageServiceMock.Object;

        var todayPoetryServiceMock = new Mock<ITodayPoetryService>();
        var mockTodayPoetryService = todayPoetryServiceMock.Object;

        var todayPageViewModel = new TodayPageViewModel(mockImageService,
            mockTodayPoetryService, null, null, null);
        var todayImageList = new List<TodayImage>();
        todayPageViewModel.PropertyChanged += (sender, args) => {
            switch (args.PropertyName) {
                case nameof(TodayPageViewModel.TodayImage):
                    todayImageList.Add(todayPageViewModel.TodayImage);
                    break;
            }
        };

        todayPageViewModel.LoadedCommandFunction();
        while (todayImageList.Count != 1) {
            await Task.Delay(100);
        }

        Assert.Same(oldTodayImageToReturn, todayImageList[0]);

        todayImageServiceMock.Verify(p => p.GetTodayImageAsync(), Times.Once);
        todayImageServiceMock.Verify(p => p.CheckUpdateAsync(), Times.Once);
        todayPoetryServiceMock.Verify(p => p.GetTodayPoetryAsync(), Times.Once);
    }

    [Fact]
    public async Task
        JinrishiciCommandFunction_CopyrightCommandFunction_Default() {
        var browserServiceMock = new Mock<IBrowserService>();
        var mockBrowserService = browserServiceMock.Object;

        var todayPageViewModel = new TodayPageViewModel(null, null, null,
            null, mockBrowserService);
        await todayPageViewModel.JinrishiciCommandFunction();
        browserServiceMock.Verify(
            p => p.OpenAsync("https://www.jinrishici.com"), Times.Once);

        var link = "http://no.such.url";
        var todayImage = new TodayImage { CopyrightLink = link };
        todayPageViewModel.TodayImage = todayImage;

        await todayPageViewModel.CopyrightCommandFunction();
        browserServiceMock.Verify(p => p.OpenAsync(link), Times.Once);
    }

    [Fact]
    public async Task ShowDetailCommandFunction_Default() {
        var contentNavigationServiceMock =
            new Mock<IContentNavigationService>();
        var mockContentNavigationService = contentNavigationServiceMock.Object;

        var todayPageViewModel = new TodayPageViewModel(null, null,
            mockContentNavigationService, null, null);
        await todayPageViewModel.ShowDetailCommandFunction();
        contentNavigationServiceMock.Verify(
            p => p.NavigateToAsync(ContentNavigationConstant.TodayDetailPage),
            Times.Once);
    }

    public async Task QueryCommandFunction_Default() {
        object parameter = null;
        var rootNavigationServiceMock = new Mock<IRootNavigationService>();
        rootNavigationServiceMock
            .Setup(p => p.NavigateToAsync(RootNavigationConstant.QueryPage,
                It.IsAny<object>()))
            .Callback<string, object>((s, o) => parameter = o);
        var mockRootNavigationService = rootNavigationServiceMock.Object;

        var todayPoetry = new TodayPoetry { Name = "小重山", Author = "张良能" };

        var todayPageViewModel = new TodayPageViewModel(null, null, null,
            mockRootNavigationService, null);
        todayPageViewModel.TodayPoetry = todayPoetry;
        await todayPageViewModel.QueryCommandFunction();

        Assert.IsType<PoetryQuery>(parameter);
        var poetryQuery = (PoetryQuery)parameter;
        Assert.Equal(todayPoetry.Name, poetryQuery.Name);
        Assert.Equal(todayPoetry.Author, poetryQuery.Author);
    }
}