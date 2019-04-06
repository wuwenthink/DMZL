using System;
using System.Collections.Generic;
using Common;
using Vechicle;

namespace RoleSpace
{

    /// <summary>
    /// 角色载具移动代理者--城市
    /// </summary>
    public class RoleVechicleMoveProxy_city :RoleVechicleMoveProxyType
    {
        //载具移动类字典
        Dictionary<int,Vechicle_move> vechicleDic;

        public override string[] ListeningMessages
        {
            get
            {
                return new string[]
                {
                    //登上载具消息
                    RoleMessage.Vechicle+RoleMessage.Up,
                     //离开载具消息
                    RoleMessage.Vechicle+RoleMessage.Down,
                };
            }
        }
        public override void HandleMessage ( string letter,params object[] data )
        {
            switch ( letter )
            {
                //登上载具消息
                case RoleMessage.Vechicle + RoleMessage.Up:
                    VechicleUp(data[0] as Role,data[1] as string);
                    //------在此处应有其他代理者处理模型更替，帧动画等其他操作-------
                    break;

                //离开载具消息
                case RoleMessage.Vechicle + RoleMessage.Down:
                    VechicleDown(data[0] as Role);
                    break;
            }
        }

        public override void Initialize ()
        {
            //实例载具字典
            vechicleDic = new Dictionary<int,Vechicle_move>();
        }

        public override void Close ()
        {
            //将所有载具类全部从UpdateManager中移除
            foreach ( Vechicle_move runner in vechicleDic.Values )
            {
                UpdateManager.I.RemoveUpdate(runner.Move_city);
            }
            vechicleDic.Clear();
        }

        /// <summary>
        /// 登上载具            
        /// </summary>
        /// <param name="role"></param>
        public override void VechicleUp ( Role role ,string  VechicleName )
        {
            //如果字典中已有载具，则撤下载具
            if ( vechicleDic.ContainsKey(role.roleState.RoleId) ) VechicleDown(role);
            //利用反射根据载具名称实例载具类，并用角色ID记录至字典
            vechicleDic[role.roleState.RoleId] = Activator.CreateInstance(Type.GetType("Vechicle." + VechicleName + "_move"))as Vechicle_move;
            //初始化载具类
            vechicleDic[role.roleState.RoleId].Init(role);
            //将步行者的Run方法放入UpdateManager中
            UpdateManager.I.OnUptate(vechicleDic[role.roleState.RoleId].Move_city);

            //修改角色状态中的人物载具
            role.roleState.VechicleName = VechicleName;
        }

        /// <summary>
        /// 离开载具
        /// </summary>
        /// <param name="role"></param>
        public override void VechicleDown ( Role role )
        {
            //将载具类的Run方法从UpdateManager移除
            UpdateManager.I.RemoveUpdate(vechicleDic[role.roleState.RoleId].Move_city);
            //从字典中移除
            vechicleDic.Remove(role.roleState.RoleId);
        }   
    }
}
