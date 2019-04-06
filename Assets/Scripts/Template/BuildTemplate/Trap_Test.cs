using Common;
using Map;
using RoleSpace;
using UnityEngine;

namespace BuildTemplate
{
    /// <summary>
    /// 测试陷阱
    /// </summary>
    public class Trap_Test :Build
    {
        private void OnTriggerEnter2D ( Collider2D collision )
        {
            //如果碰到玩家，则触发与建筑的互动
            if ( collision.tag == "Player" )
            {
                Role player = collision.GetComponent<Role>();
                MapSystem.I.SendMessage(MapMessage.Build + MapMessage.Interact,player,this);
            }
        }

        public override void Initialize ()
        {
            //将处理器集合中添加此处理器
            ProcessList.Add(new ChangeBuildColor_Blue());
        }
    }

    /// <summary>
    /// 改变建筑颜色为蓝色
    /// </summary>
    class ChangeBuildColor_Blue :IProcess<Role,Build>
    {
        public void Function ( Role role,Build build )
        {
            //建筑颜色变为蓝色
            build.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}