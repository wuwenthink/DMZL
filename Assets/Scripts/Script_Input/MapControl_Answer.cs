// ======================================================================================
// 文 件 名 称：MapControl_Answer.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：Administrator
// 创 建 时 间：2018-03-24 20:49:18
// 最 后 修 改：Administrator
// 更 新 时 间：2018-03-24 20:49:18
// ======================================================================================
// 功 能 描 述：地图设施（建筑等）对鼠标的相应
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;

public class MapControl_Answer : MonoBehaviour {

    public int answer_Type;//设施类型
    public int answer_ID;//设施ID
    public int answer_Value;//设施参数

    public GameObject Sprite_Sign;//选中特效图片
    
    public bool isSign;//是否显示选中特效

    void Start () {
        isSign = false;
    }
	

	void Update () {
        if (Sprite_Sign != null)
        {
            Sprite_Sign.SetActive(isSign);
        }
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

    }

    //当触发退出
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("玩家退出碰撞—建筑—触发");
    }


}
