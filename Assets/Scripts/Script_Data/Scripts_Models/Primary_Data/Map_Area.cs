// ====================================================================================== 
// 文件名         ：    Area.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-19                                                                  
// 最后修改日期   ：    2017-8-21                                                            
// ====================================================================================== 
// 功能描述       ：    区域                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 区域
/// </summary>
public class Map_Area
{
    /// <summary>
    /// ID
    /// </summary>
    public int id { get; private set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string name { get; private set; }

    /// <summary>
    /// 所属剧本
    /// </summary>
    public int drama { get; private set; }

    /// <summary>
    /// 区域表ID
    /// </summary>
    public int modelID { get; private set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int state { get; private set; }

    /// <summary>
    /// 所属势力ID
    /// </summary>
    public int force { get; private set; }

    /// <summary>
    /// 是否解锁
    /// </summary>
    public int isLock { get; private set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public int use { get; private set; }

    /// <summary>
    /// 地亩
    /// </summary>
    public string ground { get; private set; }

    /// <summary>
    /// 丁口
    /// </summary>
    public string prople { get; private set; }

    /// <summary>
    /// 存银
    /// </summary>
    public string money { get; private set; }

    /// <summary>
    /// 存粮
    /// </summary>
    public string food { get; private set; }

    /// <summary>
    /// 田税（百分之）
    /// </summary>
    public string tax_food { get; private set; }

    /// <summary>
    /// 商税（百分之）
    /// </summary>
    public string tax_money { get; private set; }


    public Map_Area(int _id, string _name, int _drama, int _modelID, int _state, int _force, int _isLock, int _use, string _ground, string _prople, string _money, string _food, string _tax_food, string _tax_money)
    {
        id = _id;
        name = _name;
        drama = _drama;
        modelID = _modelID;
        state = _state;
        force = _force;
        isLock = _isLock;
        use = _use;
        ground = _ground;
        prople = _prople;
        money = _money;
        food = _food;
        tax_food = _tax_food;
        tax_money = _tax_money;

    }

}

