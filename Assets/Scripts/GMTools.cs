// ======================================================================================
// 文 件 名 称：GMTools.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：Administrator
// 创 建 时 间：2018-08-03 20:50:44
// 最 后 修 改：Administrator
// 更 新 时 间：2018-08-03 20:50:44
// ======================================================================================
// 功 能 描 述：GM工具
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMTools : MonoBehaviour {
    public GameObject Sprite_back;
    public GameObject Button_ChangeProp;
    public GameObject Button_ItemAdd;
    public GameObject Label_ex;
    public Transform GameObject_Pos;

    Bag bag;
    void Start () {
        UIEventListener.Get(Button_ChangeProp).onClick = ChangeProp;
        UIEventListener.Get(Button_ItemAdd).onClick = AddItem;
        UIEventListener.Get(Sprite_back).onClick = back;

        bag = Bag.GetInstance;
    }

	void Update () {
		
	}

    void ChangeProp(GameObject btn)
    {

    }

    void AddItem(GameObject btn)
    {
        Label_ex.SetActive(true);
        foreach (var item in Constants.Items_All)
        {
            int count = 0;
            if (bag.HaveItem(item.Key))
            {

                count = bag.itemDic[item.Key].count;
            }
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Label_ex.transform, GameObject_Pos, Vector3.zero, Vector3.one);
            go.GetComponent<UILabel>().text = item.Value.name+"____"+ count;
            GameObject add = go.Find("Button_Add").gameObject;
            GameObject red = go.Find("Button_Re").gameObject;
            UIEventListener.Get(add).onClick = Add_Item;
            UIEventListener.Get(red).onClick = Red_Item;
            add.name = item.Key.ToString();
            red.name = item.Key.ToString();
            GameObject_Pos.GetComponent<UIGrid>().enabled = true;
        }
        Label_ex.SetActive(false);
    }


    void Add_Item(GameObject btn)
    {
        int id = int.Parse(btn.name);
        bag.GetItem(id, 1);

        NGUITools.DestroyChildren(GameObject_Pos);
        AddItem(Button_ItemAdd);
    }
    void Red_Item(GameObject btn)
    {
        int id = int.Parse(btn.name);
        bag.GetItem(id, 10);

        NGUITools.DestroyChildren(GameObject_Pos);
        AddItem(Button_ItemAdd);
    }

    void Add_Prop(GameObject btn)
    {

    }
    void Red_Prop(GameObject btn)
    {

    }

    void back(GameObject btn)
    {
        Destroy(this.gameObject);
    }
}
