namespace ToRead.Library.Misc;

/// <summary>
/// 网络错误信息
/// </summary>
/// <remarks>该类无需实例化,无需调教</remarks>
public static class ErrorMessages
{
    public const string HttpClientErrorTitle = "连接错误";

    public const string HttpClientErrorButton = "确定";

    public static string GetHttpClientError(string server, string message) =>
        $"与{server}连接时发生了错误：\n{message}";

    public const string JsonDeserializationErrorTitle = "读取数据错误";

    public static string GetJsonDeserializationError(string server,
        string message) =>
        $"从{server}读取数据时发生了错误：\n{message}";

    public const string JsonDeserializationErrorButton = "确定";
}