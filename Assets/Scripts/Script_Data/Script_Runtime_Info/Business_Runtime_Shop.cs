// ======================================================================================
// 文 件 名 称：Business_Runtime_Shop.cs
// 版 本 编 号：v1.2.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-12 12:34:25
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-21
// ======================================================================================
// 功 能 描 述：运行时数据：商行
// ======================================================================================

using System.Collections.Generic;

public class Business_Runtime_Shop
{
    /// <summary>
    /// 商店ID
    /// </summary>
    public int Id;

    /// <summary>
    /// 商店名
    /// </summary>
    public string Name;

    /// <summary>
    /// 行当ID
    /// </summary>
    public int TradeId;

    /// <summary>
    /// 店主ID
    /// </summary>
    public int HostId;

    /// <summary>
    /// 商业环境ID
    /// </summary>
    public int BusinessEnveromentId;

    /// <summary>
    /// 商业区ID
    /// </summary>
    public int BusinessAreaId;

    /// <summary>
    /// 售卖的商品集合
    /// </summary>
    public Dictionary<int, Runtime_Goods> GoodsDic;

    /// <summary>
    /// 职位集合
    /// </summary>
    public Dictionary<int, Dictionary<int, Worker>> Post;

    /// <summary>
    /// 总容量
    /// </summary>
    public int Vol;

    /// <summary>
    /// 已占用容量
    /// </summary>
    public int Occupy;

    /// <summary>
    /// 明天是否开店
    /// </summary>
    public bool TomorrowOpen;

    /// <summary>
    /// 开店时间
    /// </summary>
    public int OpenTime;

    /// <summary>
    /// 本月发布的任务(每月清空一次)
    /// </summary>
    public Dictionary<int, Runtime_OrgTask> TaskDic;

    /// <summary>
    /// 委托任务
    /// </summary>
    public Dictionary<int, EntrustTask> EntrustTaskDic;

    // 每次应该刷新任务数目
    private int updateTaskCount = -1;


    public Business_Runtime_Shop (int _id, string _name, int _tradeId, int _hostId, int _businessEnveromentId, int _businessAreaId)
    {
        Id = _id;
        Name = _name;
        TradeId = _tradeId;
        HostId = _hostId;
        BusinessEnveromentId = _businessEnveromentId;
        BusinessAreaId = _businessAreaId;

        Vol = SelectDao.GetDao ().SelectBusiness_Trade (Id).goodsCount;

        GoodsDic = new Dictionary<int, Runtime_Goods> ();

        Post = new Dictionary<int, Dictionary<int, Worker>> ();

        TomorrowOpen = true;
        //OpenTime = int.Parse (SelectDao.GetDao ().SelectSystem_Config (36).value.Split (':') [0]);
        TaskDic = new Dictionary<int, Runtime_OrgTask> ();
        EntrustTaskDic = new Dictionary<int, EntrustTask> ();

        SetUpdateTaskCount ();
    }

