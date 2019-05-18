using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_Tips :UIBehaviour, IPooler
    {
        /// <summary>
        /// Tips文字
        /// </summary>
        public Text TipsText;
        RectTransform rect;

        CanvasGroup group;
        float alpha;
        bool io;

        /// <summary>
        /// UI是否隐藏
        /// </summary>
        public bool IO { get => io; }
        protected override void Awake ()
        {
            rect = GetComponent<RectTransform>();
            //获取Group
            group = GetComponent<CanvasGroup>();
            //获取透明度
            alpha = group.alpha;
            group.blocksRaycasts = false;
            //获取文字
            TipsText = transform.GetComponentInChildren<Text>();
            //初始状态为隐藏
            HideTips();
        }

        /// <summary>
        /// 显示Tips
        /// </summary>
        public virtual void ShowTips ()
        {
            group.alpha = alpha;
            io = true;
            transform.position = Input.mousePosition + new Vector3(rect.sizeDelta.x / 2,rect.sizeDelta.y / 2,0);
        }

        /// <summary>
        /// 显示Tips
        /// </summary>
        public virtual void ShowTips ( string text )
        {
            TipsText.text = text;
            ShowTips();
        }

        /// <summary>
        /// 显示Tips
        /// </summary>
        public virtual void ShowTips ( string TextTable,string TextTag )
        {
            TipsText.text = LanguageManager.I.LoadWard(TextTable,TextTag);
            ShowTips();
        }
        /// <summary>
        /// 隐藏窗口
        /// </summary>
        public virtual void HideTips ()
        {
            group.alpha = 0;
            io = false;
        }

        public void OnReset ()
        {
            transform.parent = UIManager.I.SystemCanvas.transform;
        }

        public void Recover ()
        {
            TipsText.text = null;
        }
    }
}
