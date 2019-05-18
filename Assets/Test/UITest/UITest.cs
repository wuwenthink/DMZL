using Common;
using DMZL_UI;
using MainSpace;
using UnityEngine;

public class UITest : MonoBehaviour
{
    private void Awake ()
    {
        //加载bundle包
        BundleManager.I.LoadBundle("ui");
        MainSystem.I.Start();
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.I.GetSceneUI<UI_SystemButton>().Show();
        UIManager.I.GetSceneUI<UI_HintButton>().Show();

        UI_TestWindow testWindow= UIManager.I.GetWindow<UI_TestWindow>(4);
        testWindow.Show();
        UIManager.I.GetWindow<UI_TestWindow>(5).Minimize();
        UIManager.I.GetWindow<UI_TestWindow>(6).Show();

        MainSystem.I.SendMessage(MainMessages.SendHint,new VO_Hint(2,HintType.MissionComplete,3,0,false));

    

    }
}
