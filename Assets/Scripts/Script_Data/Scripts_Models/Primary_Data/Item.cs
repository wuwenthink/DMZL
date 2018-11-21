// ======================================================================================
// 文件名         ：    Item.cs
// 版本号         ：    v1.2.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017‎年‎8‎月‎10‎日  ‏‎11:28:38
// 最后修改日期   ：    2017-10-02 11:08:29
// ======================================================================================
// 功能描述       ：    学识UI
// ======================================================================================
/// <summary>
/// 道具的类
/// </summary>
public class Item
{
    /// <summary>
    /// 道具id
    /// </summary>
    public int id { get; private set; }

    /// <summary>
    /// 道具类型
    /// </summary>
    public int type { get; private set; }

    /// <summary>
    /// 道具名称
    /// </summary>
    public string name { get; private set; }

    /// <summary>
    /// 道具描述
    /// </summary>
    public string des { get; private set; }

    /// <summary>
    /// 道具图标名
    /// </summary>
    public string icon { get; private set; }

    /// <summary>
    /// 道具品质
    /// </summary>
    public int quality { get; private set; }

    /// <summary>
    /// 价值
    /// </summary>
    public int price { get; private set; }

    /// <summary>
    /// 功能索引
    /// </summary>
    public int funIndex { get; private set; }

    /// <summary>
    /// 是否允许在背包中使用
    /// </summary>
    public bool canUse { get; private set; }
    
    /// <summary>
    /// 道具的构造方法
    /// </summary>
    /// <param name="_id">id</param>
    /// <param name="_type">类型</param>
    /// <param name="_name">名称</param>
    /// <param name="_des">说明</param>
    /// <param name="_icon">图标名</param>
    /// <param name="_quality">品质</param>
    /// <param name="_weight">重量</param>
    /// <param name="_size">大小</param>
    /// <param name="_price">价值</param>
    public Item (int _id, int _type, string _name, string _des, string _icon, int _quality, int _price, int _funIndex, int _canUse)
    {
        id = _id;
        type = _type;
        name = _name;
        des = _des;
        icon = _icon;
        quality = _quality;
        price = _price;
        funIndex = _funIndex;
        canUse = _canUse == 1 ? true : false;
    }
}