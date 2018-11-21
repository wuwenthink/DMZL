using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;

public class Scene_Fight : MonoBehaviour {
    public List<GameObject> ourBornPoints;//我方出生点
    public List<GameObject> enemyBornPoints;//敌人出生点
    Camera cameraScene;
    GameObject Map_Role;
    BoxCollider2D Role_Box;

    /// <summary>
    /// 每个格子的大小：移动的固定距离
    /// </summary>
    public float fixedDistance;

    private void Awake()
    {
        Map_Role = FindObjectOfType<RoleControl_Move>().gameObject;
        Role_Box = Map_Role.GetComponent<BoxCollider2D>();
        cameraScene = NGUITools.FindCameraForLayer(Map_Role.layer);
    }

    void Start () {
        FindObjectOfType<Scene_City>().collision_Wall.GetComponent<PolygonCollider2D>().isTrigger = true;

        CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_FightEnter, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one)
            .GetComponent<FightEnterUI>().FightEnter(true, false, true, true, true, true, true, true);

    }
	

	void Update () {
        

    }

    /// <summary>
    /// 角色控制：生成和响应的判断
    /// </summary>
    public void RoleControl()
    {
        foreach (var item in FindObjectsOfType<MapData_Fight>())
        {
            if (item.bgType == "ourBorn")
            {
                ourBornPoints.Add(item.gameObject);
            }
            else if(item.bgType == "enemyBorn")
            {
                enemyBornPoints.Add(item.gameObject);
            }
        }
        List<GameObject> ourPos = ourBornPoints;//我方出生点
        List<GameObject> enemyPos = enemyBornPoints;//敌人出生点
        //生成我方队员
        foreach (var item in RunTime_Data.team_Our)
        {
            int num = Random.Range(0, ourPos.Count - 1);
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Map_Role.transform, ourPos[num].transform, Vector3.zero, Vector3.one);
            //替换角色形象，赋予角色参数的部分
            //
            ourPos.Remove(ourPos[num]);
        }

        //生成敌方队员
        foreach (var item in RunTime_Data.team_Enemy)
        {
            int num = Random.Range(0, enemyPos.Count - 1);
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Map_Role.transform, enemyPos[num].transform, Vector3.zero, Vector3.one);
            //替换角色形象，赋予角色参数的部分
            //
            enemyPos.Remove(enemyPos[num]);
        }
    }

    public void giveUpAction()
    {
        Map_Role.transform.localPosition = Map_Role.GetComponent<RoleControl_Move>().startPos;

    }

}
