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

        public  string[] ListeningMessages
        {
            get
            {
                return new string[]
                {
                    //建筑功能触发
                    MapMessage.Build+MapMessage.Interact,
                };
            }
        }

        public  void HandleMessage ( string letter,params object[] data )
        {
            BuildInteract(data[0] as Role,data[1] as Build);
        }

        public abstract void Close ();
        public abstract void Initialize ();

        public abstract void BuildInteract (Role role,Build build);
    }
}
