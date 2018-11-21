// ======================================================================================
// 文件名         ：    NewsInfoUI.cs
// 版本号         ：    v1.1.1
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-22
// 最后修改日期   ：    2017-10-02 11:54:14
// ======================================================================================
// 功能描述       ：    消息详细信息UI
// ======================================================================================

using System.Collections;
using UnityEngine;

/// <summary>
/// 消息详细信息UI
/// </summary>
public class NewsInfoUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    
    public UIText Label_NewsName;
    public UIText Lable_DescEX;
    public UIText Label_Time;
    public UIText Label_TimeLimit;
    public UIPanel GameObject_DescScroll;

    
    private Transform UI_Root;

    private NewsStore newsStore;

    // 初始化的方法
    private void Start ()
    {
        ClickController ();

         
        UI_Root = FindObjectOfType<UIRoot> ().transform;       

        int depth = gameObject.GetComponent<UIPanel> ().depth;

        GameObject_DescScroll.depth = depth + 1;
    }


    public void SetInfo (int id)
    {
        newsStore = NewsStore.GetInstance;


        News news = SelectDao.GetDao ().SelectNews (id);

        Label_NewsName.SetText (false, news.name);

        Lable_DescEX.SetText (false, news.des);

        if (newsStore.newsKnowned_Over.ContainsKey (id))
        {
            // TOUPDATE 获知时间应该转化为文字
            Label_Time.SetText (false, newsStore.newsKnowned_Over [id].TimeGet.ToString ());
        }
        else
        {
            // TOUPDATE 获知时间应该转化为文字
            Label_Time.SetText (false, newsStore.newsKnowned_Inprogress [id].TimeGet.ToString ());
        }

    }

    private void ClickController ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }

}