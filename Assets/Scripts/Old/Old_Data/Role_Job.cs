// ======================================================================================
// 文 件 名 称：Role_Job.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-12 19:15:25
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-12 19:15:25
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using UnityEngine;

public class Role_Job : MonoBehaviour
{

    public Role_Job (int _id, string _name, int _lv, string _power, int _org, int _iden, int _price, int _holiday)
    {
        id = _id;
        name = _name;
        lv = _lv;
        power = _power;
        org = _org;
        iden = _iden;
        price = _price;
        holiday = _holiday;
    }



    /// <summary>
    /// 职位ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 等级
    /// </summary>
    public int lv { get; private set; }
    /// <summary>
    /// 权限
    /// </summary>
    public string power { get; private set; }
    /// <summary>
    /// 组织机构
    /// </summary>
    public int org { get; private set; }
    /// <summary>
    /// 身份
    /// </summary>
    public int iden { get; private set; }
    /// <summary>
    /// 标准薪水
    /// </summary>
    public int price { get; private set; }
    /// <summary>
    /// 每月允许休假天数
    /// </summary>
    public int holiday { get; private set; }
}
