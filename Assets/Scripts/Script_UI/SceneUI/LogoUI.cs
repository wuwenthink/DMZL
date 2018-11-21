using System.Collections;
using UnityEngine;

public class LogoUI : MonoBehaviour
{

    public GameObject GameObject_Logo;
    public GameObject Sprite_Logo;
    public GameObject Label_XuYan;
    public GameObject Button_EnterMainMenu;
    public GameObject Label_GameIndex;


    public GameObject GameInfo;

    public GameObject Button_TestUI;//测试UI入口
    public GameObject Button_TheWorld;//测试场景入口
    public GameObject Button_DesignPattern;//创建场景入口


    //进入测试UI场景
    void ToTestUI (GameObject btn)
    {
        StartCoroutine (CommonFunc.GetInstance.ToSceneLoading (Data_Static.SceneName_TestUI));
    }

    //进入创建场景
    void ToDesignPattern(GameObject btn)
    {
        StartCoroutine(CommonFunc.GetInstance.ToSceneLoading(Data_Static.SceneName_DesignPattern));
    }

    //进入测试世界场景
    void ToTheWorld(GameObject btn)
    {
        FindObjectOfType<Load_Drama>().Load_OneDrama(1, 10090);//赵钱
    }


    void Start ()
    {
        UIEventListener.Get (Button_TestUI).onClick = ToTestUI;
        UIEventListener.Get (Button_TheWorld).onClick = ToTheWorld;
        UIEventListener.Get (Button_DesignPattern).onClick = ToDesignPattern;

        gameObject.GetComponent<AudioSource> ().Pause ();
        GameInfo.SetActive (false);
        Label_XuYan.SetActive (false);

        Invoke ("s_LogoDisappear", Sprite_Logo.GetComponent<TweenAlpha> ().duration);
        StartCoroutine (LableIndex ());

        ClickControl ();
    }



    void Update ()
    {
    }

    //LOGO消失后处理
    void s_LogoDisappear ()
    {
        Sprite_Logo.SetActive (false);
        Label_XuYan.GetComponent<UILabel> ().text = "  " + SD_WordTranslate.Class_Dic ["Long1"].chinese.Replace ("\"", "");
        Label_XuYan.SetActive (true);
        gameObject.GetComponent<AudioSource> ().Play ();
    }

    void LogoDisappear ()
    {
        GameObject_Logo.SetActive (false);
        GameInfo.SetActive (true);
    }
    IEnumerator LogoDisappear2 ()
    {
        Label_XuYan.GetComponent<TweenAlpha> ().enabled = true;
        Label_XuYan.GetComponent<TweenAlpha> ().PlayForward ();
        yield return new WaitForSeconds (Label_XuYan.GetComponent<TweenAlpha> ().duration + 0.2f);
        GameObject_Logo.SetActive (false);
        GameInfo.SetActive (true);
    }
    void l_LogoDisappear (GameObject btn)
    {
        StartCoroutine (LogoDisappear2 ());
    }

    IEnumerator LableIndex ()
    {
        Label_GameIndex.GetComponent<UILabel> ().text = SD_WordTranslate.Class_Dic ["Long2"].chinese.Replace ("\"", "");
        yield return new WaitForSeconds (20);
        Label_GameIndex.GetComponent<AudioSource> ().Stop ();
    }


    //点击响应判断
    void ClickControl ()
    {
        UIEventListener.Get (Label_XuYan).onClick = l_LogoDisappear;
        UIEventListener.Get (Button_EnterMainMenu).onClick = ToScenceMainMenu;
    }

    //前往主菜单场景
    void ToScenceMainMenu (GameObject btn)
    {
        StartCoroutine (CommonFunc.GetInstance.ToSceneLoading (Data_Static.SceneName_Main_Menu));
    }

}
