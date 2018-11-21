using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    Transform UI_Root;
    KeyControl KC;

    public GameObject Sprite_Back;
    public GameObject Label_shopName;
    public GameObject Label_CloseTime;
    public GameObject GameObject_ItemListPos;
    public GameObject Button_ItemEX;
    public GameObject Sprite_ItemIcon;
    public GameObject Label_ItemNum;
    public GameObject Sprite_ShopIcon;

    public GameObject Button_BigDeal;
    public GameObject Button_AboutShop;
    public GameObject Button_Close;

    public Transform ItemTips;
    void Start ()
    {

        UI_Root = FindObjectOfType<UIRoot> ().transform;
        KC = FindObjectOfType<KeyControl> ();

        //Tips界面
        ItemTips = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ItemTips, UI_Root, Vector3.one, Vector3.zero);
        ItemTips.gameObject.SetActive (false);

        ClickControl ();
    }

    void Update ()
    {

    }

    public void ShopType (string ID)
    {
        //string type = SD_Shop.Class_Dic [ID].type;
        //int lv = SD_Shop.Class_Dic [ID]._lv ();
        //List<string> itemList = new List<string> ();
        //Dictionary<string, Class_Item> dicc = SD_Item.Class_Dic;
        ////循环遍历item表中的shopType
        //foreach (KeyValuePair<string, Class_Item> dic in dicc)
        //{
        //    if ((dic.Value.shopType == type) && ((int.Parse (dic.Value.shopLv)) <= lv))
        //    {
        //        itemList.Add (dic.Key);
        //    }
        //}
        //for (int i = 0; i < itemList.Count; i++)
        //{
        //    string price = SD_Item.Class_Dic [itemList [i]].price;
        //    CloneItem (price, itemList [i], i);
        //}

        Button_ItemEX.SetActive (false);
    }

    public GameObject CloneItem (string itemPrice, string iconName, int much)
    {
        GameObject item = Instantiate<GameObject> (Button_ItemEX);
        item.name = iconName;
        item.transform.parent = GameObject_ItemListPos.transform;
        item.transform.localScale = Vector3.one;
        int line = Mathf.Abs (much / 7);
        int col = much - (line * 7);
        item.transform.localPosition = new Vector3 (52 * col, 65 * line * (-1), 0);
        foreach (Transform child in item.GetComponentsInChildren<Transform> ())
        {
            if (child.name == "Sprite_ItemIcon")
            {
                child.GetComponentInChildren<UISprite> ().atlas = FindObjectOfType<GameObject_Static> ().UIAtlas_Icon_Item1;
                //child.GetComponentInChildren<UISprite> ().spriteName = SD_Item.Class_Dic [item.name].icon;
            }
        }
        item.GetComponentInChildren<UILabel> ().text = itemPrice;
        UIEventListener.Get (item).onHover = ItemTipsHover;
        return item;
    }



    void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;
    }

    void Back (GameObject btn)
    {
        Destroy (this.gameObject);
        if (ItemTips)
        {
            Destroy (ItemTips.gameObject);
        }
        KC.windowIndex = "begin";
        KC.isAllow = true;
    }

    public void ItemTipsHover (GameObject btn, bool isOver)
    {
        if (isOver)
        {
            ItemTips.gameObject.SetActive (true);
            //string name = SD_Item.Class_Dic [btn.name]._item_name ();
            //string des = SD_Item.Class_Dic [btn.name]._des ();
            //string price = SD_Item.Class_Dic [btn.name].price;
            //            ItemTips.GetComponent<ItemTipsUI>().itemTipsSet(btn.name, name, des, price);
        }
        else
        {
            ItemTips.gameObject.SetActive (false);
            ItemTipsUI [] others = FindObjectsOfType<ItemTipsUI> ();
            for (int i = 0; i < others.Length; i++)
            {
                Destroy (others [i].gameObject);
            }
        }
    }
}
