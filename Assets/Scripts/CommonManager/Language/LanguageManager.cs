using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Common
{
    /// <summary>
    /// 语言管理器
    /// </summary>
    public class LanguageManager :Singleton<LanguageManager>
    {
        /// <summary> 语言集合 </summary>
        public List<string> LanguageList;
        /// <summary> 当前选择语言 </summary>
        string currentLanguage;
        /// <summary> Text字典（语言表、（Key、Text）） </summary>
        Dictionary<string,Dictionary<string,List<Text>>> TextDic;

        /// <summary>
        /// 当前语言
        /// </summary>
        public string CurrentLanguage
        {
            get
            {
                if ( currentLanguage == null )
                {
                    return LanguageList[0];
                }
                return currentLanguage;
            }
            set
            {
                if ( LanguageList.Contains(value) )
                {
                    currentLanguage = value;
                }
                else
                {
                    currentLanguage = LanguageList[0];
                }
                TextUpdate();
            }
        }


        /// <summary>
        /// 初始化字典和从文件中读取语言数量到集合中
        /// </summary>
        protected override void Initialize ()
        {
            //声明语言集合
            LanguageList = new List<string>();
            //从数据库读取语言类型
            object[][] vs = DataManager.I.ReadData(" Select " + LanguageKey.LanguageType + " From " + LanguageKey.LanguageTypeTable);
            for ( int i = 0 ; i < vs.Length ; i++ )
            {
                LanguageList.Add(vs[i][0]as string);
            }
            //实例Text字典
            TextDic = new Dictionary<string,Dictionary<string,List<Text>>>();
        }

        /// <summary>
        /// 更新所有语言
        /// </summary>
        void TextUpdate ()
        {
            foreach ( string Space in TextDic.Keys )
            {
                foreach ( string Key in TextDic[Space].Keys )
                {
                    foreach ( Text text in TextDic[Space][Key] )
                    {
                        text.text = LoadWard(Space,Key);
                    }
                }
            }
        }

        /// <summary>
        /// 在语言管理器中注册Text
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="tag">Key</param>
        /// <param name="text">需要注册的Text</param>
        public void TextRegister ( string Table,string tag,Text text )
        {
            if ( !TextDic.ContainsKey(Table) )
            {
                TextDic.Add(Table,new Dictionary<string,List<Text>>());
            }
            if ( !TextDic[Table].ContainsKey(tag) )
            {
                TextDic[Table].Add(tag,new List<Text>());
            }
            TextDic[Table][tag].Add(text);
            text.text = LoadWard(Table,tag);
        }

        /// <summary>
        /// 清空Text字典
        /// </summary>
        /// <param name="text"></param>
        public void ClearTextDic ()
        {
            TextDic.Clear();
        }
        /// <summary>
        /// 移除注册的Text
        /// </summary>
        public void RemoveText ( Text text )
        {
            foreach ( string Space in TextDic.Keys )
            {
                foreach ( string Key in TextDic[Space].Keys )
                {
                    if ( TextDic[Space][Key].Contains(text) ) TextDic[Space][Key].Remove(text);
                    break;
                }
            }
        }
        /// <summary>
        /// 返回数据库对应tag的文字内容
        /// </summary>
        /// <param name="Table">表</param>
        /// <param name="tag">tag</param>
        public string LoadWard ( string Table,string tag )
        {

            object[][] data = DataManager.I.ReadData(" Select " + CurrentLanguage + " FROM " + Table + " WHERE tag = " + "'" + tag + "'");
            return data[0][0].ToString();
        }
    }
}
