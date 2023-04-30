namespace ToRead.Library.Services
{
    /// <summary>
    /// 键值存储接口
    /// </summary>
    /// <remarks>该接口调教完成</remarks>
    public interface IPreferenceStorage
    {
        void Set(string key, int value);

        int Get(string key, int defaultValue);


        void Set(string key, string value);

        string Get(string key, string defaultValue);


        void Set(string key, DateTime value);

        DateTime Get(string key, DateTime defaultValue);
    }
}