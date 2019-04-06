using RoleSpace;

namespace Vechicle
{
    /// <summary>
    /// 载具移动事件抽象类
    /// </summary>
    abstract public class Vechicle_move
    {
        /// <summary>
        /// 初始化载具
        /// </summary>
        /// <param name="role"></param>
        abstract public void Init ( RoleSpace.Role role );
        /// <summary>
        /// 在城市中的移动事件
        /// </summary>
        abstract public void Move_city ();
        /// <summary>
        /// 在世界地图中的移动事件
        /// </summary>
        abstract public void Move_worldMap ();
        /// <summary>
        /// 在区域地图中的移动事件
        /// </summary>
        abstract public void Move_areaMap ();
    }
}
