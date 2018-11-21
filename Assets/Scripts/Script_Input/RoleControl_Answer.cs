using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RoleControl_Answer : MonoBehaviour {
    
    KeyControl KC;
    RoleControl_Move RM;

    //各界面UI脚本控件
    Transform UI_Root;
    GameObject NearTips;

    //角色
    public GameObject Player_Area;
    
    void Awake()
    {

    }

    void Start()
    {
        UI_Root = FindObjectOfType<UIRoot>().transform;
        NearTips = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_NearTips, UI_Root, Vector3.zero, Vector3.one).gameObject;
        NearTips.AddComponent<UIFollow>();
        NearTips.SetActive(false);
    }

    void Update()
    {

    }

    //当触发进入
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("玩家进入碰撞——" + other.gameObject.name);
    }


    //当触发停留
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("玩家停留碰撞—建筑—触发");

        //NearTips.SetActive(true);
        //NearTips.GetComponent<NearTipsUI>().SetMountData(other.GetComponent<NpcMountData>());
        //NearTips.GetComponent<UIFollow>().target = other.gameObject;       

    }

    //当触发退出
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("玩家退出碰撞—建筑—触发");
        NearTips.SetActive(false);
    }


}
