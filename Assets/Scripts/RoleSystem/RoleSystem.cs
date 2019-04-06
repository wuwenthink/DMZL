using Common;
using UnityEditor;

namespace RoleSpace
{
    public class RoleSystem :System<RoleSystem>
    {
        public RoleSystemInfo RoleInfo;
        public override void Start ()
        {
            //读取RoleInfo
            RoleInfo = AssetDatabase.LoadAssetAtPath<RoleSystemInfo>(RoleSystemInfo.RoleInfoPath);
        
            //注册角色载具移动代理者
            RegisterProxy(new RoleVechicleMoveProxy_city());
            //注册角色Upda处理器代理者
            RegisterProxy(new RoleUpdateProcessProxy());
        }
    }
}
