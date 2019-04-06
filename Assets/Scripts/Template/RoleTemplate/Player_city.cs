using Common;
using Map;
using RoleSpace;
using UnityEngine;

namespace RoleTemplate
{
    /// <summary>
    /// 玩家脚本_城市
    /// </summary>
    public class Player_city :Role
    {
        public override void Initialize ()
        {
            //向集合添加处理器
            UpdateProcessList.Add(new MoveControlProcess());
            UpdateProcessList.Add(new InteractProcess());
        }
    }
    /// <summary>移动控制处理器</summary>
    class MoveControlProcess :IProcess<Role>
    {

        public void Function ( Role role )
        {
            if ( role.MoveState == MoveState.Stand )
            {
                if ( Input.GetKeyDown(KeyCode.W) )
                {
                    //角色移动状态为向前
                    role.MoveState = MoveState.Up;
                }
                else if ( Input.GetKeyDown(KeyCode.A) )
                {
                    //角色移动状态为向左
                    role.MoveState = MoveState.Left;
                }
                else if ( Input.GetKeyDown(KeyCode.S) )
                {
                    //角色移动状态为向后
                    role.MoveState = MoveState.Down;
                }
                else if ( Input.GetKeyDown(KeyCode.D) )
                {
                    //角色移动状态为向右
                    role.MoveState = MoveState.Right;
                }
            }
            else
            {
                if ( Input.GetKeyUp(KeyCode.W) && role.MoveState == MoveState.Up )
                {
                    //角色移动状态为停止
                    role.MoveState = MoveState.Stand;
                }
                else if ( Input.GetKeyUp(KeyCode.A) && role.MoveState == MoveState.Left )
                {
                    //角色移动状态为停止
                    role.MoveState = MoveState.Stand;
                }
                else if ( Input.GetKeyUp(KeyCode.S) && role.MoveState == MoveState.Down )
                {
                    //角色移动状态为停止
                    role.MoveState = MoveState.Stand;
                }
                else if ( Input.GetKeyUp(KeyCode.D) && role.MoveState == MoveState.Right )
                {
                    //角色移动状态为停止
                    role.MoveState = MoveState.Stand;
                }
            }
        }
    }
    /// <summary> 互动控制处理器 </summary>
    class InteractProcess :IProcess<Role>
    {
        public void Function ( Role role )
        {
            if ( role.InteractObject != null && Input.GetKeyDown(KeyCode.E) )
            {
                //根据标签判断交互对象类型
                switch ( role.InteractObject.tag )
                {
                    case "Build":
                        Build build = role.InteractObject.GetComponent<Build>();
                        MapSystem.I.SendMessage(MapMessage.Build + MapMessage.Interact,this,build);
                        break;
                }
            }
        }
    }
}

