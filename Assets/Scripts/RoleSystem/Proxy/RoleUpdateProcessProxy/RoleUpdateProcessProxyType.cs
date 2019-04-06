using System;
using Common;

namespace RoleSpace
{
    abstract public class RoleUpdateProcessProxyType :IProxy
    {
        public Type ProxyType => typeof(RoleUpdateProcessProxyType);
       
        public abstract string[] ListeningMessages { get; }

        public abstract void Close ();
        public abstract void HandleMessage ( string letter,params object[] data );
        public abstract void Initialize ();
    }
}
