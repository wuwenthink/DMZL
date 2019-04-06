using Common;
using RoleSpace;
using UnityEngine;

namespace Vechicle
{
    /// <summary>
    /// 步行移动类
    /// </summary>
    public class Foot_move :Vechicle_move
    {
        /// <summary>
        /// 城市中的移动速度
        /// </summary>
        float Speed_city;

        /// <summary>
        /// 角色
        /// </summary>
        Role role;

        public override void Init ( Role role )
        {
            this.role = role;
            //从RoleSystem系统中的配置文件读取数据
            Speed_city = RoleSystem.I.RoleInfo.Speed_city;

            //-----在此处做遍历人物有没有移动相关的bufff或者道具

        }

        public override void Move_areaMap ()
        {
            
        }

        public override void Move_city ()
        {
            if ( role.MoveState != MoveState.Stand )
            {
                //根据移动状态进行移动
                switch ( role.MoveState )
                {
                    case MoveState.Up:
                        role.transform.Translate(0,Speed_city * UpdateManager.DeltaTime,0);
                        break;
                    case MoveState.Down:
                        role.transform.Translate(0,-Speed_city * UpdateManager.DeltaTime,0);
                        break;
                    case MoveState.Left:
                        role.transform.Translate(-Speed_city * UpdateManager.DeltaTime,0,0);
                        break;
                    case MoveState.Right:
                        role.transform.Translate(Speed_city * UpdateManager.DeltaTime,0,0);
                        break;
                }
            }
        }

        public override void Move_worldMap ()
        {
            
        }
    }
}
