using ToRead.Models;
using ToRead.Services;
using ToRead.ViewModels;
using Moq;
using Xunit;

namespace ToRead.UnitTest.ViewModels;

public class DetailPageViewModelTest {
    [Fact]
    public async Task NavigatedToCommandFunction_Default() {
        var poetry = new Poetry {Id = 0};
        var favoriteToReturn =
            new Favorite {PoetryId = poetry.Id, IsFavorite = true};

        var favoriteStorageMock = new Mock<IFavoriteStorage>();
        favoriteStorageMock
            .Setup(p => p.GetFavoriteAsync(favoriteToReturn.PoetryId))
            .ReturnsAsync(favoriteToReturn);
        var mockFavoriteStorage = favoriteStorageMock.Object;

        var detailPageViewModel = new DetailPageViewModel(mockFavoriteStorage);
        detailPageViewModel.Poetry = poetry;

        var loadingList = new List<bool>();
        detailPageViewModel.PropertyChanged += (sender, args) => {
            if (args.PropertyName == nameof(DetailPageViewModel.IsLoading)) {
                loadingList.Add(detailPageViewModel.IsLoading);
            }
        };

        await detailPageViewModel.NavigatedToCommandFunction();

        favoriteStorageMock.Verify(
            p => p.GetFavoriteAsync(favoriteToReturn.PoetryId), Times.Once);
        Assert.Same(favoriteToReturn, detailPageViewModel.Favorite);
        Assert.True(detailPageViewModel.IsFavorite);
        Assert.Equal(2, loadingList.Count);
        Assert.True(loadingList.First());
        Assert.False(loadingList.Last());

        await detailPageViewModel.FavoriteToggledCommandFunction();
        favoriteStorageMock.Verify(
            p => p.GetFavoriteAsync(favoriteToReturn.PoetryId), Times.Once);
        Assert.Same(favoriteToReturn, detailPageViewModel.Favorite);
        Assert.True(detailPageViewModel.IsFavorite);
        Assert.Equal(2, loadingList.Count);
    }

    [Fact]
    public async Task FavoriteToggledCommandFunction_Default() {
        var favorite = new Favorite {PoetryId = 0, IsFavorite = true};

        var favoriteStorageMock = new Mock<IFavoriteStorage>();
        var mockFavoriteStorage = favoriteStorageMock.Object;

        var detailPageViewModel = new DetailPageViewModel(mockFavoriteStorage);

        var loadingList = new List<bool>();
        detailPageViewModel.PropertyChanged += (sender, args) => {
            if (args.PropertyName == nameof(DetailPageViewModel.IsLoading)) {
                loadingList.Add(detailPageViewModel.IsLoading);
            }
        };

        detailPageViewModel.Favorite = favorite;
        detailPageViewModel.IsFavorite = favorite.IsFavorite;

        await detailPageViewModel.FavoriteToggledCommandFunction();
        Assert.Equal(0, loadingList.Count);
        favoriteStorageMock.Verify(
            p => p.SaveFavoriteAsync(It.IsAny<Favorite>()), Times.Never);

        detailPageViewModel.IsFavorite = !detailPageViewModel.IsFavorite;
        await detailPageViewModel.FavoriteToggledCommandFunction();

        Assert.Equal(2, loadingList.Count);
        Assert.True(loadingList.First());
        Assert.False(loadingList.Last());
        favoriteStorageMock.Verify(p => p.SaveFavoriteAsync(favorite),
            Times.Once);
    }
}