using System;
using Common;
using RoleSpace;

namespace Map
{
    /// <summary>
    /// 给建筑增加处理器代理者
    /// </summary>
    public class MapBuildAddProcessProxy :IProxy
    {
        public Type ProxyType => typeof(MapBuildAddProcessProxy);

        public string[] ListeningMessages
        {
            get
            {
                return new string[]
                {
                    //建筑增加处理器
                    MapMessage.Build+MapMessage.Add+MapMessage.Process
                };
            }
        }

        public void HandleMessage ( string letter,params object[] data )
        {
            AddProcess(data[0] as Build,data[1] as string);
        }

        public void AddProcess (Build build,string processName)
        {
            build.ProcessList.Add(Activator.CreateInstance(Type.GetType("BuildTemplate." + processName)) as IProcess<Role,Build>);
            if ( build.buildInfo.Process1 == null || build.buildInfo.Process1 == processName ) build.buildInfo.Process1 = processName;
            else if ( build.buildInfo.Process2 == null || build.buildInfo.Process2 == processName ) build.buildInfo.Process2 = processName;
            else if ( build.buildInfo.Process3 == null || build.buildInfo.Process3 == processName ) build.buildInfo.Process3 = processName;
        }

        public void Close ()
        {
         
        }

       

        public void Initialize ()
        {
          
        }
    }
}
