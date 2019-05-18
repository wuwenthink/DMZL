using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common
{
    public class UI_Minimize :UIBehaviour, IPooler
    {
        WindowUI window;
        public WindowUI Window { get => window; set => window = value; }

        Button button;
        public Text ObjectName;
        public Text WindowName;


        protected override void Awake ()
        {
            ObjectName = transform.FindChildByName("ObjectName").GetComponent<Text>();
            WindowName = transform.FindChildByName("WindowName").GetComponent<Text>();

            button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                if ( Window.IO )
                {
                    Window.Minimize();
                }
                else Window.Show();
            });
        }

        public void OnReset ()
        {
            UIManager.I.GetSceneUI<UI_ShortcutMenu>().AddMinmize(this);
        }

        public void Recover ()
        {
            Window = null;
        }
    }
}
