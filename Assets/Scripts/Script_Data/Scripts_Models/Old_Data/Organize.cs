// ====================================================================================== 
// 文件名         ：    Organize.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-18                                                                  
// 最后修改日期   ：    2017-8-18                                                            
// ====================================================================================== 
// 功能描述       ：    机构                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 机构
/// </summary>
public class Organize
{
    /// <summary>
    /// 机构id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 机构名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 上级机构id
    /// </summary>
    public int upId { get; private set; }
    /// <summary>
    /// 图标名
    /// </summary>
    public string icon { get; private set; }
    /// <summary>
    /// 介绍
    /// </summary>
    public string des { get; private set; }
    /// <summary>
    /// 首领官身份id
    /// </summary>
    public int leader { get; private set; }
    /// <summary>
    /// 隶属势力id
    /// </summary>
    public int force { get; private set; }

    public Organize(int _id, string _name, int _upId, string _icon, string _des, int _leader, int _force)
    {
        id = _id;
        name = _name;
        upId = _upId;
        icon = _icon;
        des = _des;
        leader = _leader;
        force = _force;
    }
}
