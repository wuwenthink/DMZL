// ====================================================================================== 
// 文件名         ：    TaskStore.cs                                                         
// 版本号         ：    v1.0.1                                                
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-22                                                                  
// 最后修改日期   ：    2017-11-19                                                 
// ====================================================================================== 
// 功能描述       ：    任务存储                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 任务存储
/// </summary>
public class TaskStore
{
    private static TaskStore _instance;
    public static TaskStore GetInstance
    {
        get
        {
            return _instance;
        }
    }

    public Dictionary<int, Task_Gotton> tasks_gotton;

    public TaskStore ()
    {
        _instance = this;
        tasks_gotton = new Dictionary<int, Task_Gotton> ();
    }

    /// <summary>
    /// 获取一个任务
    /// </summary>
    /// <param name="_id"></param>
    public void GetTask (int _id)
    {
        int _time = TimeManager.GetTime ();
        Task_Gotton task = new Task_Gotton (_id, _time);
        tasks_gotton.Add (_id, task);
    }

    /// <summary>
    /// 完结一个任务
    /// </summary>
    /// <param name="_id"></param>
    public void FinishTask (int _id)
    {
        // 处理结束任务并结算奖励
    }

    /// <summary>
    /// 放弃一个任务
    /// </summary>
    /// <param name="_id"></param>
    public void GiveUpTask (int _id)
    {
        // 处理结束任务并惩罚
    }

    /// <summary>
    /// 加载一条进行中的任务
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_time"></param>
    /// <param name="_rate"></param>
    public void Load_Task (int _id, int _time, int _rate)
    {
        Task_Gotton task = new Task_Gotton (_id, _time);
        task.rate = _rate;
        tasks_gotton.Add (_id, task);

    }
}
