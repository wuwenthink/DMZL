// ======================================================================================
// 文 件 名 称：Work_EvaluateUI.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-04 17:26:05
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-21
// ======================================================================================
// 功 能 描 述：UI：职事评价
// ======================================================================================

using UnityEngine;

public class Work_EvaluateUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;

    public GameObject GameObject_RoleInfoIconPos;
    public UIText Label_RoleName;
    public GameObject Button_Nature;
    public UIText Label_Nature;
    public UIText Label_NowState;

    public UISlider ProgressBar_Month;
    public UIText Label_Percent_month;
    public UISlider ProgressBar_All;
    public UIText Label_Percent_all;

    private GameObject lableTips;

    void Start ()
    {
        int depth = gameObject.GetComponent<UIPanel> ().depth;

        foreach (UIPanel u in gameObject.GetComponentsInChildren<UIPanel> ())
        {
            if (u.gameObject != gameObject)
                u.depth = depth + 1;
        }

        lableTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LableTips, transform, Vector3.zero, Vector3.one).gameObject;
        lableTips.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;

        ClickControl ();
        SetAll (102, -1);
    }

    /// <summary>
    /// 设置内容
    /// </summary>
    /// <param name="_shopId">商店ID</param>
    /// <param name="_roleId">被查询员工的角色ID</param>
    public void SetAll (int _shopId, int _roleId)
    {
        var dao = SelectDao.GetDao ();
        var shop = RunTime_Data.ShopDic [_shopId];
        var bossId = 1002;
        if (shop.Post.ContainsKey (303))
            foreach (var id in shop.Post [303].Keys)
                bossId = id;
        else
            bossId = shop.HostId;
        var boss = RunTime_Data.RolePool [bossId];

        Label_RoleName.SetText(false, boss.commonName);

        //var nature = dao.SelecRole_Nature (boss.Nature);
        //Label_Nature.SetText (false, nature.name);
        //lableTips.GetComponent<LableTipsUI> ().SetAll (false, nature.des);
        lableTips.SetActive (false);

        //Label_NowState.SetText (false, Charactor.GetInstance.GetRelationship (bossId).Relationship.name);

        // 本月评价的具体数值
        ProgressBar_Month.value = shop.GetWorker (_roleId).Grade_Month / 100f;
        Label_Percent_month.SetText (false, "数值对应的文字描述");

        // 综合评价的具体数值
        ProgressBar_All.value = shop.GetWorker (_roleId).Grade_All / 100f;
        Label_Percent_all.SetText (false, "数值对应的文字描述");
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get (Button_Nature).onHover = Nature;
    }

    private void Nature (GameObject go, bool state)
    {
        if (state)
        {
            lableTips.SetActive (true);
        }
        else
        {
            lableTips.SetActive (false);
        }
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}