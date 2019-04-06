using BuildTemplate;
using Map;
using RoleSpace;
using RoleTemplate;
using Template;
using UnityEngine;


public class InteractTest :MonoBehaviour
{
    private void Awake ()
    {
        RoleSystem.I.Start();
        MapSystem.I.Start();
    }
    private void Start ()
    {

        //读取Player实例，并初始化
        Player_city player = Instantiate(Resources.Load<Player_city>("Player_city"));
        player.Init(new RoleInfo()
        {
            RoleId = 0,
            Name = "测试角色",
            VechicleName = "Foot"
        });
        Trap_Test trap = Instantiate(Resources.Load<Trap_Test>("Trap_Test"));
        trap.Init(new BuildInfo()
        {
            BuildId = 0,
            Location_x = 3,
            Location_y = 1,
            Process1 = "ProTrapTest",
            Date1 = "测试成功ye~~"
        });
        Shop_Test shop = Instantiate(Resources.Load<Shop_Test>("Shop_Test"));
        shop.Init(new BuildInfo()
        {
            BuildId = 0,
            Location_x = -3,
            Location_y = 1
        });
    }
}
