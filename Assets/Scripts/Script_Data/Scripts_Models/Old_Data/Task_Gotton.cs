// ====================================================================================== 
// 文件名         ：    Task_Gotton.cs                                                         
// 版本号         ：    v2.0.0                                                
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-22                                                                  
// 最后修改日期   ：    2017-11-19                                                       
// ====================================================================================== 
// 功能描述       ：    进行中的任务                                                                  
// ====================================================================================== 

/// <summary>
/// 进行中的任务
/// </summary>
public class Task_Gotton : Task
{
    /// <summary>
    /// 任务id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 任务获得时间
    /// </summary>
    public int timeGet { get; private set; }
    /// <summary>
    /// 任务进度0-100
    /// </summary>
    public int rate { get; set; }

    public Task_Gotton (int _baseId, int _time) : base (_baseId)
    {
        id = RunTime_Data.Task_Sequence++;
        timeGet = _time;
        rate = 0;
    }


}
