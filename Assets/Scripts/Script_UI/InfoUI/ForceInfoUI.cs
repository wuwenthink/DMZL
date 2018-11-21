// ======================================================================================
// 文件名         ：    ForceInfoUI.cs
// 版本号         ：    v1.0.1
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-19
// 最后修改日期   ：    2017-10-31
// ======================================================================================
// 功能描述       ：    势力信息UI
// ======================================================================================

using UnityEngine;

/// <summary>
/// 势力信息UI
/// </summary>
public class ForceInfoUI : MonoBehaviour
{

    public GameObject Button_Close;

    public UILabel Label_ForceName;
    public UISprite Sprite_ForceIcon;
    public UILabel Label_Up;

    public UILabel Lable_AreaEX;

    public UILabel Lable_HistoryEX;

    public UILabel Label_Capital;
    public GameObject Button_Leader;

    public Transform GameObject_WarForcePos;
    public GameObject Button_WarForceEX;

    public UIPanel GameObject_AreaScroll;
    public UIPanel GameObject_HistoryScroll;


    private Transform UI_Root;

    // 初始化的方法
    private void Start ()
    {
        ClickController ();


        UI_Root = FindObjectOfType<UIRoot> ().transform;

        int depth = gameObject.GetComponent<UIPanel> ().depth;
        GameObject_AreaScroll.depth = depth + 1;
        GameObject_HistoryScroll.depth = depth + 1;

        if (RunTime_Data.UI_Pool ["force"] != null)
            Destroy (RunTime_Data.UI_Pool ["force"]);
        RunTime_Data.UI_Pool ["force"] = gameObject;
    }

    private void ClickController ()
    {
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Leader).onClick = ClickRole;
    }

    private void ClickRole (GameObject btn)
    {
        int id = int.Parse (btn.name);
        GameObject role_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_RoleInfo, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        role_UI.GetComponent<RoleInfoUI> ().SetInfo (id);
        role_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void ClickForce (GameObject btn)
    {
        int id = int.Parse (btn.name);
        GameObject force_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ForceInfo, UI_Root.transform, Vector3.one, Vector3.zero).gameObject;
        force_UI.GetComponent<ForceInfoUI> ().SetInfo (id);
        force_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }

    public void SetInfo (int id)
    {
        Force force = SelectDao.GetDao ().SelectForce (id);

        // 势力名称
        Label_ForceName.text = force.name;

        // 势力图标
        // TOUPDATE 图集应该修改
        //Sprite_ForceIcon.atlas = FindObjectOfType<GameObject_Static>().UIAtlas_Icon_Item1;
        //Sprite_ForceIcon.spriteName = force.icon;

        // 类型
        Label_Up.text = force.type;

        // 疆域
        Lable_AreaEX.text = force.geography;

        // 历史
        Lable_HistoryEX.text = force.history;

        // 首都
        Label_Capital.text = force.capital;

        //// 首领
        //if (force.runtimeProp.leader == 0)
        //{
        //    Button_Leader.GetComponent<UIButton> ().enabled = false;
        //    Button_Leader.GetComponentInChildren<UILabel> ().text = LanguageMgr.GetInstance.GetText ("Nomal_10");
        //}
        //else
        //{
        //    Button_Leader.GetComponentInChildren<UILabel> ().text = RunTime_Data.RolePool [force.runtimeProp.leader].CommonName;
        //    Button_Leader.name = force.runtimeProp.leader.ToString ();
        //}

        //// 交战势力
        //int num = -1;
        //if (force.runtimeProp.fight != null)
        //{
        //    foreach (int _id in force.runtimeProp.fight)
        //    {
        //        GameObject clone = Instantiate (Button_WarForceEX);
        //        clone.name = _id.ToString ();

        //        //clone.GetComponentInChildren<UISprite>().atlas = ;
        //        //clone.GetComponentInChildren<UISprite>().spriteName = force.icon;

        //        clone.transform.parent = GameObject_WarForcePos;
        //        clone.transform.localScale = Vector3.one;
        //        clone.transform.localPosition = new Vector3 (60 * (++num % 4), -40 * (num / 4), 0);

        //        UIEventListener.Get (clone).onClick = ClickForce;
        //    }
            
        //}
        //else
        //{

        //}

        Button_WarForceEX.SetActive (false);
    }

}