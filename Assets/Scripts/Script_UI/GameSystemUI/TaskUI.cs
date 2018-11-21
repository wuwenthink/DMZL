// ====================================================================================== 
// 文件名         ：    TaskUI.cs                                                         
// 版本号         ：    v1.1.0                                          
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-22                                                                  
// 最后修改日期   ：    2017-10-29                                                           
// ====================================================================================== 
// 功能描述       ：    任务UI                                                                  
// ====================================================================================== 

using UnityEngine;

/// <summary>
/// 任务UI
/// </summary>
public class TaskUI : MonoBehaviour
{
    #region GameObjects
    public GameObject Button_DesBack;
    public GameObject Sprite_Black;
    
    public GameObject Button_Title_Personal;
    public GameObject Button_Title_Official;
    public GameObject GameObject_Pos_Task;
    public GameObject Button_TaskEX;

    #endregion
    private Transform UI_Root;

    GameObject chooseTask;
    private TaskStore taskStore;
    // 初始化的方法
    void Start ()
    {
        ClickController ();

        UI_Root = FindObjectOfType<UIRoot> ().transform;

        CommonFunc.GetInstance.SetUIPanel (gameObject);


    }

    /// <summary>
    /// 显示所有事务
    /// </summary>
    public void ShowAll ()
    {
        taskStore = TaskStore.GetInstance;

        foreach (int id in taskStore.tasks_gotton.Keys)
        {
            GameObject taskObj = Instantiate (Button_TaskEX);
            taskObj.name = id.ToString ();

            TaskType type = (TaskType) taskStore.tasks_gotton [id].type;
            if (type == TaskType.身份考核任务 || type == TaskType.身份职能任务 || type == TaskType.身份获得任务)
            {


            }

            UIEventListener.Get (taskObj).onClick = SetInfo;
        }
        Button_TaskEX.SetActive (false);

    }

    private void SetInfo (GameObject btn)
    {
        CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_TaskInfo, UI_Root.transform, Vector3.zero, Vector3.one).GetComponent<TaskInfoUI> ().SetInfo (int.Parse (btn.name));
    }

    private void ClickController ()
    {
        UIEventListener.Get (Sprite_Black).onClick = Back;
        UIEventListener.Get (Button_DesBack).onClick = Back;
    }


    private void Back (GameObject btn)
    {
        Destroy (gameObject);
    }

}
