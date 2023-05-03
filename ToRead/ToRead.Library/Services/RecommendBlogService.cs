using Microsoft.Maui.Controls;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using ToRead.Library.Misc;
using ToRead.Library.Models;
using ToRead.Library.RecommendServices;

namespace ToRead.Library.Services;

public class RecommendBlogService : IRecommendBlogService
{
    public static readonly string JinrishiciTokenKey =
        $"{nameof(RecommendBlogService)}.Token";

    private readonly IAlertService _alertService;

    private readonly IPreferenceStorage _preferenceStorage;

    private readonly IToReadItemStorage _poetryStorage;

    public RecommendBlogService(IAlertService alertService,
        IPreferenceStorage preferenceStorage, IToReadItemStorage poetryStorage)
    {
        _alertService = alertService;
        _preferenceStorage = preferenceStorage;
        _poetryStorage = poetryStorage;
    }

    private string _token = string.Empty;

    private const string Server = "今日诗词服务器";

    public async Task<RecommendBlog> GetRecommendBlogAsync()
    {
        var token = await GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
        {
            return await GetRandomPoetryAsync();
        }

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-User-Token", token);

        HttpResponseMessage response;
        try
        {
            response =
                await httpClient.GetAsync("https://v2.jinrishici.com/sentence");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _alertService.Alert(ErrorMessages.HttpClientErrorTitle,
                ErrorMessages.GetHttpClientError(Server, e.Message),
                ErrorMessages.HttpClientErrorButton);
            return await GetRandomPoetryAsync();
        }

        var json = await response.Content.ReadAsStringAsync();
        JinrishiciSentence jinrishiciSentence;
        try
        {
            jinrishiciSentence = JsonSerializer.Deserialize<JinrishiciSentence>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new JsonException();
        }
        catch (Exception e)
        {
            _alertService.Alert(ErrorMessages.JsonDeserializationErrorTitle,
                ErrorMessages.GetJsonDeserializationError(Server, e.Message),
                ErrorMessages.JsonDeserializationErrorButton);
            return await GetRandomPoetryAsync();
        }

        try
        {
            return new RecommendBlog
            {
                Snippet =
                    jinrishiciSentence.Data?.Content ??
                    throw new JsonException(),
                Name =
                    jinrishiciSentence.Data.Origin?.Title ??
                    throw new JsonException(),
                Dynasty =
                    jinrishiciSentence.Data.Origin.Dynasty ??
                    throw new JsonException(),
                Author =
                    jinrishiciSentence.Data.Origin.Author ??
                    throw new JsonException(),
                Content =
                    string.Join("\n",
                        jinrishiciSentence.Data.Origin.Content ??
                        throw new JsonException()),
                Source = RecommendBlogSources.Jinrishici
            };
        }
        catch (Exception e)
        {
            _alertService.Alert(ErrorMessages.JsonDeserializationErrorTitle,
                ErrorMessages.GetJsonDeserializationError(Server, e.Message),
                ErrorMessages.JsonDeserializationErrorButton);
            return await GetRandomPoetryAsync();
        }
    }

    public async Task<string> GetTokenAsync()
    {
        if (!string.IsNullOrWhiteSpace(_token) || !string.IsNullOrWhiteSpace(
                _token =
                    _preferenceStorage.Get(JinrishiciTokenKey, string.Empty)))
        {
            return _token;
        }

        using var httpClient = new HttpClient();
        HttpResponseMessage response;
        try
        {
            response =
                await httpClient.GetAsync("https://v2.jinrishici.com/token");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _alertService.Alert(ErrorMessages.HttpClientErrorTitle,
                ErrorMessages.GetHttpClientError(Server, e.Message),
                ErrorMessages.HttpClientErrorButton);
            return _token;
        }

        var json = await response.Content.ReadAsStringAsync();
        try
        {
            _token = JsonSerializer.Deserialize<JinrishiciToken>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })?.Data ?? throw new JsonException();
        }
        catch (Exception e)
        {
            _alertService.Alert(ErrorMessages.JsonDeserializationErrorTitle,
                ErrorMessages.GetJsonDeserializationError(Server, e.Message),
                ErrorMessages.JsonDeserializationErrorButton);
            return _token;
        }

        _preferenceStorage.Set(JinrishiciTokenKey, _token);
        return _token;
    }


    public async Task<RecommendBlog> GetRandomPoetryAsync()
    {
        var poetries = await _poetryStorage.GetToReadsAsync(
            Expression.Lambda<Func<ToReadItem, bool>>(Expression.Constant(true),
                Expression.Parameter(typeof(ToReadItem), "p")),
            new Random().Next(ToReadItemStorage.NumberPoetry), 1);
        var poetry = poetries.First();
        return new RecommendBlog
        {
            Snippet = poetry.Snippet,
            Name = poetry.Name,
            Dynasty = poetry.Dynasty,
            Author = poetry.Author,
            Content = poetry.Content,
            Source = RecommendBlogSource.Local
        };
    }
}

public class JinrishiciToken
{
    public string? Data { get; set; } = string.Empty;
}

public class JinrishiciOrigin
{
    public string? Title { get; set; } = string.Empty;
    public string? Dynasty { get; set; } = string.Empty;
    public string? Author { get; set; } = string.Empty;
    public List<string>? Content { get; set; } = new();
    public List<string>? Translate { get; set; } = new();
}

public class JinrishiciData
{
    public string? Content { get; set; } = string.Empty;
    public JinrishiciOrigin? Origin { get; set; } = new();
}

public class JinrishiciSentence
{
    public JinrishiciData? Data { get; set; } = new();
}