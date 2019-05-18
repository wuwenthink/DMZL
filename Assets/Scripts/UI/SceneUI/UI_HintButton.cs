using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Common;

namespace DMZL_UI
{
    public class UI_HintButton :SceneUI
    {
        Button button;
        UI_HintPanel HintPanel;
        CanvasGroup numberImage;
        Text numberText;

        /// <summary>
        /// 新提示数量
        /// </summary>
        int newHintNmuber;
        public int NewHintNmuber
        {
            get => newHintNmuber;
            set
            {
                newHintNmuber = value;
                if ( newHintNmuber > 0 )
                {
                    numberImage.alpha = 1;
                    if ( newHintNmuber > 99 )
                    {
                        numberText.text = "99+";
                    }
                    else numberText.text = newHintNmuber.ToString();
                }
                else numberImage.alpha = 0;

            }
        }

        public override void Initialize ()
        {
            button = GetComponent<Button>();

            numberImage = transform.FindChildByName("NumberImage").GetComponent<CanvasGroup>();
            numberText = transform.FindChildByName("NumberText").GetComponent<Text>();
            //隐藏数量提示
            numberImage.alpha = 0;
        }
        protected override void Start ()
        {
            //获取 UI_SystemWindow
            HintPanel = UIManager.I.GetSceneUI<UI_HintPanel>();

            //给按钮附开关功能
            button.onClick.AddListener(shortcutMenuIO);
        }

        /// <summary>
        /// 提示面板开关
        /// </summary>
        void shortcutMenuIO ()
        {
            //如果系统窗口关闭则打开，窗口打开则关闭
            if ( !HintPanel.IO )
            {
                HintPanel.Show();
            }
            else
            {
                HintPanel.Hide();
            }
        }

    }
}