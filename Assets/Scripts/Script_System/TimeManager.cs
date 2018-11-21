// ====================================================================================== 
// 文件名         ：    TimeManager.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-9-2                                                                  
// 最后修改日期   ：    2017-9-2                                                            
// ====================================================================================== 
// 功能描述       ：    时间管理                                                                   
// ====================================================================================== 

using UnityEngine;

/// <summary>
/// 时间管理
/// </summary>
public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// 时间是否暂停
    /// </summary>
    private static bool timePause;

    /// <summary>
    /// 记录游戏时间的long值，每+1为游戏时间的1分钟
    /// </summary>
    private static int time_long;

    /// <summary>
    /// 公元年
    /// </summary>
    private static int year;
    /// <summary>
    /// 月
    /// </summary>
    private static int month;
    /// <summary>
    /// 日
    /// </summary>
    private static int day;
    /// <summary>
    /// 时辰
    /// </summary>
    private static int twoHour;
    /// <summary>
    /// 刻钟
    /// </summary>
    private static int quarter;
    /// <summary>
    /// 作为key，用于标识节日、节气、特殊祭祀日等
    /// </summary>
    public static int identifyDate;

    float mtime = 1;//倒计时
    public int timeWide;//每刻钟的秒数

    /// <summary>
    /// 健康状态
    /// </summary>
    public int health;
    /// <summary>
    /// 精神状态
    /// </summary>
    public int mind;
    /// <summary>
    /// 饥饿状态
    /// </summary>
    public int hungry;
    /// <summary>
    /// 温度
    /// </summary>
    public int temp;
    /// <summary>
    /// 当前血量
    /// </summary>
    public int hp;
    /// <summary>
    /// 当前心情
    /// </summary>
    public int mood;


    Charactor charactor;
    private void Awake()
    {
        charactor = Charactor.GetInstance;

        timePause = true;
        timeWide = 15;

        hp = 100;
    }

    void Start ()
    {

    }

    void FixedUpdate ()
    {
        if (!timePause)
        {
            mtime += Time.deltaTime;
            if (mtime >= 0)
            {
                mtime -= 1;
                time_long++;

                // 时辰+1
                if (time_long > 0 && time_long % timeWide == 0)
                {
                    quarter++;
                    DateRevision();
                    RoleState();
                }
            }
        }
    }

    /// <summary>
    /// 角色状态随时间变化
    /// </summary>
    public void RoleState()
    {
        //同步角色属性
        charactor.health = health;
        charactor.hunger = hungry;
        charactor.temp = temp;
        charactor.mood = mood;
        charactor.hp = hp;

        //饥饿度每刻钟减1。
        if (quarter == 1)
        {
            hungry -= 2;
        }
        //三个值的结果体现到血量的值上
        if (quarter == 1)
        {

            if ((health >= 20 && health <= 30) || (hungry >= 10 && hungry <= 20) || (temp >= 80 && temp <= 90) || (temp >= 10 && temp <= 20))
            {
                hp -= 5;
            }
            else if ((health >= 10 && health <= 20) || (hungry >= 0 && hungry <= 10) || (temp >= 90 && temp < 100) || (temp > 0 && temp <= 10))
            {
                hp -= 10;
            }
            else if (health <= 10 && health > 0)
            {
                hp -= 15;
            }
            else if ((health == 0) || temp >= 100 || temp <= 0)
            {
                hp = 0;
            }
        }

        //校验所有值不低于0
        if (health < 0) { health = 0; }
        if (hungry < 0) { hungry = 0; }
        if (temp < 0) { temp = 0; }
    }

    /// <summary>
    /// 日期修正
    /// </summary>
    private void DateRevision ()
    {
        // 刻钟+1
        while (quarter > 7)
        {
            quarter -= 8;
            twoHour++;
            // 时辰+1
            if (twoHour > 12)
            {
                twoHour -= 12;
                day++;
                // 月+1
                if (day > 30)
                {
                    day = 1;
                    month++;
                    // 年+1
                    if (month > 12)
                    {
                        month = 1;
                        year++;
                    }
                }
            }
        }

        identifyDate = (month - 1) * 30 + day;
    }

    /// <summary>
    /// 获取游戏内当天进行的总时长（以时辰为单位）
    /// </summary>
    /// <returns></returns>
    public static int GetTime ()
    {
        return (int) (time_long / 15);
    }

    /// <summary>
    /// 将时间转换为文字显示
    /// </summary>
    /// <param name="_time">刻钟数</param>
    /// <returns></returns>
    public static string GetTime (TimeUnit _unit, int _time)
    {
        var lm = LanguageMgr.GetInstance;
        string result = "";
        switch (_unit)
        {
            case TimeUnit.Quarter:
                int month = _time / 2880;
                result = month > 0 ? month + lm.GetText ("Nomal_16") : "";
                int day = (_time % 2880) / 96;
                result += day > 0 ? day + lm.GetText ("Nomal_17") : "";
                int hour = (_time % 96) / 8;
                result += hour > 0 ? hour + lm.GetText ("Nomal_18") : "";
                int quarter = _time % 8;
                result += quarter > 0 ? quarter + lm.GetText ("Nomal_19") : "";
                break;
            case TimeUnit.Twohour:
                month = _time / 360;
                result = month > 0 ? month + lm.GetText ("Nomal_16") : "";
                day = (_time % 360) / 12;
                result += day > 0 ? day + lm.GetText ("Nomal_17") : "";
                hour = _time % 12;
                result += hour > 0 ? hour + lm.GetText ("Nomal_18") : "";
                break;
            case TimeUnit.Day:
                month = _time / 30;
                result = month > 0 ? month + lm.GetText ("Nomal_16") : "";
                day = _time % 30;
                result += day > 0 ? day + lm.GetText ("Nomal_17") : "";
                break;
        }

        return result;
    }

    /// <summary>
    /// 时间的基本单位
    /// </summary>
    public enum TimeUnit
    {
        Quarter,
        Twohour,
        Day
    };

    /// <summary>
    /// 初始化游戏时间
    /// </summary>
    public void InitTime (int _year, int _month, int _day)
    {
        year = _year;
        month = _month;
        day = _day;
        twoHour = 4;
        quarter = 0;
        time_long = 0;
        identifyDate = (month - 1) * 30 + day;
    }

    /// <summary>
    /// 时间暂停
    /// </summary>
    public static void PauseTime ()
    {
        timePause = true;
    }

    /// <summary>
    /// 时间继续
    /// </summary>
    public static void ContinueTime ()
    {
        timePause = false;
    }

    /// <summary>
    /// 时间暂停或继续（测试用）
    /// </summary>
    public static void SwitchTime ()
    {
        timePause = !timePause;
    }

    /// <summary>
    /// 获取时间状态
    /// </summary>
    /// <returns></returns>
    public bool GetTimeState ()
    {
        return timePause;
    }

    /// <summary>
    /// 获取游戏里当前的日期
    /// </summary>
    /// <returns></returns>
    public static int GetDays()
    {
        return day;
    }

    /// <summary>
    /// 获得当前的月的数字
    /// </summary>
    /// <returns></returns>
    public int GetMonths()
    {
        return month;
    }

    /// <summary>
    /// 获得当前的年的数字
    /// </summary>
    /// <returns></returns>
    public int GetYears()
    {
        return year;
    }

    /// <summary>
    /// 获取当前时辰
    /// </summary>
    /// <returns></returns>
    public int GetTwoHour ()
    {
        return twoHour;
    }

    /// <summary>
    /// 获取当前刻钟
    /// </summary>
    /// <returns></returns>
    public int GetQuarter()
    {
        return quarter;
    }

    /// <summary>
    /// 获取当前的时间段
    /// </summary>
    /// <returns></returns>
    public TimeFrame GetCurrTimeFrame ()
    {
        switch (twoHour)
        {
            case 3:
                return TimeFrame.Morning;
            case 4:
                return TimeFrame.Morning;
            case 5:
                return TimeFrame.DayTime;
            case 6:
                return TimeFrame.DayTime;
            case 7:
                return TimeFrame.DayTime;
            case 8:
                return TimeFrame.DayTime;
            case 9:
                return TimeFrame.Evening;
            case 10:
                return TimeFrame.Evening;
        }
        return TimeFrame.Night;
    }

    /// <summary>
    /// 快进时间（一天内：几个时辰）
    /// <param name="_twoHour">时辰数</param>
    /// </summary>
    public void SpeedTime (int _twoHour)
    {
        twoHour += _twoHour;
        DateRevision ();
    }

    /// <summary>
    /// 快进时间（长跨度：直接赋值改变）
    /// </summary>
    /// <param name="_year"></param>
    /// <param name="_month"></param>
    /// <param name="_day"></param>
    public void SpeedTime(int _year,int _month,int _day,int _twoHour)
    {
        year = _year;
        month = _month;
        day = _day;
        twoHour = _twoHour;
        quarter = 0;
        DateRevision();
    }


    /// <summary>
    /// 获取时辰的文字显示
    /// </summary>
    /// <returns></returns>
    public static string GetTwoHour(int _time)
    {
        string str = "";

        switch (_time == -1 ? twoHour : _time)
        {
            case 0:
                str = LanguageMgr.GetInstance.GetText("Nomal_50");
                break;
            case 1:
                str = LanguageMgr.GetInstance.GetText("Nomal_51");
                break;
            case 2:
                str = LanguageMgr.GetInstance.GetText("Nomal_52");
                break;
            case 3:
                str = LanguageMgr.GetInstance.GetText("Nomal_53");
                break;
            case 4:
                str = LanguageMgr.GetInstance.GetText("Nomal_54");
                break;
            case 5:
                str = LanguageMgr.GetInstance.GetText("Nomal_55");
                break;
            case 6:
                str = LanguageMgr.GetInstance.GetText("Nomal_56");
                break;
            case 7:
                str = LanguageMgr.GetInstance.GetText("Nomal_57");
                break;
            case 8:
                str = LanguageMgr.GetInstance.GetText("Nomal_58");
                break;
            case 9:
                str = LanguageMgr.GetInstance.GetText("Nomal_59");
                break;
            case 10:
                str = LanguageMgr.GetInstance.GetText("Nomal_60");
                break;
            case 11:
                str = LanguageMgr.GetInstance.GetText("Nomal_61");
                break;
        }

        return str;
    }

    /// <summary>
    /// 获取节日的文字显示
    /// </summary>
    /// <returns>不是节日时返回空值</returns>
    public string GetFestival()
    {
        if (!Constants.Festival.ContainsKey(TimeManager.identifyDate))
            return "";
        return Constants.Festival[TimeManager.identifyDate];
    }

    /// <summary>
    /// 获取节气的文字显示
    /// </summary>
    /// <returns>不是节气时返回空值</returns>
    public string GetSolarTerms()
    {
        if (!Constants.SolarTerms.ContainsKey(TimeManager.identifyDate))
            return "";
        return Constants.SolarTerms[TimeManager.identifyDate];
    }





    //void GameTime_Year()
    //{
    //    mTime -= Time.deltaTime;
    //    //秒-刻-时-天-月-年时间转换
    //    if (mTime <= 0)
    //    {
    //        nowMingHour++;
    //        NowTimePGValue += 0.0834f;
    //        mTime = sTime;
    //    }

    //    if (nowMingHour == 13f)
    //    {
    //        nowDay++;
    //    }
    //    if (nowDay == 31f)
    //    {
    //        nowMonth++;
    //    }
    //    if (nowMonth == 13f)
    //    {
    //        nowYear++;
    //    }
    //    if (nowMonth == 13f) { nowMonth = 1; }
    //    if (nowMingHour == 13f) { nowMingHour = 1; }
    //    if (nowDay == 31f) { nowDay = 1; }

    //    switch (nowMingHour)
    //    {
    //        case 1: MingHourS = "子"; break;
    //        case 2: MingHourS = "丑"; break;
    //        case 3: MingHourS = "寅"; break;
    //        case 4: MingHourS = "卯"; break;
    //        case 5: MingHourS = "辰"; break;
    //        case 6: MingHourS = "巳"; break;
    //        case 7: MingHourS = "午"; break;
    //        case 8: MingHourS = "未"; break;
    //        case 9: MingHourS = "申"; break;
    //        case 10: MingHourS = "酉"; break;
    //        case 11: MingHourS = "戌"; break;
    //        case 12: MingHourS = "亥"; break;
    //    }
    //    if (NowTimePGValue >= 1f)
    //    {
    //        NowTimePGValue = 0;
    //    }
    //}

}
