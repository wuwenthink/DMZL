using UnityEngine;

public class GameStart : MonoBehaviour
{

    void Start ()
    {
        //跳转到logo场景

        CommonFunc.GetInstance.ToScence (Data_Static.SceneName_GameInfo_LOGO);
        new Constants().ReadAll();
        new RunTime_Data().Init();
    }



}
