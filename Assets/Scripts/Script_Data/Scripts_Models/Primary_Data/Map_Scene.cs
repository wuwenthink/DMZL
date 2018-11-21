using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Scene  {

    /// <summary>
    /// 场景地图ID
    /// </summary>
    public int id { get; private set; }

    /// <summary>
    /// 场景名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 场景类型
    /// </summary>
    public int type { get; private set; }

    /// <summary>
    /// 场景说明
    /// </summary>
    public string des { get; private set; }

    /// <summary>
    /// 所属区域
    /// </summary>
    public int area { get; private set; }
    /// <summary>
    /// 场景模板
    /// </summary>
    public int model { get; private set; }

    public Map_Scene(int _id, string _name, int _type, string _des, int _area,int _model)
    {
        id = _id;
        Name = _name;
        type = _type;
        des = _des;
        area = _area;
        model = _model;
    }

}
