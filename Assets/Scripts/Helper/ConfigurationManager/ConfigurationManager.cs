using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 配置文件读取器
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        static string ReadConfig ( string fileName )
        {
            //1. 因为 Application.streamingAssetsPath 在安卓平台下有时候无法读取资源
            //   所有在开发过程中,使用自定义路径(unity 宏标签).
            string path;
#if UNITY_EDITOR || UNITY_STANDALONE
            path = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;
#elif UNITY_ANDROID
            path ="jar:file://"+ Application.dataPath + "!/assets/" + fileName;
#elif UNITY_IPHONE
            path ="file://"+ Application.dataPath + "/Raw/" + fileName;
#endif
            //2. 因为 System.IO.FileStream 只能操作明确的文件,而手机当中的文件没有明确路径.
            //所以使用WWW类读取.
            //例如:window 
            //  D:\u3d1809\Month03\工程
            //手机:
            //  ...\工程  
            //所以使用WWW类读取.
            using ( WWW www = new WWW(path) )
            {
                //yield return www;//等待www读取完成
                while ( true )
                {
                    if ( www.isDone )
                        return www.text;
                }
            }
        }

        /// <summary>
        /// 配置文件读取
        /// </summary>
        /// <param name="config">配置文件名</param>
        /// <param name="handler">执行方法</param>
        public static void LoadConfig ( string config,Action<string> handler )
        {

            using ( StringReader reader = new StringReader(ReadConfig(config)) )
            {
                if ( reader.Peek()==0)
                {
                    Debug.LogError("缺少配置文件：" + config);
                    return;
                }
                string line;
                while ( ( line = reader.ReadLine() ) != null )
                {
                    //line 
                    handler(line);
                }
            }
        }
        /// <summary>
        /// 将map写入文件
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="map"></param>
        public static void WriteMapToFile (string configName, Dictionary<string,string> map )
        {
            //定义写入配置文件的字符串集合
            List<string> writer = new List<string>();
            //遍历配置文件集合
            foreach ( string Key in map.Keys )
            {
                //拼接字符串
                writer.Add(Key + "=" + map[Key]);
            }
            //写入文件
            File.WriteAllLines("Assets/StreamingAssets/"+configName,writer.ToArray());
        }
    }
}