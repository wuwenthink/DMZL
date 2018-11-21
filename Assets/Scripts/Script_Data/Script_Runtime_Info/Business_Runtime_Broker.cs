// ======================================================================================
// 文 件 名 称：Business_Runtime_Broker.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-18 16:46:09
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-18 16:46:09
// ======================================================================================
// 功 能 描 述：运行时数据：牙行
// ======================================================================================

using System.Collections.Generic;

public class Business_Runtime_Broker : Business_Broker
{
    public Business_Runtime_Broker (int _baseId, int _id, int _businessAreaId) : base (_baseId)
    {
        Id = _id;
        BusinessAreaId = _businessAreaId;
        PeopleDic = new Dictionary<int, List<People>> ();
    }

    /// <summary>
    /// 牙行的ID
    /// </summary>
    public int Id { private set; get; }

    /// <summary>
    /// 商业区ID
    /// </summary>
    public int BusinessAreaId;

    /// <summary>
    /// 当日介绍的人(职业身份ID，人员信息)（每天结束后清空）
    /// </summary>
    public Dictionary<int, List<People>> PeopleDic { private set; get; }

    public List<People> GetPeople (int _modelId)
    {
        var comm = CommonFunc.GetInstance;
        if (!PeopleDic.ContainsKey (_modelId))
        {
            PeopleDic.Add (_modelId, new List<People> ());
            for (int i = 0; i < refresh_num; i++)
            {
                //PeopleDic [_modelId].Add (comm.GenerateNpc (_modelId, ElitePercent));
            }
        }
        return PeopleDic [_modelId];
    }
}

public class People
{
    /// <summary>
    /// 人员
    /// </summary>
    public Role_Main role;
    /// <summary>
    /// 是否已经收下
    /// </summary>
    public bool gotten;
    /// <summary>
    /// 薪水
    /// </summary>
    public int salary;

    public People (Role_Main _role, int _salary)
    {
        role = _role;
        gotten = false;
        salary = _salary;
    }
}