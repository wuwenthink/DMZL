// ======================================================================================
// 文 件 名 称：LanguageMgr.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-26 21:14:55
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-26 21:14:55
// ======================================================================================
// 功 能 描 述：处理多语言的本地化
// ======================================================================================

using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine;

public class LanguageMgr : MonoBehaviour
{
    private static LanguageMgr _instance;

    public static LanguageMgr GetInstance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    public SystemLanguage language;

    private Dictionary<string, string> languageDic;

    private SqliteDataReader reader;

    private void Awake ()
    {
        LoadLanguage ();

        _instance = this;

    }

    private void LoadLanguage ()
    {

        languageDic = new Dictionary<string, string> ();
        foreach (KeyValuePair<string, Class_WordTranslate> item in SD_WordTranslate.Class_Dic)
        {
            languageDic.Add (item.Key, item.Value.chinese);
        }

        //reader = BasicData.GetDataDbService.ExecuteReader("SELECT id, " + language.ToString() + " FROM WordTranslate;");
        //while (reader.Read())
        //{
        //    languageDic.Add(reader.GetString(0), reader.GetString(1));
        //}
    }

    /// <summary>
    /// 通过key获取不同语言对应的文字
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public string GetText (string _key)
    {
        if (languageDic != null && languageDic.ContainsKey (_key))
        {
            return languageDic [_key];
        }
        else
        {
            Debug.LogError ("languageDic == null OR key not exist！");
            return string.Empty;
        }
    }
}