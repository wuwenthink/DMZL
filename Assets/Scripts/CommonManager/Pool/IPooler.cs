using UnityEngine;

namespace Common
{
    /// <summary>
    /// 对象池接口
    /// </summary>
    public interface IPooler 
    {
        /// <summary>
        /// 对象复位执行
        /// </summary>
        void OnReset ();
        /// <summary>
        /// 对象回收执行
        /// </summary>
        void Recover ();
    }
}
