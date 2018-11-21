using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesTipsUI : MonoBehaviour {
    
    public GameObject GameObject_DesList;
    public GameObject Label_StoryDesLable;
    public GameObject Label_DesTitle;
    public GameObject Button_DesBack;

    void Start () {
        CommonFunc.GetInstance.SetUIPanel(gameObject);

    }
	

	void Update () {
		
	}

    /// <summary>
    /// 说明文本界面
    /// </summary>
    /// <param name="Title">标题</param>
    /// <param name="Lable">内容</param>
    public void setDes(string Title,string Lable)
    {
        Button_DesBack.SetActive(true);
        Label_DesTitle.GetComponent<UILabel>().text = Title;
        Label_StoryDesLable.GetComponent<UILabel>().text = Lable;

        UIEventListener.Get(Button_DesBack).onClick = CloseDes;
    }

    void CloseDes(GameObject btn)
    {
        Destroy(gameObject);
    }
}
