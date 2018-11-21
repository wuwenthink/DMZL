// ======================================================================================
// 文 件 名 称：Business_Trade.cs 
// 版 本 编 号：v1.1.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-01 15:08:15
// 最 后 修 改：xic
// 更 新 时 间：2017-11-21
// ======================================================================================
// 功 能 描 述：商人行当
// ======================================================================================
using System.Collections.Generic;

public class Business_Trade
{
    public Business_Trade (int _id, string _name, string _icon, int _type, int _identity, int _building, string _Job, string _GoodsType, int _lv, int _taskID, int _addPrice, int _kickback_max, int _higgle, int _negotiate, int _kickback, int _taxNum, int _goodsCount, int _taskCount)
    {
        id = _id;
        name = _name;
        icon = _icon;
        type = _type;
        identity = _identity;
        building = _building;
        Job = _Job;
        if (!_Job.Equals ("0"))
        {
            list_Job = new Dictionary<int, int> ();
            string [] reg = _Job.Split (';');
            foreach (string str in reg)
            {
                string [] s = str.Split (',');
                list_Job.Add (int.Parse (s [0]), int.Parse (s [1]));
            }
        }
        else
            list_Job = null;
        GoodsType = _GoodsType;
        if (!_GoodsType.Equals ("0"))
        {
            list_GoodsType = new List<int> ();
            string [] reg = _GoodsType.Split (';');
            foreach (string str in reg)
            {
                list_GoodsType.Add (int.Parse (str));
            }
        }
        else
            list_GoodsType = null;
        lv = _lv;
        taskID = _taskID;
        addPrice = _addPrice;
        kickback_max = _kickback_max;
        higgle = _higgle;
        negotiate = _negotiate;
        kickback = _kickback;
        taxNum = _taxNum;
        goodsCount = _goodsCount;
        taskCount = _taskCount;
    }


    /// <summary>
    /// ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 行当名称
    /// </summary>
    public string name { get; private set; }

    /// <summary>
    /// 行当图标
    /// </summary>
    public string icon { get; private set; }

    /// <summary>
    /// 行当类型
    /// </summary>
    public int type { get; private set; }
    /// <summary>
    /// 所属身份
    /// </summary>
    public int identity { get; private set; }
    /// <summary>
    /// 对应建筑
    /// </summary>
    public int building { get; private set; }
    /// <summary>
    /// 对应职位
    /// </summary>
    public string Job { get; private set; }
    public Dictionary<int, int> list_Job { get; private set; }
    /// <summary>
    /// 商品类型
    /// </summary>
    public string GoodsType { get; private set; }
    public List<int> list_GoodsType { get; private set; }
    /// <summary>
    /// 商店等级
    /// </summary>
    public int lv { get; private set; }
    /// <summary>
    /// 开启任务ID
    /// </summary>
    public int taskID { get; private set; }
    /// <summary>
    /// 定价溢价
    /// </summary>
    public int addPrice { get; private set; }
    /// <summary>
    /// 最高回扣比例
    /// </summary>
    public int kickback_max { get; private set; }
    /// <summary>
    /// 还价基础成功率
    /// </summary>
    public int higgle { get; private set; }
    /// <summary>
    /// 谈判基础成功率
    /// </summary>
    public int negotiate { get; private set; }
    /// <summary>
    /// 回扣基础成功率
    /// </summary>
    public int kickback { get; private set; }
    /// <summary>
    /// 税收定额
    /// </summary>
    public int taxNum { get; private set; }
    /// <summary>
    /// 商店容量
    /// </summary>
    public int goodsCount { get; private set; }
    /// <summary>
    /// 每次刷新任务数
    /// </summary>
    public int taskCount { get; private set; }

}

