using System;
using Common;
using RoleSpace;

namespace Map
{
    /// <summary>
    /// 建筑执行功能代理者类型
    /// </summary>
    public abstract class MapBuildInteractProxyType :IProxy
    {
        public  Type ProxyType => typeof(MapBuildInteractProxyType);

        public abstract string[] ListeningMessages { get; }

        public abstract void Close ();
        public abstract void Initialize ();

        public abstract void BuildInteract (Role role,Build build);
        public abstract void HandleMessage ( string letter,params object[] data );
    }
}
