// ======================================================================================
// 文 件 名 称：Constants.cs
// 版 本 编 号：v2.1.0
// 原 创 作 者：xic
// 创 建 日 期：2017-8-10
// 更 新 日 期：2017-10-03 21:26:39
// ======================================================================================
// 功 能 描 述：各种常量和字典数据等游戏运行时常用但不会改变的数据
// ======================================================================================

using Mono.Data.Sqlite;
using System.Collections.Generic;

public class Constants
{
    /// <summary>
    /// 节日
    /// </summary>
    public static Dictionary<int, string> Festival { private set; get; }

    /// <summary>
    /// 节气
    /// </summary>
    public static Dictionary<int, string> SolarTerms { private set; get; }

    /// <summary>
    /// 年份信息
    /// </summary>
    public static Dictionary<int, List<string>> YearInfo { private set; get; }

    /// <summary>
    /// 所有的道具
    /// </summary>
    public static Dictionary<int, Item> Items_All { private set; get; }
    

    // 数据库
    private SqliteDbService dbService;
    private SqliteDbService dbService_save;

    private SqliteDataReader reader;

    // TOUPDATE 在游戏开始运行时调用一次
    /// <summary>
    /// 在数据库中查询所有字典表
    /// </summary>
    public void ReadAll ()
    {
        Festival = new Dictionary<int, string> ();
        SolarTerms = new Dictionary<int, string> ();
        YearInfo = new Dictionary<int, List<string>> ();

        dbService = BasicData.GetDataDbService;

        dbService_save = BasicData.GetSaveDbService;       

        Items_All = new Dictionary<int, Item> ();

        // 节日
        reader = dbService.ExecuteReader ("SELECT TCMonth, TCDay, name FROM Time_Day WHERE type=1;");
        while (reader.Read ())
            Festival.Add ((reader.GetInt32 (0) - 1) * 30 + reader.GetInt32 (1), reader.GetString (2));

        // 节气
        reader = dbService.ExecuteReader ("SELECT TCMonth, TCDay, name FROM Time_Day WHERE type=2;");
        while (reader.Read ())
            SolarTerms.Add ((reader.GetInt32 (0) - 1) * 30 + reader.GetInt32 (1), reader.GetString (2));

        // 年份
        reader = dbService.ExecuteReader ("SELECT ADYear, Reigntitle, TCYear, era FROM Time_Year;");
        while (reader.Read ())
        {
            YearInfo.Add (reader.GetInt32 (0), new List<string> ());
            YearInfo [reader.GetInt32 (0)].Add (reader.GetString (1));
            YearInfo [reader.GetInt32 (0)].Add (reader.GetInt32 (2) + "");
            YearInfo [reader.GetInt32 (0)].Add (reader.GetString (3));
        }

        // 所有道具
        reader = dbService.ExecuteReader ("SELECT id, type, name, des, icon, quality, price, funIndex, canUse FROM Item;");
        while (reader.Read ())
        {
            Item item = new Item (reader.GetInt32 (0), reader.GetInt32 (1), reader.GetString (2), reader.GetString (3), reader.GetString (4), reader.GetInt32 (5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32 (8));
            Items_All.Add (reader.GetInt32 (0), item);
        }
    }


    /// <summary>
    /// 获取年份的文字显示（年号、年份、天干地支）
    /// </summary>
    /// <param name="year">公元年</param>
    /// <returns>年号、年份、天干地支</returns>
    public static string[] GetYear(int year)
    {
        string[] str = Constants.YearInfo[year].ToArray();

        // 转换日期数字
        int _year = int.Parse(str[1]);
        str[1] = NumberToWords(_year);

        return str;

    }

    /// <summary>
    /// 获取日期的文字显示（月、日、季节）
    /// </summary>
    /// <param name="month">月</param>
    /// <param name="day">日</param>
    /// <returns>月、日、季节</returns>
    public static string[] GetDate(int month,int day)
    {
        string[] str = new string[3];

        str[0] = TransformMonth(month);

        str[1] = TransformDay(day);

        if (month >= 3 && month <= 5)
            str[2] = LanguageMgr.GetInstance.GetText("Nomal_62");
        else if (month >= 6 && month <= 8)
            str[2] = LanguageMgr.GetInstance.GetText("Nomal_63");
        else if (month >= 9 && month <= 11)
            str[2] = LanguageMgr.GetInstance.GetText("Nomal_64");
        else
            str[2] = LanguageMgr.GetInstance.GetText("Nomal_65");

        return str;
    }


    /// <summary>
    /// 100以内数字转换成文字显示
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string NumberToWords(int index)
    {
        string str = "";

        // 十位
        int tens = index / 10;
        if (tens == 0)
            str = "";
        else if (tens == 1)
            str = LanguageMgr.GetInstance.GetText("Nomal_35");
        else
            str = TransformNumber(tens) + LanguageMgr.GetInstance.GetText("Nomal_35");

        // 个位
        int unit = index % 10;
        str += TransformNumber(unit);

        return str;
    }

    /// <summary>
    /// 0-9的文字显示
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string TransformNumber(int index)
    {
        switch (index)
        {
            case 1:
                return LanguageMgr.GetInstance.GetText("Nomal_39");
            case 2:
                return LanguageMgr.GetInstance.GetText("Nomal_40");
            case 3:
                return LanguageMgr.GetInstance.GetText("Nomal_41");
            case 4:
                return LanguageMgr.GetInstance.GetText("Nomal_42");
            case 5:
                return LanguageMgr.GetInstance.GetText("Nomal_43");
            case 6:
                return LanguageMgr.GetInstance.GetText("Nomal_44");
            case 7:
                return LanguageMgr.GetInstance.GetText("Nomal_45");
            case 8:
                return LanguageMgr.GetInstance.GetText("Nomal_46");
            case 9:
                return LanguageMgr.GetInstance.GetText("Nomal_47");
            default:
                return null;
        }
    }

    /// <summary>
    /// 月份的文字显示
    /// </summary>
    /// <param name="_month"></param>
    /// <returns></returns>
    public static string TransformMonth(int _month)
    {
        string str = "";

        switch (_month)
        {
            case 1:
                str = LanguageMgr.GetInstance.GetText("Nomal_33") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 2:
                str = LanguageMgr.GetInstance.GetText("Nomal_40") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 3:
                str = LanguageMgr.GetInstance.GetText("Nomal_41") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 4:
                str = LanguageMgr.GetInstance.GetText("Nomal_42") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 5:
                str = LanguageMgr.GetInstance.GetText("Nomal_43") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 6:
                str = LanguageMgr.GetInstance.GetText("Nomal_44") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 7:
                str = LanguageMgr.GetInstance.GetText("Nomal_45") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 8:
                str = LanguageMgr.GetInstance.GetText("Nomal_46") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 9:
                str = LanguageMgr.GetInstance.GetText("Nomal_47") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 10:
                str = LanguageMgr.GetInstance.GetText("Nomal_35") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 11:
                str = LanguageMgr.GetInstance.GetText("Nomal_35") + LanguageMgr.GetInstance.GetText("Nomal_39") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
            case 12:
                str = LanguageMgr.GetInstance.GetText("Nomal_34") + LanguageMgr.GetInstance.GetText("Nomal_48");
                break;
        }

        return str;
    }

    /// <summary>
    /// 日期的文字显示
    /// </summary>
    /// <param name="_day"></param>
    /// <returns></returns>
    public static string TransformDay(int _day)
    {
        string str = "";

        // 十位
        int tens = _day / 10;
        if (tens == 0)
            str = LanguageMgr.GetInstance.GetText("Nomal_49");
        else if (tens == 1)
            str = LanguageMgr.GetInstance.GetText("Nomal_35");
        else
            str = TransformNumber(tens) + LanguageMgr.GetInstance.GetText("Nomal_35");

        // 个位
        int unit = _day % 10;
        str += TransformNumber(unit);

        // 初十单独处理
        if (_day == 10)
            str = LanguageMgr.GetInstance.GetText("Nomal_49") + LanguageMgr.GetInstance.GetText("Nomal_35");

        return str;
    }


    /// <summary>
    /// 属性值的文字显示
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string PropValueToWords (string value)
    {
        int level = int.Parse (value) / 50 + 1 > 9 ? 9 : int.Parse (value) / 50 + 1;
        return LanguageMgr.GetInstance.GetText ("Nomal_" + level);
    }
}

/// <summary>
/// 身份类型
/// </summary>
public enum IdentityType
{
    /// <summary>
    /// 职业身份
    /// </summary>
    职业 = 1,

