using Common;
using Map;
using RoleSpace;
using UnityEngine;

namespace Template
{
    /// <summary>
    /// 商店建筑测试
    /// </summary>
    public class Shop_Test :Build
    {

        public override void Initialize ()
        {
            //将处理器集合中添加此处理器
            ProcessList.Add(new RoleColor_Green());
        }
    }

    /// <summary>
    /// 角色变成绿色
    /// </summary>
    class RoleColor_Green :IProcess<Role,Build>
    {
        public void Function ( Role role ,Build build )
        {
            //角色颜色变为绿色
            build.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
