using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drama_Role
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
    /// 角色表ID
    /// </summary>
    public int modelID { get; private set; }

    /// <summary>
    /// posX
    /// </summary>
    public int posX { get; private set; }

    /// <summary>
    /// posY
    /// </summary>
    public int posY { get; private set; }

    /// <summary>
    /// 所属组织
    /// </summary>
    public int org { get; private set; }

    /// <summary>
    /// 职务编号
    /// </summary>
    public int work { get; private set; }

    /// <summary>
    /// 所属势力
    /// </summary>
    public int force { get; private set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public int use { get; private set; }

    /// <summary>
    /// 初始道具
    /// </summary>
    public int item { get; private set; }

    public Drama_Role(int _id, string _name, int _drama, int _modelID, int _posX, int _posY, int _org, int _work, int _force, int _use, int _item)
    {
        id = _id;
        name = _name;
        drama = _drama;
        modelID = _modelID;
        posX = _posX;
        posY = _posY;
        org = _org;
        work = _work;
        force = _force;
        use = _use;
        item = _item;
    }

}
