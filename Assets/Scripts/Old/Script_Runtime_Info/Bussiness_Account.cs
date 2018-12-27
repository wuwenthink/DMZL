// ======================================================================================
// 文 件 名 称：Bussiness_Account.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-12 15:43:19
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-12 15:43:19
// ======================================================================================
// 功 能 描 述：账本
// ======================================================================================


using System.Collections.Generic;

public class Bussiness_Account
{
    private static Bussiness_Account _instance;
    public static Bussiness_Account GetInstance
    {
        get
        {
            return _instance;
        }
    }

    public Bussiness_Account ()
    {
        _instance = this;
        IncomeList = new List<Clause> ();
        ExpenseList = new List<Clause> ();
    }

    /// <summary>
    /// 收入记录
    /// </summary>
    public List<Clause> IncomeList { private set; get; }
    /// <summary>
    /// 支出记录
    /// </summary>
    public List<Clause> ExpenseList { private set; get; }

    /// <summary>
    /// 记录收入
    /// </summary>
    /// <param name="_time">时间</param>
    /// <param name="_event">事件描述</param>
    /// <param name="_cost">费用</param>
    public void RecordIncome (int _time, string _event, int _cost)
    {
        IncomeList.Add (new Clause (_time, _event, _cost));
    }

    /// <summary>
    /// 记录支出
    /// </summary>
    /// <param name="_time">时间</param>
    /// <param name="_event">事件描述</param>
    /// <param name="_cost">费用</param>
    public void RecordExpense (int _time, string _event, int _cost)
    {
        ExpenseList.Add (new Clause (_time, _event, _cost));
    }

    /// <summary>
    /// 清空所有记录
    /// </summary>
    public void ClearAll ()
    {
        IncomeList.Clear ();
        ExpenseList.Clear ();
    }

}

/// <summary>
///  账本的条目
/// </summary>
public class Clause
{
    /// <summary>
    /// 时间
    /// </summary>
    public int Time;
    /// <summary>
    /// 事件描述
    /// </summary>
    public string Event;
    /// <summary>
    /// 费用
    /// </summary>
    public int Cost;

    public Clause (int _time, string _event, int _cost)
    {
        Time = _time;
        Event = _event;
        Cost = _cost;
    }
}
