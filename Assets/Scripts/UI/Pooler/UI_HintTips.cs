using System.Text;
using Common;
using MainSpace;
using StorySpace;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DMZL_UI
{
    /// <summary>
    /// 提示Tips
    /// </summary>
    public class UI_HintTips :UIBehaviour, IPooler
    {
        Text NewsType;
        Text NewsTitle;
        Button button;
        Button eliminate;

        VO_Hint vo;
        /// <summary>
        /// 消息VO
        /// </summary>
        public VO_Hint VO
        {
            get => vo;
            set
            {
                vo = value;
                LanguageManager.I.TextRegister(LanguageKey.Word_UI,vo.HintType.ToString(),NewsType);

                StringBuilder @string = new StringBuilder(vo.Content.ToString());

                if (vo.Number!=0) @string .Append(" x "+ vo.Number.ToString());
                NewsTitle.text = @string.ToString();
            }
        }

        protected override void Awake ()
        {
            NewsType = transform.FindChildByName("NewsType").GetComponent<Text>();
            NewsTitle= transform.FindChildByName("NewsTitle").GetComponent<Text>();
            button = GetComponent<Button>();
            eliminate = transform.FindChildByName("Eliminate").GetComponent<Button>();
                
            //按下按钮查看提示
            button.onClick.AddListener(() => MainSystem.I.SendMessage(MainMessages.ReadHint,VO));
            //按下按钮消除提示
            eliminate.onClick.AddListener(() => MainSystem.I.SendMessage(MainMessages.EliminateHint,VO.Id));
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init ( VO_Hint VO )
        {
            this.VO = VO;
            //如果提示被阅读过，则隐藏消除按钮
            if ( VO.Read ) eliminate.gameObject.SetActive(false);
            else eliminate.gameObject.SetActive(true);
        }

        public void OnReset ()
        {
           
        }

        public void Recover ()
        {
            LanguageManager.I.RemoveText(NewsType);
            NewsTitle.text = null;
            vo  = default;
        }
    }
}
