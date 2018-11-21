// ======================================================================================
// 文件名         ：    Load_Drama.cs
// 版本号         ：    v1.2.1
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-17
// 最后修改日期   ：    2017-10-02 11:25:23
// ======================================================================================
// 功能描述       ：    加载剧本
// ======================================================================================

using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加载剧本
/// </summary>
public class Load_Drama : MonoBehaviour {
    int DramaID;

    void Start ()
    {

    }

    void Update ()
    {

    }

    /// <summary>
    /// 加载剧本
    /// </summary>
    /// <param name="dramaID">剧本ID</param>
    /// <param name="roleID">角色在剧本中的ID</param>
    public void Load_OneDrama (int dramaID , int roleID)
    {

        //暂时直接跳转到城市地图
        StartCoroutine(CommonFunc.GetInstance.ToSceneLoading(Data_Static.SceneName_TheWorld));

        DramaID = dramaID;

        Load_MainRole (roleID);//【加载角色关联信息】
        Load_Area();

        Load_Realtime_Data ();//【加载运行时初始化数据】

        // 读取剧本初始时间
        int year = SelectDao.GetDao().SelectDrama_Main(dramaID).TCYear;
        int month = SelectDao.GetDao().SelectDrama_Main(dramaID).TCMonth;
        int day = SelectDao.GetDao().SelectDrama_Main(dramaID).TCDay;
        FindObjectOfType<TimeManager>().InitTime(year, month, day);
    }

    /// <summary>
    /// 加载Runtime数据
    /// </summary>
    private void Load_Realtime_Data ()
    {
        RunTime_Data.currSceneId = 1001;


        Bag.GetInstance.GetItem(70001, 5);
        Bag.GetInstance.GetItem(70002, 5);
        Bag.GetInstance.GetItem(70003, 5);
        Bag.GetInstance.GetItem(70004, 5);
        Bag.GetInstance.GetItem(71005, 5);
        Bag.GetInstance.GetItem(71006, 5);
        Bag.GetInstance.GetItem(71007, 5);
        Bag.GetInstance.GetItem(71008, 5);
        Bag.GetInstance.GetItem(71009, 5);
        Bag.GetInstance.GetItem(71010, 5);

    }

    /// <summary>
    /// 加载角色关联信息
    /// </summary>
    /// <param name="id"></param>
    private void Load_MainRole(int id)
    {
        int roleID = SelectDao.GetDao().SelectDrama_Role(id).modelID;
        Role_Main role_Prop = SelectDao.GetDao().SelectRole(roleID);
        int famous = 0;
        if (role_Prop.famous==1)
        {
            famous = 1;
        }
        else
        {
            famous = 0;
        }
        Charactor roleMain = new Charactor(-1, role_Prop.commonName,role_Prop.gender,role_Prop.birthday,role_Prop.place,role_Prop.story,role_Prop.headIcon,role_Prop.imageID, role_Prop.famous,role_Prop.tili,role_Prop.wuli,role_Prop.zhili,role_Prop.poli,role_Prop.yili,role_Prop.meili,role_Prop.shengyu,role_Prop.mingwang);

        //roleMain.Force = SelectDao.GetDao().SelectDrama_Role(id).force;

        Charactor.GetInstance.health = 80;
        Charactor.GetInstance.hunger = 60;
        Charactor.GetInstance.mood = 60;
        Charactor.GetInstance.temp = 50;
        Charactor.GetInstance.hp = 100;
        Charactor.GetInstance.action = 60;

        List<Item> items = SelectDao.GetDao().SelectItemByType(1);
        //foreach (var a in items)
        //{
        //    Charactor.GetInstance.PackmanTool.Add(a.id, 25);
        //    Charactor.GetInstance.TransTool.Add(a.id, 50);
        //}
    }

    /// <summary>
    /// 加载地区
    /// </summary>
    void Load_Area()
    {

    }

}