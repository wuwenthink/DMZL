using RoleSpace;

namespace Map
{
    /// <summary>
    /// 建筑执行功能代理者
    /// </summary>
    public class MapBuildInteractProxy :MapBuildInteractProxyType
    {
        public override string[] ListeningMessages
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

        public override void HandleMessage ( string letter,params object[] data )
        {
            BuildInteract(data[0] as Role,data[1] as Build);
        }

        public override void BuildInteract ( Role role,Build build )
        {
            //遍历处理器集合并执行
            int count = build.ProcessList.Count;
            for ( int i = 0 ; i < count ; i++ )
            {
                build.ProcessList[i].Function(role,build);
            }
        }

        public override void Close ()
        {
 
        }

       

        public override void Initialize ()
        {
          
        }
    }
}
