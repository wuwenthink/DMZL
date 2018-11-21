using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drama_Area
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
    /// 最高行政机构
    /// </summary>
    public int org_gov { get; private set; }

    /// <summary>
    /// 最高军事机构
    /// </summary>
    public int org_army { get; private set; }

    /// <summary>
    /// 最高学府
    /// </summary>
    public int org_edu { get; private set; }

    /// <summary>
    /// 其他机构
    /// </summary>
    public string org_other { get; private set; }

    /// <summary>
    /// 最高官职
    /// </summary>
    public int lead_post { get; private set; }

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






    public Drama_Area(int _id,string _name,int _drama,int _modelID,int _state,int _force,int _isLock,int _use,int _org_gov,int _org_army,int _org_edu,
        string _org_other,int _lead_post,string _ground,string _prople,string _money,string _food,string _tax_food,string _tax_money)
    {
        id=_id;
        name=_name;
        drama=_drama;
        modelID=_modelID;
        state=_state;
        force=_force;
        isLock=_isLock;
        use=_use;
        org_gov=_org_gov;
        org_army=_org_army;
        org_edu=_org_edu;
        org_other=_org_other;
        lead_post=_lead_post;
        ground=_ground;
        prople=_prople;
        money=_money;
        food=_food;
        tax_food=_tax_food;
        tax_money=_tax_money;
    }
}
