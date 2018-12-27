// ======================================================================================
// 文件名         ：    NewsUI.cs
// 版本号         ：    v2.0.3
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-21
// 最后修改日期   ：    2017-10-31
// ======================================================================================
// 功能描述       ：    消息UI
// ======================================================================================

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息UI
/// </summary>
public class NewsUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;
    public GameObject Button_Title_Now;
    public GameObject Button_Title_Old;
    public Transform GameObject_NewsPos;
    public GameObject Button_NewsEX;
    

    private Transform UI_Root;

    private NewsStore newsStore;

    private SelectDao selectDao;

    // 存储显示出来的新消息
    private List<Transform> NewsGameObjectList = new List<Transform> ();

    // 進行中的消息集合
    private List<Transform> UnderwayNewsList = new List<Transform> ();

    // 過期或忽略的消息集合
    private List<Transform> OverlookNewsList = new List<Transform> ();

    // 记录正在操作的消息對象
    private Transform _selectedNewsObj = null;

    // 初始化的方法
    private void Start ()
    {
        ClickController ();

        CommonFunc.GetInstance.SetUIPanel(gameObject);
        UI_Root = FindObjectOfType<UIRoot> ().transform;

    }

    ///// <summary>
    ///// 顯示新消息（進入的界面）
    ///// </summary>
    //public void SetNew ()
    //{
    //    newsStore = NewsStore.GetInstance;

    //    selectDao = SelectDao.GetDao ();

    //    if (!Button_NIEX.activeSelf)
    //    {
    //        Button_NIEX.SetActive (true);
    //    }

    //    NewsGameObjectList.Clear ();

    //    int num = -1;
    //    foreach (int id in newsStore.newsKnowned_Inprogress.Keys)
    //    {
    //        GameObject newsObj = Instantiate (Button_NIEX);
    //        newsObj.name = id.ToString ();

    //        newsObj.transform.parent = GameObject_NIPos;
    //        newsObj.transform.localScale = Vector3.one;
    //        newsObj.transform.localPosition = new Vector3 (50 * ++num, 0, 0);

    //        newsObj.transform.Find ("Label_NIName").GetComponent<UIText> ().SetText (false, selectDao.SelectNews (id).name);
    //        // TOUPDATE 这里应该显示多久前获知
    //        string str = string.Format (LanguageMgr.GetInstance.GetText ("News_13"), newsStore.newsKnowned_Inprogress [id].TimeGet);
    //        newsObj.transform.Find ("Label_NITime").GetComponent<UIText> ().SetText (false, str);

    //        UIEventListener.Get (newsObj).onClick = Open_ButtonMenu;

    //        NewsGameObjectList.Add (newsObj.transform);
    //    }
    //    Button_NIEX.SetActive (false);
    //}

    // 点击一条新消息后，显示ButtonTips（详情、忽略）
    private void Open_ButtonMenu (GameObject btn)
    {
        _selectedNewsObj = btn.transform;
        Transform buttonTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ButtonTips, UI_Root.transform, Vector3.zero, Vector3.one);
        LanguageMgr lm = LanguageMgr.GetInstance;
        buttonTips.GetComponent<ButtonTipsUI> ().SetAll (true, lm.GetText ("News_6"), lm.GetText ("News_7"));
    }

    private void ClickController ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get(Button_Close).onClick = Back;
        
    }

    // 接收ButtonTips的按钮点击情况
    private void ReceiveButtonState (int clickButtonIndex)
    {
        if (clickButtonIndex == 0)
        // 详情
        {
            ShowNewsDetail (null);
        }
        else if (clickButtonIndex == 1)
        // 忽略
        {

        }
    }

    // 忽略一條消息
    private void OvrelookSelectedNews (int _id)
    {
        newsStore = NewsStore.GetInstance;
        newsStore.OvrelookNews (_id);
    }

    // 显示消息详情
    private void ShowNewsDetail (GameObject btn)
    {
        if (btn)
        {
            _selectedNewsObj = btn.transform;
        }
        Transform newsInfo_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_NewsInfo, UI_Root.transform, Vector3.zero, Vector3.one);
        newsInfo_UI.GetComponent<NewsInfoUI> ().SetInfo (int.Parse (_selectedNewsObj.name));
        newsInfo_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    // 显示所有消息
    private void Open_All (GameObject btn)
    {
        newsStore = NewsStore.GetInstance;
        selectDao = SelectDao.GetDao ();

        // 进行中的消息
        int numNew = -1;
        foreach (News_Knowned news in newsStore.newsKnowned_Inprogress.Values)
        {
            Transform newsObj = CommonFunc.GetInstance.UI_Instantiate (Button_NewsEX.transform, GameObject_NewsPos, new Vector3 (0, -40 * ++numNew, 0), Vector3.one);
            newsObj.name = news.Id.ToString ();

            newsObj.transform.Find ("Label_NewsName").GetComponent<UIText> ().SetText (false, selectDao.SelectNews (news.Id).name);

            UIEventListener.Get (newsObj.gameObject).onClick = ShowNewsDetail;

            UnderwayNewsList.Add (newsObj);

        }

        Button_NewsEX.SetActive (false);
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }
}