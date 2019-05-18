using Common;
using Map;
using RoleSpace;

namespace BuildTemplate
{
    /// <summary>
    /// 陷阱测试
    /// </summary>
    public class ProTrapTest :IProcess<Role,Build>
    {
        public void Function (Role role,Build build )
        {
            Alert.Log("触发测试陷阱: 碰触陷阱角色名字"+ role.roleState.Name+",碰触陷阱数据："+ build.buildInfo.Date1);
        }
    }
}
