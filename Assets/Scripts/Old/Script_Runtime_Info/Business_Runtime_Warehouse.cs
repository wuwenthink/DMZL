// ======================================================================================
// 文 件 名 称：Business_Runtime_Warehouse.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-12 13:05:34
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-12 20:13
// ======================================================================================
// 功 能 描 述：运行时数据：仓库
// ======================================================================================


using System.Collections.Generic;

public class Business_Runtime_Warehouse : Business_Warehouse
{
    public Business_Runtime_Warehouse (int _type, string _name, int _lv, int _building, int _Vol, string _volValue, int _dailyLoss, int _rentingPrice, int _buyPrice, int _maintainPrice, int _robberPercent, string _robberValue, int _firePercent, string _fireValue, int _dirtyPercent, int _id) : base (_type, _name, _lv, _building, _Vol, _volValue, _dailyLoss, _rentingPrice, _buyPrice, _maintainPrice, _robberPercent, _robberValue, _firePercent, _fireValue, _dirtyPercent)
    {
        Id = _id;
        GoodsDic = new Dictionary<int, Goods> ();
        WareRecordList = new List<WareRecord> ();
        Demaged = 0;
        Dirty = 0;
        Loss = _dailyLoss;
    }

    public Business_Runtime_Warehouse (int _type, int _id) : base (_type)
    {
        Id = _id;
        GoodsDic = new Dictionary<int, Goods> ();
        WareRecordList = new List<WareRecord> ();
        Demaged = 0;
        Dirty = 0;
        Loss = SelectDao.GetDao().SelectBusiness_Warehouse(_type).dailyLoss;
    }

    /// <summary>
    /// 仓库ID
    /// </summary>
    public int Id;
    /// <summary>
    /// 看护人(-1是店主自己)
    /// </summary>
    public int Person = -1;
    /// <summary>
    /// 仓库磨损度（根据时间和事件增加减少）
    /// </summary>
    public int Demaged { set; get; }
    /// <summary>
    /// 每月损耗比例
    /// </summary>
    public int Loss { set; get; }
    /// <summary>
    /// 容量等级
    /// </summary>
    public int VolLevel = 0;
    /// <summary>
    /// 盗贼等级
    /// </summary>
    public int RobberLevel = 0;
    /// <summary>
    /// 火灾等级
    /// </summary>
    public int FireLevel = 0;
    /// <summary>
    /// 每月损耗比例
    /// </summary>
    public int Dirty { set; get; }
    /// <summary>
    /// 当前容量
    /// </summary>
    public int CurrVol { private set; get; }
    /// <summary>
    /// 货物集合
    /// </summary>
    public Dictionary<int, Goods> GoodsDic { private set; get; }
    /// <summary>
    /// 已占用容量
    /// </summary>
    public int Occupy { private set; get; }
    /// <summary>
    /// 仓库收支记录
    /// </summary>
    public List<WareRecord> WareRecordList { private set; get; }

    /// <summary>
    /// 获得当前仓库的容量（包含计算，升级后直接给VolLevel赋值，并重新调用此方法即可）。
    /// </summary>
    /// <returns></returns>
    public int GetNow_Vol()
    {
        int vol = SelectDao.GetDao().SelectBusiness_Warehouse(Id).Vol;
        string[] lvUp = SelectDao.GetDao().SelectBusiness_Warehouse(Id).volValue.Split(';');
        int lvVol = int.Parse(lvUp[0]);
        if (VolLevel == 0)
        {
            VolLevel = vol;
        }
        else
        {
            //临时计算公式
            VolLevel = vol + VolLevel * lvVol;
        }
        return CurrVol;
    }

    /// <summary>
    /// 获得当前火灾的几率（包含计算，升级后直接给FireLevel赋值，并重新调用此方法即可）。
    /// </summary>
    /// <returns></returns>
    public int GetNow_Fire()
    {
        int percent = 0;
        string[] lvUp = SelectDao.GetDao().SelectBusiness_Warehouse(Id).fireValue.Split(';');
        int lvVol = int.Parse(lvUp[0]);
        int fire = SelectDao.GetDao().SelectBusiness_Warehouse(Id).firePercent;
        //临时计算公式
        percent = fire - fire * FireLevel * lvVol;
        return percent;
    }

    /// <summary>
    /// 获得当前遭遇盗窃的几率（包含计算，升级后直接给RobberLevel赋值，并重新调用此方法即可）。
    /// </summary>
    /// <returns></returns>
    public int GetNow_Robber()
    {
        int percent = 0;
        string[] lvUp = SelectDao.GetDao().SelectBusiness_Warehouse(Id).robberValue.Split(';');
        int lvVol = int.Parse(lvUp[0]);
        int robber = SelectDao.GetDao().SelectBusiness_Warehouse(Id).robberPercent;
        //临时计算公式
        percent = robber - robber * RobberLevel * lvVol;
        return percent;
    }
    

    /// <summary>
    /// 增加商品
    /// </summary>
    /// <param name="_id">商品ID</param>
    /// <param name="_count">数量</param>
    public void AddGoods (int _id, int _count)
    {
        if (!GoodsDic.ContainsKey (_id))
            GoodsDic.Add (_id, new Goods (_id, _count));
        else
            GoodsDic [_id].Count += _count;
        // TODO 时间待改
        Record (10, _id, _count);
    }

    /// <summary>
    /// 判断仓库加入新物品后是否超重
    /// 可以放入时返回false
    /// </summary>
    /// <param name="_id">要放入的商品ID</param>
    /// <param name="_count">数量</param>
    /// <returns></returns>
    public bool IsFull (int _id, int _count)
    {
        if (_count + Occupy < CurrVol)
            return false;
        return true;
    }

    /// <summary>
    /// 减少商品
    /// </summary>
    /// <param name="_id">商品ID</param>
    /// <param name="_count">数量</param>
    public void ReduceGoods (int _id, int _count)
    {
        if (!HaveEnough (_id, _count))
            return;
        GoodsDic [_id].Count -= _count;
        Occupy -= _count;
        // TODO 时间待改
        Record (10, _id, -_count);
    }

    /// <summary>
    /// 判断是否拥有足够的某种商品
    /// </summary>
    /// <param name="_id">商品ID</param>
    /// <param name="_count">数量</param>
    /// <returns></returns>
    public bool HaveEnough (int _id, int _count)
    {
        if (GoodsDic.ContainsKey (_id) && GoodsDic [_id].Count >= _count)
            return true;
        return false;
    }

    // 记录仓库变化
    private void Record (int _time, int _goodsId, int _change)
    {
        WareRecordList.Add (new WareRecord (_time, _goodsId, _change));
    }

    public class Goods
    {
        public int GoogsId;
        public int Count;

        public Goods (int _id, int _count)
        {
            GoogsId = _id;
            Count = _count;
        }
    }

    public class WareRecord
    {
        public int Time;
        public int GoodsId;
        public int Change;

        public WareRecord (int _time, int _goodsId, int _change)
        {
            Time = _time;
            GoodsId = _goodsId;
            Change = _change;
        }
    }
}
