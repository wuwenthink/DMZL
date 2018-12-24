// ======================================================================================
// 文件名         ：    BagUI.cs
// 版本号         ：    v2.4.0
// 作者           ：    wuwenthink
// 修改人         ：    wuwenthink
// 创建日期       ：
// 最后修改日期   ：    2017-10-25 21:35
// ======================================================================================
// 功能描述       ：    背包的UI
// ======================================================================================

using System.Text;
using UnityEngine;

/// <summary>
/// 背包UI
/// </summary>
public class BagUI : MonoBehaviour
{
    public GameObject Sprite_Back;
    public GameObject Button_Close;
    public GameObject GameObject_Money;
    public GameObject GameObject_ScrollList;
    public GameObject GameObject_ItemListPos;
    public GameObject Button_Clear;

    public UISlider ProgressBa_Size;
    public UIText Label_SizeNum;

    public GameObject GameObject_ItemTips;
    public UISprite Sprite_ItemIcon;
    public UILabel Label_ItemName;
    public GameObject Button_Use;
    public UIText Label_Use;
    public GameObject Button_Throw;
    public UILabel Label_ItemNum;
    public GameObject Sprite_BGBOX;

    public UIInput Input_InputGetNum;
    public GameObject Button_Reduce;
    public GameObject Button_Add;    

    private Transform UI_Root;
    private KeyControl KC;

    private Transform lableTips;
    private GameObject systemTips;

    private Bag bag;
    GameObject chooseGO;

    private void Start ()
    {
        UI_Root = FindObjectOfType<UIRoot> ().transform;
        KC = FindObjectOfType<KeyControl> ();
        GameObject_ItemTips.SetActive (false);

        lableTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ItemTips, UI_Root, Vector3.zero, Vector3.one);
        lableTips.gameObject.SetActive (false);

        systemTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_SystemTips, UI_Root, Vector3.zero, Vector3.one).gameObject;
        systemTips.SetActive (false);

        ClickControl ();

    }

    /// <summary>
    /// 显示背包内容
    /// </summary>
    public void ShowBag ()
    {
        bag = Bag.GetInstance;
        
        int much = -1;
        StringBuilder objectName = new StringBuilder ();
        foreach (int id in bag.itemDic.Keys)
        {
            // 可以叠加的情况
            // 按每格叠加上限数量分开显示
            for (int i = 0; i < bag.itemDic [id].count; i += bag.MaxOverlay_Item)
            {
                GameObject item = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_Button_ItemEX, GameObject_ItemListPos.transform, Vector3.zero, Vector3.one).gameObject;

                // 清空StringBuilder
                objectName.Length = 0;
                // 拼接对象名：道具id_编号
                objectName.Append (id);
                objectName.Append ("_");
                objectName.Append (i);
                // 对象名赋值
                item.name = objectName.ToString ();
                item.GetComponent<Botton_ItemEXUI>().Show_Part(true, false, false, false);
                if (bag.itemDic[id].count - i <= bag.MaxOverlay_Item)
                    item.GetComponent<Botton_ItemEXUI>().SetItem(id, bag.itemDic[id].count - i);
                else
                    item.GetComponent<Botton_ItemEXUI>().SetItem(id, bag.MaxOverlay_Item);

                much++;
                int line = Mathf.FloorToInt (much / 7);
                int col = much - (line * 7);
                item.transform.localPosition = new Vector3 (65 * col, 65 * line * (-1), 0);

                UIEventListener.Get (item).onClick = ItemClick;
                UIEventListener.Get (item).onHover = LableTipsHover;
            }
        }

        objectName = null;
        
        UIEventListener.Get (Button_Clear).onClick = Sort_Items;
    }

    /// <summary>
    /// 整理背包
    /// </summary>
    private void Sort_Items ()
    {
        bag = Bag.GetInstance;
        bag.SortBag ();

        foreach (Transform child in GameObject_ItemListPos.transform)
        {
            child.gameObject.tag = "Clear";
        }

        StringBuilder objectName = new StringBuilder ();

        int much = -1;

        foreach (int id in bag.itemDic.Keys)
        {

            for (int i = 0; i < bag.itemDic [id].count; i += bag.MaxOverlay_Item)
            {
                // 清空StringBuilder
                objectName.Length = 0;
                // 拼接对象名：道具id_编号
                objectName.Append (id);
                objectName.Append ("_");
                objectName.Append (i);

                Transform item = GameObject_ItemListPos.transform.Find (objectName.ToString ());
                item.tag = "Untagged";

                if (bag.itemDic [id].count - i <= bag.MaxOverlay_Item)
                    item.Find ("Label_ItemNum").GetComponent<UILabel> ().text = (bag.itemDic [id].count - i).ToString ();
                else
                    item.Find ("Label_ItemNum").GetComponent<UILabel> ().text = (bag.MaxOverlay_Item.ToString ());

                much++;
                int line = Mathf.FloorToInt (much / 6);
                int col = much - (line * 6);
                item.localPosition = new Vector3 (40 * col, 48 * line * (-1), 0);
            }

        }

        objectName = null;

        foreach (Transform child in GameObject_ItemListPos.transform)
        {
            if (child.gameObject.tag == "Clear")
                Destroy (child.gameObject);
        }
    }

    /// <summary>
    /// 整理背包
    /// </summary>
    private void Sort_Items (GameObject btn)
    {
        Sort_Items ();
    }

    /// <summary>
    /// 点击道具
    /// </summary>
    /// <param name="btn"></param>
    private void ItemClick (GameObject btn)
    {
        // 获取道具id
        Bag bag = Bag.GetInstance;


        chooseGO = btn;
        int id = int.Parse (btn.name.Split ('_') [0]);

        // 记录操作的道具gameObject的名字
        GameObject_ItemTips.name = btn.name;

        // 显示道具图片和名称
        GameObject_ItemTips.SetActive (true);
        Sprite_ItemIcon.spriteName = bag.itemDic [id].item.icon;
        Label_ItemName.text = bag.itemDic [id].item.name;

        // 显示道具数量
        Label_ItemNum.text = bag.itemDic[id].count.ToString();

        // 设置输入框和增减按钮
        Input_InputGetNum.value = "1";
        EventDelegate.Add (Input_InputGetNum.onChange, input_num);
        UIEventListener.Get (Button_Add).onClick = add_num;
        UIEventListener.Get (Button_Reduce).onClick = reduce_num;
        Button_Throw.name = id.ToString();
        UIEventListener.Get(Button_Throw).onClick = item_Throw;
        UIEventListener.Get(Button_Use).onClick = item_Use;
        // 设置使用、丢弃按钮
        if (bag.itemDic [id].item.type == 0)
        {
            Button_Use.SetActive (false);
            Button_Throw.SetActive (false);
        }
        else
        {
            if (bag.itemDic [id].item.canUse)
            {
                Button_Use.gameObject.SetActive (true);
                Button_Throw.SetActive (true);
                Button_Use.transform.localPosition = new Vector3 (162, 21);
                Button_Throw.transform.localPosition = new Vector3 (162, -23);
                UIEventListener.Get (Button_Use).onClick = item_Use;
            }
            else
            {
                Button_Use.gameObject.SetActive (false);
                Button_Throw.SetActive (true);
                Button_Throw.transform.localPosition = new Vector3 (162, 0);
            }
        }

    }

    /// <summary>
    /// 鼠标划过道具
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="isOver"></param>
    public void LableTipsHover (GameObject btn, bool isOver)
    {
        if (isOver)
        {
            lableTips.gameObject.SetActive (true);
            int id = int.Parse (btn.name.Split ('_') [0]);
            lableTips.GetComponent<ItemTipsUI> ().itemTipsSet (id, false);
        }
        else
        {
            lableTips.gameObject.SetActive (false);
        }
    }

    /// <summary>
    /// 监听输入框
    /// </summary>
    /// <param name="input"></param>
    /// <param name="selected"></param>
    private void input_num ()
    {
        int num;
        if (int.TryParse(Input_InputGetNum.value,out num))
        {
            if (IsPositiveInteger(Input_InputGetNum.value))
            {
                int value = int.Parse(Input_InputGetNum.value);
                if (value > int.Parse(Label_ItemNum.text))
                    Input_InputGetNum.value = Label_ItemNum.text;
                if (value <= 0)
                    Input_InputGetNum.value = "1";
            }
            else
            {
                Input_InputGetNum.value = "1";
            }
        }
        else
        {
            Input_InputGetNum.value = "1";
        }
    }

    /// <summary>
    /// 检测输入是否为正整数
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private bool IsPositiveInteger (string text)
    {
        char [] charArray = text.ToCharArray ();
        foreach (char ch in charArray)
        {
            if (ch < '0' || ch > '9')
                return false;
        }
        return true;
    }

    /// <summary>
    /// 输入-增加按钮
    /// </summary>
    /// <param name="btn"></param>
    private void add_num (GameObject btn)
    {
        int input = int.Parse (Input_InputGetNum.value);
        int max = int.Parse (Label_ItemNum.text);
        if (input < max)
            Input_InputGetNum.value = input + 1 + "";
    }

    /// <summary>
    /// 输入-减少按钮
    /// </summary>
    /// <param name="btn"></param>
    private void reduce_num (GameObject btn)
    {
        int input = int.Parse (Input_InputGetNum.value);
        if (input > 1)
            Input_InputGetNum.value = input - 1 + "";
    }

    /// <summary>
    /// 道具-使用
    /// </summary>
    /// <param name="btn"></param>
    private void item_Use (GameObject btn)
    {

    }

    /// <summary>
    /// 道具-丢弃
    /// </summary>
    /// <param name="btn"></param>
    private void item_Throw (GameObject btn)
    {
        // 获取数量
        int num = int.Parse (Input_InputGetNum.value);

        systemTips.SetActive (true);
        systemTips.GetComponent<SystemTipsUI> ().SetTipDesc (
            LanguageMgr.GetInstance.GetText("Bag_8") + Label_ItemName.text + " X " + num, 
            LanguageMgr.GetInstance.GetText("Tips_5"), LanguageMgr.GetInstance.GetText("Tips_6"));
        GameObject[] gameObjects = new GameObject[2];
        gameObjects[0].name = btn.name;
        UIEventListener.Get(gameObjects[0]).onClick = systemYes;
        UIEventListener.Get(gameObjects[1]).onClick = systemNo;
    }
    void systemYes(GameObject btn)
    {
        // 隐藏提示窗口
        systemTips.SetActive(false);
        // 获取丢弃和拥有数量
        int value = int.Parse(Input_InputGetNum.value);
        int have = int.Parse(Label_ItemNum.text);
        int id = int.Parse(btn.name);
        // 丢弃
        bag.LoseItem(chooseGO.name, value);

        // 修改背包面板
        if (value >= have)
            PoolInstance.Instance.Despawn(chooseGO.transform);
        else
        {
            Label_ItemNum.text = (have - value).ToString();
            chooseGO.GetComponent<Botton_ItemEXUI>().SetItem(id, have - value);
        }
    }
    void systemNo(GameObject btn)
    {
        // 隐藏提示窗口
        systemTips.SetActive(false);
    }

    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;
        UIEventListener.Get (Sprite_BGBOX).onClick = TipsBack;

        //UIEventListener.Get(FindObjectOfType<SystemTipsUI>().Button_Agree).onClick = ClickAgree;
    }

    private void TipsBack (GameObject btn)
    {
        GameObject_ItemTips.SetActive (false);
    }

    public void Back (GameObject btn)
    {
        Destroy (this.gameObject);
        if (lableTips.gameObject)
        {
            Destroy (lableTips.gameObject);
        }
        if (systemTips)
        {
            Destroy (systemTips);
        }
        KC.windowIndex = "begin";
        KC.isAllow = true;
    }
}