// ======================================================================================
// 文件名         ：    MakingUI.cs
// 版本号         ：    v1.1.0
// 作者           ：    wuwenthink
// 修改人         ：    wuwenthink
// 创建日期       ：
// 最后修改日期   ：   2017-10-02 11:16:23
// ======================================================================================
// 功能描述       ：    生产界面UI
// ======================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingUI : MonoBehaviour
{

    private Transform UI_Root;
    
    public GameObject Sprite_Back;
    public GameObject Button_Close;
    
    private ProduceStore produceStore;
    private Dictionary<int, Item> item_All;
    

    private void Start ()
    {
        ClickController ();

        CommonFunc.GetInstance.SetUIPanel(gameObject);
        UI_Root = FindObjectOfType<UIRoot> ().transform;

    }
    
    // 配方记载
    public UILabel Label_MakingDes;

    // 预计收获
    public GameObject Botton_ItemEX;
    public UISprite Sprite_Icon;
    public UIText Label_Num;
    public UIText Label_ItemName;

    // 配方内容
    public GameObject GameObject_Pos_NeedPart;
    public UIText Label_Time_Personal;
    public GameObject GameObject_SkillPos;

    // 订做

    public GameObject Button_Building;
    public UIText Label_BuildingName;
    public UIText Label_Time_Custom;
    public GameObject GameObject_MoneyPos;      

    /// <summary>
    /// 显示详细信息
    /// </summary>
    /// <param name="id"></param>
    public void SetInfo (int id)
    {
        produceStore = ProduceStore.GetInstance;
        item_All = Constants.Items_All;        

        MakeRecipe produce = SelectDao.GetDao ().SelectProduce (id);

        int type = produce.skillType;        

        // 配方记载
        Label_MakingDes.text = produce.des;

        // 预计收获
        Botton_ItemEX.name = produce.composeItemId.ToString ();
        Label_ItemName.SetText(false, produce.composeItemId.ToString());
        Sprite_Icon.spriteName = item_All [produceStore.Store [type] [id].produce.composeItemId].icon;
        Label_Num.SetText(false, produce.composeItemNum.ToString ());
        UIEventListener.Get (Botton_ItemEX).onClick = ClickItem;

        // 配方内容
        if (produce.item_Need != null && produce.Dic_item_Need.Count > 0)
        {
            int num = -1;
            foreach (var str in produce.Dic_item_Need)
            {
                int itemId = str.Key;
                int itemNum = str.Value;


            }
        }

        // TOUPDATE 时间应该转换成文字
        Label_Time_Personal.SetText(false, produce.time.ToString ());

        // 订做
        if (produce.CustomOrg != 0)
        {
            Label_BuildingName.SetText(false, SelectDao.GetDao ().SelectOrganize (produce.CustomOrg).name);
            Button_Building.name = produce.CustomOrg.ToString ();
            UIEventListener.Get (Button_Building).onClick = ClickOrg;
        }

        Label_Time_Custom.SetText(false, produce.CustomTime.ToString ());
    }

    private void ClickOrg (GameObject btn)
    {
        int id = int.Parse (btn.name);
        Transform org = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_OrganizeInfo, UI_Root, Vector3.one, Vector3.zero);
        org.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
        org.GetComponent<OrganizeInfoUI> ().SetInfo (id);
    }

    private void ClickItem (GameObject btn)
    {
        Transform lableTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ItemTips, UI_Root, Vector3.one, Vector3.zero);
        lableTips.GetComponent<ItemTipsUI> ().itemTipsSet (int.Parse (btn.name), true);
        lableTips.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void ClickController ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;               
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }
}