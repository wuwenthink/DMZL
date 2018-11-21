// ======================================================================================
// 文件名         ：    RoleInfoUI.cs
// 版本号         ：    v1.2.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-19
// 最后修改日期   ： 2017-10-31
// ======================================================================================
// 功能描述       ：    角色信息UI（NPC）
// ======================================================================================

using UnityEngine;

/// <summary>
/// 角色信息UI（NPC）
/// </summary>
public class RoleInfoUI : MonoBehaviour
{
    public GameObject Button_Close;

    public GameObject Texture_Role;
    public UILabel Label_RoleName;
    public GameObject GameObject_Famous;

    public UIText Label_WordName;
    public UIText Label_TitleName;
    public UIText Label_Gender;
    public UIText Label_Place;
    public UIText Label_Fame;
    public GameObject Button_Force;

    public GameObject Botton_Identity;
    public GameObject Botton_Friend;
    public GameObject Botton_Other;

    public GameObject GameObject_Identity;
    public GameObject GameObject_Friend;
    public GameObject GameObject_Other;

    public Transform GameObject_Pos_Iden;
    public GameObject Button_EX_1;

    public UIText Lable_StoryEX;

    public GameObject Button_Nature;
    public UIText Label_Age;
    public UIText Label_GoodOr;


    private Transform UI_Root;

    // 初始化的方法
    private void Start ()
    {
        ClickController ();


        UI_Root = FindObjectOfType<UIRoot> ().transform;

        int depth = gameObject.GetComponent<UIPanel> ().depth;

        foreach (UIPanel u in gameObject.GetComponentsInChildren<UIPanel> ())
        {
            if (u.gameObject != gameObject)
                u.depth = depth + 1;
        }

        if (RunTime_Data.UI_Pool ["role"] != null)
            Destroy (RunTime_Data.UI_Pool ["role"]);
        RunTime_Data.UI_Pool ["role"] = gameObject;
    }

    private void ClickController ()
    {
        UIEventListener.Get (Button_Close).onClick = Back;
        UIEventListener.Get (Button_Force).onClick = ClickForce;

        UIEventListener.Get (Botton_Identity).onClick = ClickIdentityPage;
        UIEventListener.Get (Botton_Friend).onClick = ClickFriendPage;
        UIEventListener.Get (Botton_Other).onClick = ClickOtherPage;

        UIEventListener.Get (Button_Nature).onClick = ClickNature;
    }

    // TOUPDATE 性格详细界面
    private void ClickNature (GameObject btn)
    {
    }

    private void ClickIdentityPage (GameObject btn)
    {
        Botton_Identity.GetComponent<UIButton> ().enabled = false;
        Botton_Friend.GetComponent<UIButton> ().enabled = true;
        Botton_Other.GetComponent<UIButton> ().enabled = true;

        Botton_Identity.GetComponent<UIButton> ().state = UIButtonColor.State.Disabled;
        Botton_Friend.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Other.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;

        GameObject_Identity.SetActive (true);
        GameObject_Friend.SetActive (false);
        GameObject_Other.SetActive (false);
    }

    private void ClickFriendPage (GameObject btn)
    {
        Botton_Identity.GetComponent<UIButton> ().enabled = true;
        Botton_Friend.GetComponent<UIButton> ().enabled = false;
        Botton_Other.GetComponent<UIButton> ().enabled = true;

        Botton_Identity.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Friend.GetComponent<UIButton> ().state = UIButtonColor.State.Disabled;
        Botton_Other.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;

        GameObject_Identity.SetActive (false);
        GameObject_Friend.SetActive (true);
        GameObject_Other.SetActive (false);
    }

    private void ClickOtherPage (GameObject btn)
    {
        Botton_Identity.GetComponent<UIButton> ().enabled = true;
        Botton_Friend.GetComponent<UIButton> ().enabled = true;
        Botton_Other.GetComponent<UIButton> ().enabled = false;

        Botton_Identity.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Friend.GetComponent<UIButton> ().state = UIButtonColor.State.Normal;
        Botton_Other.GetComponent<UIButton> ().state = UIButtonColor.State.Disabled;

        GameObject_Identity.SetActive (false);
        GameObject_Friend.SetActive (false);
        GameObject_Other.SetActive (true);
    }

    private void ClickIdentity (GameObject btn)
    {
        int id = int.Parse (btn.name);
        Transform iden_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_IdentityInfo, UI_Root, Vector3.one, Vector3.zero);
        iden_UI.GetComponent<IdentityInfoUI> ().SetInfo (id);
        iden_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void ClickForce (GameObject btn)
    {
        int id = int.Parse (btn.name);
        Transform force_UI = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_ForceInfo, UI_Root, Vector3.one, Vector3.zero);
        force_UI.GetComponent<ForceInfoUI> ().SetInfo (id);
        force_UI.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 10;
    }

    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }

    public void SetInfo (int _id)
    {
        // 获取角色
        Role_Main role = RunTime_Data.RolePool [_id];

        // 图像、名字、是否历史名人
        // Texture_Role下面显示待机动画

        Label_RoleName.text = role.commonName;

        if (role.famous == 1)
            GameObject_Famous.SetActive (true);
        else
            GameObject_Famous.SetActive (false);

        // 详细信息
        Label_Gender.SetText(false, role.gender);
        Label_Place.SetText(false, role.place);
        Label_Fame.SetText(false, role.shengyu.ToString ());

        // TOUPDATE 隶属势力
        //Button_Force.GetComponentInChildren<UISprite>().atlas = FindObjectOfType<GameObject_Static>().UIAtlas_Icon_Item1;
        //Button_Force.GetComponentInChildren<UISprite>().spriteName = Constants.Constants.Force_All[org.force].icon;
        //Button_Force.name = SelectDao.GetDao ().SelectForce (role.Force).name;

        // 身份



        Button_EX_1.SetActive (false);

        // 传记
        Lable_StoryEX.SetText(false, role.story);

        // 性格、年龄、善恶
        // TOUPDATE 年龄、善恶应该经过转换
        //Button_Nature.transform.Find ("Label_Nature").GetComponent<UILabel> ().text = role.Nature.ToString ();
        // TOUPDATE 这里这个按钮的名字应该改为性格id

        Label_Age.SetText(false, role.birthday.ToString ());
        //Label_GoodOr.SetText(false, role.GoodOrEvil.ToString ());
    }

}