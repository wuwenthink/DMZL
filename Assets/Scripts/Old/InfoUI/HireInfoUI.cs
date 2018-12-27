// ======================================================================================
// 文 件 名 称：HireInfoUI.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-31 16:01:30
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-31 16:01:30
// ======================================================================================
// 功 能 描 述：UI：雇佣资料
// ======================================================================================
using UnityEngine;

public class HireInfoUI : MonoBehaviour
{
    public GameObject GameObject_RoleInfoIconPos;
    public GameObject Label_WordName;
    public GameObject Label_TitleName;
    public GameObject Label_Gender;
    public GameObject Label_Age;

    public GameObject GameObject_Scroll_Know;
    public GameObject GameObject_Pos_Know;
    public GameObject GameObject_Scroll_Skill;
    public GameObject GameObject_Pos_Skill;
    public GameObject GameObject_MoneyPos;

    public GameObject Button_OK;

    public GameObject Button_Close;
    public GameObject Sprite_Back;


    void Start ()
    {

        ClickControl ();
    }


    void Update ()
    {

    }


    private void ClickControl ()
    {
        UIEventListener.Get (Sprite_Back).onClick = Back;
        UIEventListener.Get (Button_Close).onClick = Back;
    }

    void Back (GameObject btn)
    {

    }


}
