using System;

namespace Common
{
    /// <summary>
    /// 功能代理者接口
    /// </summary>
    public interface IProxy:IObserver
    {
        /// <summary>
        /// 返回功能代理者的类型
        /// </summary>
        Type ProxyType
        {
            get;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize ();
        /// <summary>
        /// 关闭
        /// </summary>
        void Close ();
    }
}
