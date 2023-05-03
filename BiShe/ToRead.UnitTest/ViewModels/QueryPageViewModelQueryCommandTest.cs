using ToRead.Models;
using ToRead.Services;
using ToRead.UnitTest.Helpers;
using ToRead.ViewModels;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace ToRead.UnitTest.ViewModels;

public class QueryPageViewModelQueryCommandTest : IDisposable {
    public QueryPageViewModelQueryCommandTest() =>
        PoetryStorageHelper.RemoveDatabaseFile();


    public void Dispose() => PoetryStorageHelper.RemoveDatabaseFile();

    [Fact]
    public async Task QueryCommandFunction_Default() {
        var parameter = new object();
        var contentNavigationServiceMock =
            new Mock<IContentNavigationService>();
        contentNavigationServiceMock
            .Setup(p => p.NavigateToAsync(ContentNavigationConstant.ResultPage,
                It.IsAny<object>()))
            .Callback<string, object>((s, o) => parameter = o);
        var mockContentNavigationService = contentNavigationServiceMock.Object;

        var queryPageViewModel =
            new QueryPageViewModel(mockContentNavigationService);
        queryPageViewModel.FilterViewModelCollection.First()
            .AddCommandFunction();
        queryPageViewModel.FilterViewModelCollection[0].Type =
            FilterType.AuthorNameFilter;
        queryPageViewModel.FilterViewModelCollection[0].Content = "苏轼";
        queryPageViewModel.FilterViewModelCollection[1].Type =
            FilterType.ContentFilter;
        queryPageViewModel.FilterViewModelCollection[1].Content = "山";
        await queryPageViewModel.QueryCommandFunction();

        Assert.IsAssignableFrom<Expression<Func<Poetry, bool>>>(parameter);

        var poetryStorage =
            await PoetryStorageHelper.GetInitializedPoetryStorage();
        var poetryList = await poetryStorage.GetPoetriesAsync(
            (Expression<Func<Poetry, bool>>) parameter, 0, Int32.MaxValue);
        Assert.Equal(2, poetryList.Count());
        await poetryStorage.CloseAsync();
    }
}