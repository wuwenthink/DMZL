using UnityEngine;

namespace Common
{
    /// <summary>
    /// 快捷菜单
    /// </summary>
    public class UI_ShortcutMenu :SceneUI
    {
        Transform content;
        public override void Initialize ()
        {
            content = transform.FindChildByName("Content");
        }

        public void AddMinmize ( UI_Minimize minimize )
        {
            minimize.transform.parent = content;
        }
    }
}
