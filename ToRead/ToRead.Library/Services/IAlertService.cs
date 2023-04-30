namespace ToRead.Library.Services
{
    /// <summary>
    /// 提示信息.
    /// </summary>
    /// <remarks>该接口调教完成</remarks>
    public interface IAlertService
    {
        void Alert(string title, string message, string button);
    }
}