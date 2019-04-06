using System.Collections.Generic;
using Common;
using RoleSpace;
using UnityEngine;

namespace Map
{
    /// <summary>
    /// 建筑的抽象类
    /// </summary>
    public abstract class Build:MonoBehaviour
    {
        /// <summary>
        /// 建筑信息
        /// </summary>
        public BuildInfo buildInfo;

        /// <summary>
        /// 处理器集合
        /// </summary>
        public List<IProcess<Role,Build>> ProcessList;

        private void Awake ()
        {
            //实例处理器集合
            ProcessList = new List<IProcess<Role,Build>>();
            
            //初始化
            Initialize();
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        public  void Init ( BuildInfo buildInfo )
        {
            this.buildInfo = buildInfo;

            //确定建筑位置
            MapSystem.I.SendMessage(MapMessage.Build + MapMessage.Location,this,buildInfo.Location_x,buildInfo.Location_y);

            //将建筑信息中的处理器实例
            if ( buildInfo.Process1 != null ) MapSystem.I.SendMessage(MapMessage.Build + MapMessage.Add + MapMessage.Process,this,buildInfo.Process1);
            if ( buildInfo.Process2 != null ) MapSystem.I.SendMessage(MapMessage.Build + MapMessage.Add + MapMessage.Process,this,buildInfo.Process2);
            if ( buildInfo.Process3 != null ) MapSystem.I.SendMessage(MapMessage.Build + MapMessage.Add + MapMessage.Process,this,buildInfo.Process3);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Initialize();


    }
}

