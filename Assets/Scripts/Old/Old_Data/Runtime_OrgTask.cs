// ======================================================================================
// 文 件 名 称：Runtime_OrgTask.cs 
// 版 本 编 号：v1.1.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-20 12:05:54
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-21
// ======================================================================================
// 功 能 描 述：运行时数据：发布的组织任务
// ======================================================================================


public class Runtime_OrgTask : TaskOrg
{
    /// <summary>
    /// 接取人ID
    /// </summary>
    public int ReceiveRoleId { private set; get; }
    /// <summary>
    /// 本月次数
    /// </summary>
    public int PublicCount { private set; get; }
    /// <summary>
    /// 任务状态
    /// </summary>
    public OrgTaskState State { private set; get; }
    /// <summary>
    /// 有效期内
    /// </summary>
    public bool WithinLimit { private set; get; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public int publicTime { private set; get; }



    public Runtime_OrgTask (int _id, int _rceceiveRoleId = 0) : base (_id)
    {
        PublicCount = 1;
        State = _rceceiveRoleId == 0 ? OrgTaskState.Nobody : OrgTaskState.Running;
        WithinLimit = true;
        publicTime = TimeManager.GetTime ();
    }

    /// <summary>
    /// 设置接取人
    /// </summary>
    /// <param name="_rceceiveRoleId">接取人ID（主角是-1）</param>
    public void SetRole (int _rceceiveRoleId)
    {
        ReceiveRoleId = _rceceiveRoleId;
        State = OrgTaskState.Running;
    }

    /// <summary>
    /// 处理任务完成
    /// </summary>
    public void FinishTask ()
    {
        State = OrgTaskState.Finished;
    }

    /// <summary>
    /// 刷新任务
    /// </summary>
    public void Refresh (int _rceceiveRoleId = 0)
    {
        PublicCount++;
        ReceiveRoleId = _rceceiveRoleId;
        if (_rceceiveRoleId == 0)
            State = OrgTaskState.Nobody;
        else
            State = OrgTaskState.Running;
        WithinLimit = true;
    }

    /// <summary>
    /// 设置过期
    /// </summary>
    public void SetOutofLimit ()
    {
        WithinLimit = false;
    }

    public enum OrgTaskState
    {
        Nobody,
        Running,
        Finished
    };
}
