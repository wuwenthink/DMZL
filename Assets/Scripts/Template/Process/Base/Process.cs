namespace Common
{
    /// <summary>
    /// 处理器
    /// </summary>
    public interface IProcess
    {
        void Function ();
    }
    /// <summary>
    /// 处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProcess<T>
    {
        void Function ( T data );
    }
    /// <summary>
    /// 处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    public interface IProcess<T, K>
    {
        void Function ( T data1,K data2 );
    }
    /// <summary>
    /// 处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="S"></typeparam>
    public interface IProcess<T, K, S>
    {
        void Function ( T data,K data2,S data3 );
    }
}
