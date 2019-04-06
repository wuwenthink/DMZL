using System;
using Common;

namespace RoleSpace
{
    /// <summary>
    ///  角色载具移动理者类型
    /// </summary>
    abstract public class RoleVechicleMoveProxyType :IProxy
    {
        public Type ProxyType =>typeof(RoleVechicleMoveProxyType);

        public abstract string[] ListeningMessages { get; }

        public abstract void Close ();
        public abstract void Initialize ();

        /// <summary>
        /// 登上载具
        /// </summary>
        /// <param name="role"></param>
        public abstract void VechicleUp ( Role role ,string VechicleName);
        /// <summary>
        /// 下载具
        /// </summary>
        /// <param name="role"></param>
        public abstract void VechicleDown ( Role role );
        public abstract void HandleMessage ( string letter,params object[] data );
    }
}