    /// <summary>
    /// 官职身份
    /// </summary>
    官职 = 2,

    /// <summary>
    /// 职事身份
    /// </summary>
    职事 = 3,

    /// <summary>
    /// 称号身份
    /// </summary>
    称号 = 4,

    /// <summary>
    /// 荣衔身份
    /// </summary>
    荣衔 = 5,

    /// <summary>
    /// 爵位身份
    /// </summary>
    爵位 = 6
};

/// <summary>
/// 身份的状态
/// </summary>
public enum IdentityState
{
    /// <summary>
    /// 隐藏
    /// </summary>
    hidden = 2,

    /// <summary>
    /// 未解锁
    /// </summary>
    locked = 1,

    /// <summary>
    /// 显示
    /// </summary>
    show = 0
};

/// <summary>
/// 消息的进行状态
/// </summary>
public enum NewsState
{
    /// <summary>
    /// 进行中
    /// </summary>
    inProgress = 0,

    /// <summary>
    /// 已过期（未读）
    /// </summary>
    overdue = 1,

    /// <summary>
    /// 已忽略
    /// </summary>
    overlook = 2
};

/// <summary>
/// 任务类型
/// </summary>
public enum TaskType
{
    系统任务,
    时代背景任务,
    主线剧情任务,
    支线剧情任务,
    身份获得任务,
    身份职能任务,
    身份考核任务,
    消息任务,
    事件任务,
    个人任务
};

/// <summary>
/// 学识类别
/// </summary>
public enum KnowledgeClass
{
    学问大类 = 1,
    每类历练 = 2,
    每类书籍 = 3
};

/// <summary>
/// 学识类型
/// </summary>
public enum KnowledgeType
{
    无 = 0,
    经学 = 1,
    文学 = 2,
    韬略 = 3,
    宗教 = 4,
    杂学 = 5
};

/// <summary>
/// 技法类型
/// </summary>
public enum SkillType
{
    标准技法 = 1,
    主动使用技能 = 2,
    被动属性技能 = 3,
    生产道具技能 = 4,
    解锁配方 = 5
};

/// <summary>
/// 时间段
/// </summary>
public enum TimeFrame
{
    Morning = 0,
    DayTime = 1,
    Evening = 2,
    Night = 3
};



