// ======================================================================================
// 文件名         ：    ItemInBag.cs
// 版本号         ：    v1.1.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-10
// 最后修改日期   ：    2017-10-03 20:25:39
// ======================================================================================
// 功能描述       ：    角色拥有的道具信息
// ======================================================================================

/// <summary>
/// 角色拥有的道具信息
/// </summary>
public class ItemInBag
{
    /// <summary>
    /// 道具
    /// </summary>
    public Item item;

    /// <summary>
    /// 拥有数量
    /// </summary>
    public int count { get; set; }

    /// <summary>
    /// 角色拥有道具的构造方法
    /// </summary>
    /// <param name="_item">道具</param>
    /// <param name="_count">拥有数量</param>
    public ItemInBag (Item _item, int _count)
    {
        item = _item;
        count = _count;
    }

    /// <summary>
    /// 清空
    /// 销毁时调用
    /// </summary>
    public void Clear ()
    {
        item = null;
    }
}