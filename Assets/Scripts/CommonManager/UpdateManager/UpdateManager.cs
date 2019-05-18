using System;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// Update管理器
    /// </summary>
    public class UpdateManager :MonoSingleton<UpdateManager>
    {
        /// <summary>
        /// Update事件
        /// </summary>
        Action updateAction;

        /// <summary>
        /// 系统Update事件
        /// </summary>
        Action systemUpdateAction;

        /// <summary>
        /// Update是否打开
        /// </summary>
        private bool updateIO;
        public bool UpdateIO { get => updateIO; }

        /// <summary>
        /// 事件流逝速度
        /// </summary>
        public static int TimeRate;

        /// <summary>
        /// 每帧间隔时间
        /// </summary>
        public static float DeltaTime;

     

        /// <summary>
        /// 开始Update
        /// </summary>
        public void StartUpdate ()
        {
            updateIO = true;
        }
        /// <summary>
        /// 暂停Update
        /// </summary>
        public void PauseUpdate ()
        {
            updateIO = false;
        }

        protected override void Initialize ()
        {
            //初始事件流失速度为1
            TimeRate = 1;
            //Update开关附初值
            updateIO = true;
        }



        #region 游戏Update方法
        /// <summary>
        /// 添加Update事件
        /// </summary>
        /// <param name="updateAction"></param>
        /// <returns>Update编号</returns>
        public void OnUpdate ( Action updateAction )
        {
            //向集合中添加Update事件
            this.updateAction += updateAction;
        }

        /// <summary>
        /// 移除Update
        /// </summary>
        /// <param name="Listnum"></param>
        public void RemoveUpdate ( Action updateAction )
        {
            this.updateAction -= updateAction;
        }

        /// <summary>
        /// 清空Update
        /// </summary>
        public void ClearUpdate ()
        {
            //清空Update集合
            updateAction = null;
        }

        #endregion 清空Update

        #region 系统Update方法
        /// <summary>
        /// 添加系统Update事件
        /// </summary>
        /// <param name="updateAction"></param>
        /// <returns>Update编号</returns>
        public void OnSystemUpdate ( Action updateAction )
        {
            //向集合中添加Update事件
            systemUpdateAction += updateAction;

        }

        /// <summary>
        /// 移除系统Update
        /// </summary>
        /// <param name="Listnum"></param>
        public void RemoveSystemUpdate ( Action updateAction )
        {
            systemUpdateAction -= updateAction;
        }

        /// <summary>
        /// 清空系统Update
        /// </summary>
        public void ClearSystemUpdate ()
        {
            //清空Update集合
            systemUpdateAction = null;
        }
        #endregion


        // Update is called once per frame
        private void Update ()
        {
         
            //系统Update事件执行
            systemUpdateAction?.Invoke();

            //游戏Update事件执行
            if ( UpdateIO )
            {
                updateAction?.Invoke();
                //获取间隔时间
                DeltaTime = Time.deltaTime * TimeRate;
            }
            else DeltaTime = 0;

        }
    }
}
