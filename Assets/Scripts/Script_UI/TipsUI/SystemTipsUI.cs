// ====================================================================================== 
// 文件名         ：    SystemTipsUI.cs                                                         
// 版本号         ：    v1.0.1.0                                                  
// 作者           ：    wuwenthink
// 修改人         ：    wuwenthink                                                          
// 创建日期       ：                                                                      
// 最后修改日期   ：    2017-8-28                                                            
// ====================================================================================== 
// 功能描述       ：    系统提示UI                                                                
// ======================================================================================

using UnityEngine;

public class SystemTipsUI : MonoBehaviour
{
    GameObject Sprite_Back;
    public UIText Label_TipsDesc;

    public GameObject Button_Agree;
    public GameObject Button_Cancel;
    public UIText Label_Agree;
    public UIText Label_Cancel;

    void Start ()
    {
        Sprite_Back = GameObject.Find ("Sprite_Back");

        UIEventListener.Get(Sprite_Back).onClick = back;
        UIEventListener.Get(Button_Cancel).onClick = back;
    }

    void back (GameObject btn)
    {
        this.gameObject.SetActive (false);
    }

    /// <summary>
    /// 设定Tips内容和文字显示
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="des">内容</param>
    /// <param name="yes">确定按钮</param>
    /// <param name="no">取消按钮</param>
    public GameObject[] SetTipDesc (string des,string yes,string no)
    {
        Label_TipsDesc.SetText(false, des);
        Label_Agree.SetText(false, yes);
        Label_Cancel.SetText(false, no);
        GameObject[] games = new GameObject[2];
        games[0] = Button_Agree;
        games[1] = Button_Cancel;
        return games;
    }

}
