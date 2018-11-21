using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData_Player : MonoBehaviour {
    //位置信息
    public int worldPlaceID;//所处世界地图点
    public double PlayerPosX;//玩家X轴
    public double PlayerPosY;//玩家Y轴

    //玩家基础属性
    public int id;    //人物ID
    public string commonName;    //名字
    public string wordName;    //字
    public string titleName;    //名号
    public string gender;    //性别
    public int age;    //年龄
    public string character;    //性格索引ID
    public string story;    //传记
    public string headIcon;    //角色头像
    public int imageID;    //角色形象索引ID
    public int forceID;    //势力索引ID
    public int identityID;    //身份索引ID
    public int relationID;    //关系索引ID
    public int partnerID;    //随从索引ID
    public int hp;    //生命值
    public int action;    //行动值
    public int attack;    //武力值
    public int mp;    //耐力值
    public int lead;    //领导力
    public int firm;    //坚毅力
    public int strategy;    //谋略力
    public int wit;    //应变力
    public int hunger;    //饱腹度
    public int health;    //健康度
    public int mood;    //心情值
    public int energy;    //气力值
    public int load;    //负重上限
    public int charm;    //魅力
    public int fame;    //名声
    public int merit;    //功绩
    public int goodOrEvil;    //善恶值
    public int lucky;    //幸运值
    //玩家其他属性
    public int lifeYear;//寿命


    //玩家技能相关

    //玩家身份相关

    //玩家关系相关

    //背包相关
    public Dictionary<int, int> playerHave_Bag;//随身背包
    public int playerHave_Money;//随身背包

    //建筑类

    //商店相关

    //生产相关
    public Dictionary<int, int> make_AlreadyOpen;//已开启配方和对应熟练度

    //剧情进度

    //地图探索度

    //任务信息

    //……

    public GameObject Player_Move;

    void Awake()
    {
        if (playerHave_Money<0)
        {
            playerHave_Money = 0;
        }
        //测试使用数据

        playerHave_Money = 12535;
    }

    void Start () {
		
	}
	

	void Update () {
        PlayerPosX = Player_Move.transform.localPosition.x;
        PlayerPosY = Player_Move.transform.localPosition.y;
    }


  
}
