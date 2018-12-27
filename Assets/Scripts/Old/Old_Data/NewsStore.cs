// ====================================================================================== 
// 文件名         ：    NewsStore.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-22                                                                  
// 最后修改日期   ：    2017-8-22                                                            
// ====================================================================================== 
// 功能描述       ：    消息存储                                                                  
// ====================================================================================== 

using System.Collections.Generic;

/// <summary>
/// 消息存储
/// </summary>
public class NewsStore
{
    private static NewsStore _instance;
    public static NewsStore GetInstance
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>
    /// 进行中的消息集合
    /// </summary>
    public Dictionary<int, News_Knowned> newsKnowned_Inprogress;
    /// <summary>
    /// 已经忽略或者过期的消息集合
    /// </summary>
    public Dictionary<int, News_Knowned> newsKnowned_Over;

    /// <summary>
    /// 非进行中的消息最大存储量
    /// </summary>
    private int maxCount = 20;

    public NewsStore ()
    {
        _instance = this;
        newsKnowned_Inprogress = new Dictionary<int, News_Knowned> ();
        newsKnowned_Over = new Dictionary<int, News_Knowned> ();
    }


    /// <summary>
    /// 获取一条消息
    /// </summary>
    /// <param name="_id"></param>
    public News_Knowned GetNews (int _id)
    {
        int _time = TimeManager.GetTime ();
        News_Knowned news = new News_Knowned (_id, _time);
        newsKnowned_Inprogress.Add (news.Id, news);
        return news;
    }

    /// <summary>
    /// 统一处理过期的消息
    /// </summary>
    public void SortOutOverdue ()
    {
        foreach (int index in newsKnowned_Inprogress.Keys)
        {
            // TOUPDATE 过期
            if (true)
            {
                newsKnowned_Inprogress [index].State = NewsState.overdue;
                if (newsKnowned_Over.Count + 1 > maxCount)
                    foreach (int i in newsKnowned_Over.Keys)
                    {
                        newsKnowned_Over.Remove (i);
                        break;
                    }
                newsKnowned_Over.Add (index, newsKnowned_Inprogress [index]);
                newsKnowned_Inprogress.Remove (index);
            }
        }
    }

    /// <summary>
    /// 忽略一条消息
    /// </summary>
    /// <param name="_id"></param>
    public void OvrelookNews (int _id)
    {
        newsKnowned_Inprogress [_id].State = NewsState.overlook;
        if (newsKnowned_Over.Count + 1 > maxCount)
            foreach (int i in newsKnowned_Over.Keys)
            {
                newsKnowned_Over.Remove (i);
                break;
            }
        newsKnowned_Over.Add (_id, newsKnowned_Inprogress [_id]);
        newsKnowned_Inprogress.Remove (_id);
    }

    /// <summary>
    /// 加载一条消息（用于读档）
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_time"></param>
    /// <param name="_state"></param>
    public void Load_News (int _id, int _time, int _state)
    {
        News_Knowned news = new News_Knowned (_id, _time);
        news.State = (NewsState) _state;
        if (_state == (int) NewsState.inProgress)
            newsKnowned_Inprogress.Add (_id, news);
        else
            newsKnowned_Over.Add (_id, news);
    }


}
