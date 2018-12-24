using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Text;
using LitJson;
using SimpleJSON;
using System.Text.RegularExpressions;

public static class JsonReader
{

    /// <summary>
    /// 读取json数据，根据id查找（表格里的第一行），返回id和类对象互相对应的字典。
    /// ToJson.tostring()会在字符串前后各加一个"，注意读取的时候删掉:string newStr = oldStr.Substring(1, oldStr.Length()-1);或者string str= str.replace("\"", "");
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static Dictionary<string, T> ReadJson<T>(string fileName)
    {
        TextAsset ta = Resources.Load(fileName) as TextAsset;
        if (ta.text == null) { Debug.Log("根据路径未找到对应表格数据"); };
        Dictionary<string, T> d = JsonMapper.ToObject<Dictionary<string, T>>(ta.text);
        return d;
    }

    /// <summary>
    /// 保存数据到json文件中
    /// ToJson.tostring()会在字符串前后各加一个"，注意读取的时候删掉:string newStr = oldStr.Substring(1, oldStr.Length()-1);或者string str= str.replace("\"", "");
    /// </summary>
    /// <param name="path"></param>
    /// <param name="json"></param>
    public static void WriteJson(string path, object json)
    {
        string ta;
        //Regex.Unescape这个函数的作用是把 正则表达式 表示的字符串转换成 非正则表达式 的字符串
        ta = Regex.Unescape(JsonMapper.ToJson(json));

        //ta = JsonMapper.ToJson(json);
        //Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        //ta = reg.Replace(ta, delegate (Match m) { return ((char)System.Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });

        string directName = Path.GetDirectoryName(path);
        if (!Directory.Exists(directName))
        {
            Directory.CreateDirectory(directName);
        }
        File.WriteAllText(path, ta, Encoding.UTF8);
        Debug.Log("成功输出Json文件  :" + path);
        //在Editor模式下重新导入文件数据，刷新。
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
    /// <summary>
    /// 转换中文语言为UTF8
    /// </summary>
    /// <param name="des"></param>
    /// <returns></returns>
    public static string ChangeStringToUTF8(string des)
    {
        UTF8Encoding utf8 = new UTF8Encoding();
        return utf8.GetString(utf8.GetBytes(des));
    }

    /// <summary>
    /// 把 正则表达式 表示的字符串转换成 非正则表达式 的字符串
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static string ChangeStringToRegex(object json)
    {
        //Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        //string ta = reg.Replace(json.ToString(), delegate (Match m) { return ((char)System.Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        //return ta;
        return Regex.Unescape(JsonMapper.ToJson(json));
    }
}




