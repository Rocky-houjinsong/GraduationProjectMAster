namespace ToRead.Services;

public class BrowserService : IBrowserService
{
    public async Task OpenAsync(string url) => await Browser.OpenAsync(url);
}