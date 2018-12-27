using UnityEngine;

public class SystemMenuUI : MonoBehaviour
{

    Transform UI_Root;

    public GameObject Button_BackGame;
    public GameObject Button_LoadGame;
    public GameObject Button_SaveGame;
    public GameObject Button_SettingGame;
    public GameObject Button_BackMain;
    public GameObject Button_ExitGame;


    void Start ()
    {

        ClickControl ();
    }


    void Update ()
    {

    }

    //点击响应判断
    void ClickControl ()
    {
        UIEventListener.Get (Button_BackGame).onClick = ToBackGame;
        UIEventListener.Get (Button_LoadGame).onClick = ToLoadGame;
        UIEventListener.Get (Button_SaveGame).onClick = ToSaveGame;
        UIEventListener.Get (Button_SettingGame).onClick = ToSettingGame;
        UIEventListener.Get (Button_BackMain).onClick = ToBackMain;
        UIEventListener.Get (Button_ExitGame).onClick = ToCloseGame;
    }


    //返回游戏
    void ToBackGame (GameObject btn)
    {
        Destroy (this.gameObject);
    }
    //加载游戏进度
    void ToLoadGame (GameObject btn)
    {
        GameObject go= CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_LoadGame, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one).gameObject;
        go.GetComponent<UIPanel>().depth = this.GetComponent<UIPanel>().depth + 10;
    }
    //存储游戏进度
    void ToSaveGame (GameObject btn)
    {
        FindObjectOfType<Save_Game> ().Save (0);

    }
    //游戏设定菜单
    void ToSettingGame (GameObject btn)
    {
        Debug.Log ("设置游戏");
    }
    //回到主菜单
    void ToBackMain (GameObject btn)
    {
        StartCoroutine (CommonFunc.GetInstance.ToSceneLoading (Data_Static.SceneName_Main_Menu));
    }
    //退出游戏
    void ToCloseGame (GameObject btn)
    {
        CommonFunc.GetInstance.ToCloseGame ();
    }
}
