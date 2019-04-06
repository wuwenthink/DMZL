using Common;
using System;

namespace Common
{
    /// <summary>
    /// 功能中介者接口
    /// </summary>
    public interface IMediator:IObserver
    {
        /// <summary>
        /// 中介者的类型
        /// </summary>
        Type MediatorType
        {
            get;
        }
    }
}
