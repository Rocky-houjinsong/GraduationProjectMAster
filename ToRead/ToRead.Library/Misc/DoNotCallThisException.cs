namespace ToRead.Library.Misc;

/// <summary>
/// 不可调用方法 异常类
/// </summary>
/// <remarks>该类无需调教</remarks>
public class DoNotCallThisException : Exception
{
    public DoNotCallThisException() : base("不应该调用此项目。")
    {
    }
}