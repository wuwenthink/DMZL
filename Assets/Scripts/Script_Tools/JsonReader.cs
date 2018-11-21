using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Text;
using LitJson;
using SimpleJSON;

public static class JsonReader
{

    //读取json数据，根据id查找（表格里的第一行），返回id和类对象互相对应的字典。
    public static Dictionary<string, T> ReadJson<T>(string fileName)
    {
        TextAsset ta = Resources.Load(fileName) as TextAsset;
        if (ta.text == null) { Debug.Log("根据路径未找到对应表格数据"); };
        Dictionary<string, T> d = JsonMapper.ToObject<Dictionary<string, T>>(ta.text);
        return d;
    }

    /// <summary>
    /// 保存数据到json文件中
    /// </summary>
    /// <param name="path"></param>
    /// <param name="json"></param>
    public static void WriteJson(string path, object json)
    {
        string ta;
        ta = JsonMapper.ToJson(json);
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
}




