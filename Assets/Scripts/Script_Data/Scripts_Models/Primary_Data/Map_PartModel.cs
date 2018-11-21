
public class Map_PartModel
{
    /// <summary>
    /// 模块模板ID
    /// </summary>
    public int id { get; private set; }

    /// <summary>
    /// 固定名称
    /// </summary>
    public string name { get; private set; }

    /// <summary>
    /// 模块说明
    /// </summary>
    public string des { get; private set; }

    /// <summary>
    /// 模块类型
    /// </summary>
    public int partType { get; private set; }

    /// <summary>
    /// 模块类型
    /// </summary>
    public int isSpecial { get; private set; }

    /// <summary>
    /// 道路类型
    /// </summary>
    public int roadType { get; private set; }

    /// <summary>
    /// 基础防御
    /// </summary>
    public int fangyu { get; private set; }

    /// <summary>
    /// 基础卫生
    /// </summary>
    public int weisheng { get; private set; }

    /// <summary>
    /// 基础治安
    /// </summary>
    public int zhian { get; private set; }

    /// <summary>
    /// 基础繁荣
    /// </summary>
    public int fanrong { get; private set; }

    /// <summary>
    /// 历史模块
    /// </summary>
    public int isFixed { get; private set; }

    /// <summary>
    /// 格子图片
    /// </summary>
    public string mapIcon { get; private set; }

    /// <summary>
    /// 地图标志
    /// </summary>
    public string mapSign { get; private set; }


    public Map_PartModel(int _id, string _name, string _des, int _partType, int _isSpecial, int _roadType, int _fangyu, int _weisheng, int _zhian, int _fanrong, int _isFixed, string _mapIcon, string _mapSign)
    {
        id = _id;
        name = _name;
        des = _des;
        partType = _partType;
        isSpecial = _isSpecial;
        roadType = _roadType;
        fangyu = _fangyu;
        weisheng = _weisheng;
        zhian = _zhian;
        fanrong = _fanrong;
        isFixed = _isFixed;
        mapIcon = _mapIcon;
        mapSign = _mapSign;
    }

}
