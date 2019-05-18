using Common;
using UnityEngine;

namespace DMZL_UI
{
    public class UI_SystemPanel :SceneUI
    {
        UI_Opinion[] opinions;
        public override void Initialize ()
        {
            opinions = GetComponentsInChildren<UI_Opinion>();
            //再Update中检测是否按ESC
            UpdateManager.I.OnSystemUpdate(systemWindowESCIO);
        }
        protected override void Start ()
        {
            opinions[0].Init(new VO_Opinion("WordTable_Test","AttackGate","AttackGateExplain",() =>Alert.Log("AttackGate")));
            opinions[1].Init(new VO_Opinion("WordTable_Test","LeaveGate","LeaveGateExplain",() => Alert.Log("LeaveGate")));
        }
        void systemWindowESCIO ()
        {
            if ( Input.GetKeyDown(KeyCode.Escape) )
            {
                //如果系统窗口关闭则打开，窗口打开则关闭
                if ( !IO )
                {
                    Show();
                }
                else
                {
                   Hide();
                }
            }
        }
    }
}
