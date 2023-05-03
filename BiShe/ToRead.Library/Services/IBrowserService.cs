namespace ToRead.Services;

public interface IBrowserService
{
    Task OpenAsync(string url);
}