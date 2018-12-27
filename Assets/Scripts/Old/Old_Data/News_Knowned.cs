// ======================================================================================
// 文件名         ：    News_Knowned.cs
// 版本号         ：    v2.0.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-21
// 最后修改日期   ：    2017-11-18
// ======================================================================================
// 功能描述       ：    已经获知的消息
// ======================================================================================

/// <summary>
/// 已经获知的消息
/// </summary>
public class News_Knowned : News
{
    /// <summary>
    /// 消息id（进行时ID）
    /// </summary>
	public int Id { get; private set; }

    /// <summary>
    /// 获取消息的时间
    /// </summary>
    public int TimeGet { get; private set; }

    /// <summary>
    /// 消息状态
    /// </summary>
    public NewsState State = NewsState.inProgress;

    public News_Knowned (int _id, int _time) : base (_id)
    {
        Id = RunTime_Data.News_Sequence++;
        TimeGet = _time;
    }
}