    /// <summary>
    /// 增加商品
    /// </summary>
    /// <param name="_id">商品ID</param>
    /// <param name="_count">数量</param>
    public void AddGoods (int _id, int _count)
    {
        if (IsFull (_id, _count))
            return;
        if (!GoodsDic.ContainsKey (_id))
            GoodsDic.Add (_id, new Runtime_Goods (_id, _count));
        else
            GoodsDic [_id].Count += _count;
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
        if (_count + Occupy < Vol)
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

    /// <summary>
    /// 修改价格
    /// </summary>
    /// <param name="_goodsId">商品ID</param>
    /// <param name="_price">修改后的价格</param>
    public void ModiPrice (int _goodsId, int _price)
    {
        if (GoodsDic.ContainsKey (_goodsId))
            GoodsDic [_goodsId].Price = _price;
    }

    /// <summary>
    /// 设置明天休业
    /// </summary>
    public void CloseTomorrow ()
    {
        TomorrowOpen = false;
    }

    /// <summary>
    /// 设置明天开店时间
    /// </summary>
    /// <param name="_time"></param>
    public void SetOpenTime (int _time)
    {
        TomorrowOpen = true;
        OpenTime = _time;
    }

    /// <summary>
    /// 发生职位变化后，每天结束时调用
    /// </summary>
    /// <param name="_temp"></param>
    public void DailyDeal_Employee ()
    {
        Post = Temp_Data.GetInstance.TempEmployee [Id];
    }

    /// <summary>
    /// 发布任务
    /// </summary>
    /// <param name="_id">店铺任务ID</param>
    /// <param name="_receiveRole">接取人ID（主角是-1，可为空）</param>
    public void PublicTask (int _id, int _receiveRole = 0)
    {
        if (!TaskDic.ContainsKey (_id))
            TaskDic.Add (_id, new Runtime_OrgTask (_id, _receiveRole));
        else
            TaskDic [_id].Refresh (_receiveRole);
    }

    /// <summary>
    /// 主角工作的商店刷新每日任务
    /// </summary>
    public void UpdateTask_Daily ()
    {
        var dao = SelectDao.GetDao ();

        SetTaskOutofTime (true);

        var list = dao.SelectRandomTaskOrgByDaily (updateTaskCount, true);
        foreach (var item in list)
        {
            TaskDic.Add (item.id, new Runtime_OrgTask (item.id));
        }
    }

    /// <summary>
    /// 主角工作的商店刷新阶段任务
    /// </summary>
    public void UpdateTask_Stage ()
    {
        var dao = SelectDao.GetDao ();

        SetTaskOutofTime (false);

        var list = dao.SelectRandomTaskOrgByDaily (updateTaskCount, false);
        foreach (var item in list)
        {
            TaskDic.Add (item.id, new Runtime_OrgTask (item.id));
        }
    }

    /// <summary>
    /// 主角工作的商店刷新委托任务
    /// </summary>
    public void UpdateTask_Entrust ()
    {
        EntrustTaskDic.Clear ();
        var list = SelectDao.GetDao ().SelectRandomTaskByType ((int) TaskType.身份职能任务, updateTaskCount);
        foreach (var task in list)
        {
            EntrustTaskDic.Add (task.baseId, new EntrustTask (task.baseId));
        }
    }

    /// <summary>
    /// 按角色ID查找员工
    /// </summary>
    /// <param name="_roleId">角色ID，主角为-1</param>
    /// <returns></returns>
    public Worker GetWorker (int _roleId)
    {
        foreach (var dic in Post.Values)
        {
            foreach (var item in dic)
            {
                if (item.Key == _roleId)
                    return item.Value;
            }
        }
        return null;
    }

    // 设置每次刷新任务数
    private void SetUpdateTaskCount ()
    {
        var dao = SelectDao.GetDao ();
        if (updateTaskCount == -1)
            updateTaskCount = dao.SelectBusiness_Trade (TradeId).taskCount;
    }

    // 刷新任务前，将之前的任务设为过期
    private void SetTaskOutofTime (bool _isDaily)
    {
        foreach (var item in TaskDic.Values)
        {
            if (item.isDaily == _isDaily)
                item.SetOutofLimit ();
        }
    }
}

public class Runtime_Goods
{
    /// <summary>
    /// 商品ID
    /// </summary>
    public int Id;

    /// <summary>
    /// 商品时价
    /// </summary>
    public int Price;

    /// <summary>
    /// 市场价
    /// </summary>
    public int MarketPrice;

    /// <summary>
    /// 剩余商品数量
    /// </summary>
    public int Count;

    public Runtime_Goods (int _id, int _count)
    {
        Id = _id;
        Count = _count;
    }

    public Runtime_Goods (int _id, int _count, int _price)
    {
        Id = _id;
        Count = _count;
        Price = _price;
        MarketPrice = _price;
    }
}

/// <summary>
/// 员工
/// </summary>
public class Worker
{
    /// <summary>
    /// 人物ID
    /// </summary>
    public int Id;
    /// <summary>
    /// 人物名
    /// </summary>
    public string Name;
    /// <summary>
    /// 工资
    /// </summary>
    public int Salary;
    /// <summary>
    /// 总体工作表现
    /// </summary>
    public int Grade_All;
    /// <summary>
    /// 当月工作表现
    /// </summary>
    public int Grade_Month;

    public Worker (int _id, int _salary)
    {
        Id = _id;
        Name = RunTime_Data.RolePool [_id].commonName;
        Salary = _salary;
        Grade_All = 0;
        Grade_Month = 0;
    }
}

/// <summary>
/// 委托任务
/// </summary>
public class EntrustTask : Task
{
    public int publicTime;
    public Runtime_OrgTask.OrgTaskState State;

    public EntrustTask (int _baseId) : base (_baseId)
    {
        publicTime = TimeManager.GetTime ();
        State = Runtime_OrgTask.OrgTaskState.Nobody;
    }


}