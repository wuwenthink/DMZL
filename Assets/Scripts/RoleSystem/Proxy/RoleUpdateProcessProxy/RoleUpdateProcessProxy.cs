using System.Collections.Generic;
using Common;

namespace RoleSpace
{
    class RoleUpdateProcessProxy :RoleUpdateProcessProxyType
    {
        List<Role> rolesList;
        public override string[] ListeningMessages
        {
            get
            {
                return new string[]
                {
                    //增加Update处理器
                    RoleMessage.Add+RoleMessage.UpdateProcess,
                    //移除Update处理器
                    RoleMessage.Remove+RoleMessage.UpdateProcess
                };
            }
        }

        public override void Close ()
        {
            throw new System.NotImplementedException();
        }
        public override void Initialize ()
        {
            rolesList = new List<Role>();
            UpdateManager.I.OnUptate(UpdateFunction);
        }

        public override void HandleMessage ( string letter,params object[] data )
        {
            switch ( letter )
            {
                case RoleMessage.Add + RoleMessage.UpdateProcess:
                    rolesList.Add(data[0]as Role);
                    break;
                case RoleMessage.Remove + RoleMessage.UpdateProcess:
                    rolesList.Remove(data[0] as Role);
                    break;
            }
        }

        void UpdateFunction ()
        {
            //获取角色数量
            int rolesCount = rolesList.Count;
            //处理器数量
            int processCount;
            for ( int i = 0 ; i < rolesCount ; i++ )
            {
                //获取处理器数量
                processCount = rolesList[i].UpdateProcessList.Count;
                for ( int j = 0 ; j < processCount ; j++ )
                {
                    //执行处理器
                    rolesList[i].UpdateProcessList[j].Function(rolesList[i]);
                }
            }
        }
    }
}
