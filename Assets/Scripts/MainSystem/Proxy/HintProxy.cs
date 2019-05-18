using System;
using Common;
using DMZL_UI;

namespace MainSpace
{
    public  class HintProxy :IProxy
    {
        public Type ProxyType =>typeof(HintProxy);

        public string[] ListeningMessages
        {
            get => new string[]
                {
                   MainMessages.SendHint,
                   MainMessages.ReadHint,
                   MainMessages.EliminateHint
                };
        }
        public void HandleMessage ( string letter,params object[] data )
        {
            switch ( letter )
            {
                case MainMessages.SendHint:
                    SendHint((VO_Hint)data[0]);
                    break;
                case MainMessages.ReadHint:
                    ReadHint((VO_Hint)data[0]);
                    break;
                case MainMessages.EliminateHint:
                    EliminateHint(Convert.ToInt32(data[0]));
                    break;
            }
        }
        UI_HintPanel hintPanel;

        public void Initialize ()
        {
            hintPanel = UIManager.I.GetSceneUI<UI_HintPanel>();
        }
        public void Close (){}

     
        /// <summary>
        /// 发出提示
        /// </summary>
        /// <param name="vo"></param>
        void SendHint ( VO_Hint vo)
        {
            //将提示插入数据库
            int Id = SaveManager.I.InsertBaseFrom("Main_Hint",(int)vo.HintType + "," + vo.Content + "," + vo.Number + ", 0");
            vo.Id = Id;
            //面板加入提示
            hintPanel.AddNewHint(vo);
            hintPanel.Show();
        }

        /// <summary>
        /// 阅读提示
        /// </summary>
        /// <param name="vo"></param>
        void ReadHint ( VO_Hint vo )
        {
            //如果提示没有被读过，则消除数据库并从提示面板消除
            if ( !vo.Read )
            {
                EliminateHint(vo.Id);
            }
        }

        /// <summary>
        /// 消除提示
        /// </summary>
        void EliminateHint (int Id)
        {
            SaveManager.I.Updata("Main_Hint","Read = 1","Id=" + Id.ToString());
            hintPanel.RemoveNewHint(Id);
        }
  
    }
}
