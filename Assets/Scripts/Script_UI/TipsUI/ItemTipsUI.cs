// ====================================================================================== 
// 文件名         ：    ItemTipsUI.cs                                                         
// 版本号         ：    v1.1.0                                              
// 作者           ：    wuwenthink
// 修改人         ：    wuwenthink                                                          
// 创建日期       ：                                                                      
// 最后修改日期   ：    2017-11-1                                     
// ====================================================================================== 
// 功能描述       ：    道具详细信息的通用UI                                                                
// ======================================================================================

using UnityEngine;

public class ItemTipsUI : MonoBehaviour
{
    public UIText Label_ItemName;
    public UIText Label_ItemDes;
    public UIText Label_Size;
    public UISprite Sprite_ItemIcon;
    public GameObject GameObject_MoneyPos;

    public GameObject Sprite_Back;
    public GameObject Label_CloseTips;

    public Camera guiCamera;
    Vector3 offset;

    Transform money;

    bool isClicked = true;
    Bag bag;

    void Awake ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;

        bag = Bag.GetInstance;
        money = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, GameObject_MoneyPos.transform, Vector3.zero, Vector3.one);

    }

    void Start ()
    {
        offset = new Vector3 (0.48f, -0.2f, 0);

        CommonFunc.GetInstance.SetUIPanel(gameObject);

    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }

    public void LateUpdate ()
    {
        if (!isClicked)
        {
            Vector3 pos = NGUITools.FindCameraForLayer (FindObjectOfType<UIRoot> ().gameObject.layer).ScreenToWorldPoint (Input.mousePosition);
            this.gameObject.transform.position = pos + offset;
        }
    }

    /// <summary>
    /// 给itemTips的UI相关信息赋值
    /// </summary>
    /// <param name="id">道具ID</param>
    /// <param name="isClicked">点击？划过？</param>
    public void itemTipsSet (int id, bool _isClicked)
    {
        isClicked = _isClicked;
        Sprite_Back.SetActive (_isClicked);
        Label_CloseTips.SetActive (_isClicked);

        Item item = Constants.Items_All [id];

        money.GetComponent<Price_MoneyEXUI>().SetMoney(Constants.Items_All[id].price);
        
        Sprite_ItemIcon.spriteName = item.icon;
        Label_ItemName.SetText (false, item.name);
        Label_ItemDes.SetText (false, item.des);
        Label_Size.SetText (false, bag.itemDic[id].count.ToString ());
    }

}
