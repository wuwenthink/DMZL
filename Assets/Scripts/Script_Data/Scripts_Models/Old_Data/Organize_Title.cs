// ======================================================================================
// 文 件 名 称：Organize_Title.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-06 21:57:11
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-06 21:57:11
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organize_Title : MonoBehaviour {
    public Organize_Title(int _id, string _name, int _lv, string _power, int _org, int _iden)
    {
        id = _id;
        name = _name;
        lv = _lv;
        power = _power;
        org = _org;
        iden = _iden;
    }

    public int id { get; private set; }
    public string name { get; private set; }
    public int lv { get; private set; }
    public string power { get; private set; }
    public int org { get; private set; }
    public int iden { get; private set; }

}
