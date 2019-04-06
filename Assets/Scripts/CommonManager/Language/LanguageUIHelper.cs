using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public static class LanguageUIHelper
    {
        /// <summary>
        /// 更换语言
        /// </summary>
        /// <param name="num">语言集合编号</param>
        public static void ChangeLanguage_Int(int num)
        {
            string language;
            if (num < LanguageManager.I.LanguageList.Count)
            {
                language = LanguageManager.I.LanguageList[num];
            }
            else language = LanguageManager.I.LanguageList[0];
            LanguageManager.I.CurrentLanguage = language;
            PlayerPrefs.SetInt(LanguageKey.PlayerPrefsKey, num);
        }
        /// <summary>
        /// 上次选择
        /// </summary>
        /// <returns></returns>
        public static int LastValue_Int()
        {
            if (PlayerPrefs.HasKey(LanguageKey.PlayerPrefsKey))
            {
                int languageNum = PlayerPrefs.GetInt(LanguageKey.PlayerPrefsKey);
                if (languageNum < LanguageManager.I.LanguageList.Count)
                {
                    LanguageManager.I.CurrentLanguage = LanguageManager.I.LanguageList[languageNum];
                    return languageNum;
                }
            }
            return 0;
        }
        /// <summary>
        /// 返回Dropdown.OptionData集合
        /// </summary>
        /// <returns></returns>
        public static List<Dropdown.OptionData> LanguageDropdownOptions()
        {
            List<Dropdown.OptionData> optionsList = new List<Dropdown.OptionData>();
            foreach (string language in LanguageManager.I.LanguageList)
            {
                optionsList.Add(new Dropdown.OptionData(language));
            }
            return optionsList;
        }
    }
}
