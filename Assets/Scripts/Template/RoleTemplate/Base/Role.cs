using UnityEngine;
using Common;
using System.Collections.Generic;

namespace RoleSpace
{
    /// <summary>
    /// 角色脚本
    /// </summary>
    abstract public class Role :MonoBehaviour
    {
        /// <summary>
        /// 角色状态
        /// </summary>
        public RoleInfo roleState;

        /// <summary>
        /// 移动方向
        /// </summary>
        public MoveState MoveState;

        /// <summary>
        /// 互动对象
        /// </summary>
        public Transform InteractObject;

        private void OnTriggerStay2D ( Collider2D collision )
        {
            InteractObject = collision.transform;
        }
        private void OnTriggerExit2D ( Collider2D collision )
        {
            if ( InteractObject == collision.transform ) InteractObject = null;
        }

        /// <summary>
        /// Update处理器集合
        /// </summary>
        public List<IProcess<Role>> UpdateProcessList;

        /// <summary>
        /// 交互处理器集合
        /// </summary>
        public List<IProcess<Role,Role>> ProcessList;

       

        private void Awake ()
        {
            //实例 Update处理器集合
            UpdateProcessList = new List<IProcess<Role>>();
            //实例 交互处理器集合
            ProcessList = new List<IProcess<Role,Role>>();
            //初始互动对象为空
            InteractObject = null;
            //初始移动方向为站立
            MoveState = MoveState.Stand;

            //初始化
            Initialize();
        }

        /// <summary>
        /// 初始化角色数据
        /// </summary>
        public void Init ( RoleInfo roleState )
        {
            this.roleState = roleState;
            //发送信息登上现在的载具
            RoleSystem.I.SendMessage(RoleMessage.Vechicle + RoleMessage.Up,this,roleState.VechicleName);
            //发送信息添加Update处理器
            RoleSystem.I.SendMessage(RoleMessage.Add + RoleMessage.UpdateProcess,this);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Initialize ();    
    }
}
