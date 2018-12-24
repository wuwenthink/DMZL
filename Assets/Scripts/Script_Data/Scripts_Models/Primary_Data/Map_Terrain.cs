using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Terrain {
    /// <summary>
    /// 地形id
    /// </summary>
    public int id { get; private set; }

    /// <summary>
    /// 地形名称
    /// </summary>
    public string name { get; private set; }

    /// <summary>
    /// 地形类型
    /// </summary>
    public int type { get; private set; }

    /// <summary>
    /// 地面阻力
    /// </summary>
    public int drag { get; private set; }

    /// <summary>
    /// 地形行走特效
    /// </summary>
    public string effect { get; private set; }

    /// <summary>
    /// 地形图标
    /// </summary>
    public string icon { get; private set; }

    public Map_Terrain(int _id, string _name, int _type, int _drag, string _effect, string _icon)
    {
        id = _id;
        name = _name;
        type = _type;
        drag = _drag;
        effect = _effect;
        icon = _icon;
    }
}
