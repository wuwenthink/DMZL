using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData_Config : MonoBehaviour {

    //【时间部分】
    public string nowDynasty;//朝代
    public string nowReigntitle;//年号
    public int nowYear;//年
    public int nowMonth;//月
    public int nowDay;//日
    public int nowMingHour;//时辰

    //【键位部分】
    public string Move_Up;   //向上移动按键
    public string Move_Down;   //向下移动按键
    public string Move_Left;   //向左移动按键
    public string Move_Right;   //向右移动按键

    public string UI_SysInfo;   //打开功能菜单
    public string UI_SystemTip;   //打开系统菜单

    public string UI_RoleInfo;   //打开人物信息菜单
    public string UI_Bag;   //打开背包菜单
    public string UI_Making;   //打开生产菜单

    public string Key_NearTip;   //触发交互按键

    //【设置部分】

    void Start () {
		
	}
	
	void Update () {
		
	}
}
