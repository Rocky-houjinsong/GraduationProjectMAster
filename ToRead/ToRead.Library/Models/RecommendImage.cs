namespace ToRead.Library.Models;

public class RecommendImage
{
    public string FullStartDate { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public string Copyright { get; set; } = string.Empty;

    public string CopyrightLink { get; set; } = string.Empty;

    public byte[]? ImageBytes { get; set; }
}