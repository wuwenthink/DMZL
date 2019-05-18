using Common;
using UnityEngine.UI;

namespace DMZL_UI
{
    public class UI_SceneLoading :SceneUI
    {
        Slider slider;
        public override void Initialize ()
        {
            slider =  GetComponentInChildren<Slider>();
        }

        /// <summary>
        /// 给与一个值
        /// </summary>
        /// <param name="num"></param>
        public void SetValue (float num)
        {
            slider.value = num;
        }
    }
}
