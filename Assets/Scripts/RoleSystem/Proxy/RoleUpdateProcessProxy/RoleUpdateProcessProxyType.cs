using System;
using Common;

namespace RoleSpace
{
    /// <summary>
    /// Update处理器代理者
    /// </summary>
    abstract public class RoleUpdateProcessProxyType :IProxy
    {
        public Type ProxyType => typeof(RoleUpdateProcessProxyType);
       
        public abstract string[] ListeningMessages { get; }

        public abstract void Close ();
        public abstract void HandleMessage ( string letter,params object[] data );
        public abstract void Initialize ();
        public abstract void UpdateFunction ();
    }
}
