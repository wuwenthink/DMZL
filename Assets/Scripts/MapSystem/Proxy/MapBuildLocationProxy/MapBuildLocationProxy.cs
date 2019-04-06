using UnityEngine;

namespace Map
{
    /// <summary>
    /// 建筑位置代理者
    /// </summary>
    public class MapBuildLocationProxy :MapBuildLocationProxyType
    {
        public override string[] ListeningMessages
        {
            get
            {
                return new string[]
                {
                    //建筑坐标
                    MapMessage.Build+MapMessage.Location,
                };
            }
        }
        public override void Initialize ()
        {

        }
        public override void Close ()
        {
            
        }

        public override void HandleMessage ( string letter,params object[] data )
        {
            Location(data[0] as Build,(float)data[1],(float)data[2]);
        }

     

        /// <summary>
        /// 建筑更换位置
        /// </summary>
        /// <param name="build"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void Location ( Build build,float x,float y )
        {
            build.transform.position = new Vector2(x,y);
            build.buildInfo.Location_x = x;
            build.buildInfo.Location_y = y;
        }
    }
}
