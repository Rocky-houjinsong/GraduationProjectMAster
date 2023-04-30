namespace ToRead.Library.Services
{
    /// <summary>
    /// 浏览网页服务接口
    /// </summary>
    /// <remarks>该接口调教完成</remarks>
    public interface IBrowerService
    {
        Task OpenAsync(string url);
    }
}