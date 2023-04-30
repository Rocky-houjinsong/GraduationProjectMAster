namespace ToRead.Library.Helpers
{
    /// <summary>
    /// 嵌入式数据库接口
    /// </summary>
    /// <remarks>SQLite,Neo4j数据库实现 借助ORM工具</remarks>
    public interface IEDbContext
    {
        /// <summary>
        /// 创建数据库
        /// </summary>
        void Create();

        void Connection();

        /// <summary>
        /// 是否连接成功
        /// </summary>
        bool IsConnected();
    }
}