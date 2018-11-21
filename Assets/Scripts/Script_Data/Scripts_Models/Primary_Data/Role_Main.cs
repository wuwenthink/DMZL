// ======================================================================================
// 文件名         ：    Role.cs
// 版本号         ：    v3.1.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-8-15
// 最后修改日期   ：    2017-11-18
// ======================================================================================
// 功能描述       ：    角色(NPC)信息
// ======================================================================================

using System.Collections.Generic;

/// <summary>
/// 角色信息
/// </summary>
public class Role_Main
{
    /// <summary>
    /// 人物ID
    /// </summary>
    public int id { get; private set; }

    /// <summary>
    /// 名字
    /// </summary>
    public string commonName { get; private set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string gender { get; private set; }

    /// <summary>
    /// 出生时间
    /// </summary>
    public int birthday { get; private set; }

    /// <summary>
    /// 故乡
    /// </summary>
    public string place { get; private set; }

    /// <summary>
    /// 传记
    /// </summary>
    public string story { get; private set; }

    /// <summary>
    /// 角色头像
    /// </summary>
    public string headIcon { get; private set; }

    /// <summary>
    /// 形象图片ID
    /// </summary>
    public string imageID { get; private set; }

    /// <summary>
    /// 历史名人
    /// </summary>
    public int famous { get; private set; }

    /// <summary>
    /// 体力
    /// </summary>
    public int tili { get; private set; }

    /// <summary>
    /// 武力
    /// </summary>
    public int wuli { get; private set; }

    /// <summary>
    /// 智力
    /// </summary>
    public int zhili { get; private set; }

    /// <summary>
    /// 魄力
    /// </summary>
    public int poli { get; private set; }

    /// <summary>
    /// 毅力
    /// </summary>
    public int yili { get; private set; }

    /// <summary>
    /// 魅力
    /// </summary>
    public int meili { get; private set; }

    /// <summary>
    /// 声誉
    /// </summary>
    public int shengyu { get; private set; }

    /// <summary>
    /// 名望
    /// </summary>
    public int mingwang { get; private set; }


    public Role_Main(int _id, string _commonName, string _gender, int _birthday, string _place, string _story, string _headIcon, string _imageID, int _famous, int _tili, int _wuli, int _zhili, int _poli, int _yili, int _meili, int _shengyu, int _mingwang)
    {
        id = _id;
        commonName = _commonName;
        gender = _gender;
        birthday = _birthday;
        place = _place;
        story = _story;
        headIcon = _headIcon;
        imageID = _imageID;
        famous = _famous;
        tili = _tili;
        wuli = _wuli;
        zhili = _zhili;
        poli = _poli;
        yili = _yili;
        meili = _meili;
        shengyu = _shengyu;
        mingwang = _mingwang;
    }

}