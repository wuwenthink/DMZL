// ======================================================================================
// 文 件 名 称：LoadGameUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-12-15 17:37:21
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-12-15 17:37:21
// ======================================================================================
// 功 能 描 述：UI：读档
// ======================================================================================

using UnityEngine;

public class LoadGameUI : MonoBehaviour
{
    public GameObject Button_Back;
    public GameObject Button_Close;

    public GameObject Button_Fixed;
    public GameObject Button_Auto1;
    public GameObject Button_Auto2;

    public UIText Label_Value_drama;
    public UIText Label_Value_identity;
    public UIText Label_Value_money;
    public UISprite Sprite_map;

    public GameObject Button_OpenSave;

    GameObject chooseBook;
    private void Start ()
    {
        SetList();
        ClickController ();
    }

    public void SetList ()
    {
        Button_Fixed.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
        Button_Fixed.transform.Find("GameObject_Info").Find("Button_RoleIcon").Find("Sprite_RoleInfoIcon").GetComponent<UISprite>().spriteName = "";
        Button_Fixed.transform.transform.Find("GameObject_Info").Find("Label_RoleName").GetComponent<UIText>().SetText(false, "");
        Button_Fixed.transform.transform.Find("GameObject_Info").Find("Label_Place").GetComponent<UIText>().SetText(false, "");
        Button_Fixed.transform.transform.Find("GameObject_Info").Find("Label_Time").GetComponent<UIText>().SetText(false, "");

        Button_Auto1.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
        Button_Auto1.transform.Find("GameObject_Info").Find("Button_RoleIcon").Find("Sprite_RoleInfoIcon").GetComponent<UISprite>().spriteName = "";
        Button_Auto1.transform.transform.Find("GameObject_Info").Find("Label_RoleName").GetComponent<UIText>().SetText(false, "");
        Button_Auto1.transform.transform.Find("GameObject_Info").Find("Label_Place").GetComponent<UIText>().SetText(false, "");
        Button_Auto1.transform.transform.Find("GameObject_Info").Find("Label_Time").GetComponent<UIText>().SetText(false, "");

        Button_Auto2.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
        Button_Auto2.transform.Find("GameObject_Info").Find("Button_RoleIcon").Find("Sprite_RoleInfoIcon").GetComponent<UISprite>().spriteName = "";
        Button_Auto2.transform.transform.Find("GameObject_Info").Find("Label_RoleName").GetComponent<UIText>().SetText(false, "");
        Button_Auto2.transform.transform.Find("GameObject_Info").Find("Label_Place").GetComponent<UIText>().SetText(false, "");
        Button_Auto2.transform.transform.Find("GameObject_Info").Find("Label_Time").GetComponent<UIText>().SetText(false, "");

        Button_Fixed.name = "1";
        Button_Auto1.name = "2";
        Button_Auto2.name = "3";
        ClickBook(Button_Fixed);
    }

    //点击相应存档
    void ClickBook(GameObject btn)
    {
        if (chooseBook != null)
        {
            chooseBook.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = false;
        }
        btn.transform.Find("Sprite_Choose").GetComponent<UISprite>().enabled = true;
        chooseBook = btn;

        SetInfo(int.Parse(btn.name));
    }

    private void SetInfo (int SaveIndex)
    {

        Label_Value_drama.SetText(false, "无记载");
        Label_Value_identity.SetText(false, "无记载");
        Label_Value_money.SetText(false, "无记载");



    }

    private void ClickController ()
    {
        UIEventListener.Get (Button_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;

        UIEventListener.Get(Button_Fixed).onClick = ClickBook;
        UIEventListener.Get(Button_Auto1).onClick = ClickBook;
        UIEventListener.Get(Button_Auto2).onClick = ClickBook;      
    }

    private void Back (GameObject go)
    {
        Destroy (gameObject);
    }
}
