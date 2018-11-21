// ====================================================================================== 
// 文件名         ：    Building_Model.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-7-22                                                                   
// 最后修改日期   ：    2017-7-22                                                            
// ====================================================================================== 
// 功能描述       ：    建筑信息的实体类                                                                  
// ====================================================================================== 

/// <summary>
/// 建筑的类
/// </summary>
public class Building_Model  
{
    public Building_Model(int _id, string _name, int _buildingType, string _des, int _size, int _userType, int _durability, int _price, string _mapIcon, string _mapSign)
    {
        id = _id;
        name = _name;
        buildingType = _buildingType;
        des = _des;
        size = _size;
        userType = _userType;
        durability = _durability;
        price = _price;
        mapIcon = _mapIcon;
        mapSign = _mapSign;
    }



    /// <summary>
    /// 建筑模板ID
    /// </summary>
    public int id { get; private set; }

    /// <summary>
    /// 固定名称
    /// </summary>
    public string name { get; private set; }

    /// <summary>
    /// 建筑类型
    /// </summary>
    public int buildingType { get; private set; }

    /// <summary>
    /// 建筑说明
    /// </summary>
    public string des { get; private set; }

    /// <summary>
    /// 建筑大小
    /// </summary>
    public int size { get; private set; }

    /// <summary>
    /// 产权类型
    /// </summary>
    public int userType { get; private set; }

    /// <summary>
    /// 耐久度
    /// </summary>
    public int durability { get; private set; }

    /// <summary>
    /// 售出价格
    /// </summary>
    public int price { get; private set; }

    /// <summary>
    /// 小地图icon
    /// </summary>
    public string mapIcon { get; private set; }

    /// <summary>
    /// 小地图标志类型
    /// </summary>
    public string mapSign { get; private set; }





}
