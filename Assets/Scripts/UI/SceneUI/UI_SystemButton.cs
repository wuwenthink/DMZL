using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Common;

namespace DMZL_UI
{
    public class UI_SystemButton :SceneUI
    {
        Button button;
        UI_ShortcutMenu shortcutMenu;
        Text text;
        public override void Initialize ()
        {
            button = GetComponent<Button>();
            text = GetComponentInChildren<Text>();
        }
        protected override void Start ()
        {
            //获取 UI_SystemWindow
            shortcutMenu = UIManager.I.GetSceneUI<UI_ShortcutMenu>();
            //文字将注册进入LanguageManager
            LanguageManager.I.TextRegister("WordTable_Test","ShortcutMenu",text);
            //给按钮附开关功能
            button.onClick.AddListener(shortcutMenuIO);     
        }
        /// <summary>
        /// 快捷菜单开关
        /// </summary>
        void shortcutMenuIO ()
        {
            //如果系统窗口关闭则打开，窗口打开则关闭
            if ( !shortcutMenu.IO )
            {
                shortcutMenu.Show();
            }
            else
            {
                shortcutMenu.Hide();
            }
        }
    }
}