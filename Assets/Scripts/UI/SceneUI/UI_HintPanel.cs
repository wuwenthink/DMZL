using UnityEngine;
using Common;
using System.Collections.Generic;
using MainSpace;
using System;
using UnityEngine.UI;

namespace DMZL_UI
{
    public class UI_HintPanel :SceneUI
    {
        Transform Content;
        List<UI_HintTips> tipsList;

        Button historyButton;

        public override void Initialize ()
        {
            tipsList = new List<UI_HintTips>();

            //获取内容transform
            Content = transform.FindChildByName("Content");
            //获取历史按钮
            historyButton = transform.FindChildByName("HistoryButton").GetComponent<Button>();
            LanguageManager.I.TextRegister(LanguageKey.Word_UI,"History",historyButton.GetComponentInChildren<Text>());
            historyButton.onClick.AddListener(()=>UIManager.I.GetWindow<UI_HintHistoryWindow>().Show());
        }

        protected override void Start ()
        {
            //从数据库读取未读提示数据
            object[][] data = SaveManager.I.Select("*","Main_Hint","Read=0");

            //将数据转化成VO，并增加新提示
            VO_Hint.DataToVOs(data).Foreach(e => AddNewHint(e));
            Show();
        }

        /// <summary>
        /// 增加新提示
        /// </summary>
        /// <param name="vo"></param>
        public void AddNewHint ( VO_Hint vo )
        {
            //从对象池获取Tips并初始化
            UI_HintTips tips = Pool.I.GetObject(BundleManager.I.Load<UI_HintTips>("UI_HintTips"));
            tips.Init(vo);
            tipsList.Add(tips);
            tips.transform.parent = Content;
            UIManager.I.GetSceneUI<UI_HintButton>().NewHintNmuber++;
        }

        /// <summary>
        /// 消除新提示
        /// </summary>
        /// <param name="vo"></param>
        public void RemoveNewHint ( int Id )
        {
            //如果有此条消息则回收它
            for ( int i = tipsList.Count-1 ; i >= 0 ; i-- )
            {
                if ( tipsList[i].VO.Id == Id )
                {
                    Pool.I.CollectObject(tipsList[i]);
                    tipsList.RemoveAt(i);
                    //未读提示数量减1
                    UIManager.I.GetSceneUI<UI_HintButton>().NewHintNmuber--;
                }
            }
        }

    }
}
