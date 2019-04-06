using System;
using Common;

namespace Map
{
    /// <summary>
    /// 建筑位置代理者类型
    /// </summary>
    public abstract class MapBuildLocationProxyType :IProxy
    {
        public Type ProxyType => typeof(MapBuildLocationProxyType);

        public abstract string[] ListeningMessages { get; }

        public abstract void Close ();
        public abstract void HandleMessage ( string letter,params object[] data );
        public abstract void Initialize ();
        /// <summary>
        /// 移动位置
        /// </summary>
        public abstract void Location (Build build,float x,float y);
    }
}
