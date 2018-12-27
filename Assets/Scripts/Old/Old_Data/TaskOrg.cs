// ======================================================================================
// 文 件 名 称：TaskOrg.cs
// 版 本 编 号：v1.1.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-20 11:49:09
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-21
// ======================================================================================
// 功 能 描 述：组织任务
// ======================================================================================

using System.Collections.Generic;

public class TaskOrg
{
    public int id { private set; get; }
    public int taskId { private set; get; }
    public int orgId { private set; get; }
    public List<int> publicPost { private set; get; }
    public List<int> getPost { private set; get; }
    public int addPerformance { private set; get; }
    public List<string> addOrgProp { private set; get; }
    public int numPerMonth { private set; get; }
    public bool isDaily { private set; get; }

    public TaskOrg (int _id)
    {
        TaskOrg taskOrg = SelectDao.GetDao ().SelectTaskOrg (_id);
        id = taskOrg.id;
        taskId = taskOrg.taskId;
        orgId = taskOrg.orgId;
        publicPost = taskOrg.publicPost;
        getPost = taskOrg.getPost;
        addPerformance = taskOrg.addPerformance;
        addOrgProp = taskOrg.addOrgProp;
        numPerMonth = taskOrg.numPerMonth;
        isDaily = taskOrg.isDaily;
    }

    public TaskOrg (int _id, int _taskId, int _orgId, string _publicPost, string _getPost, int _addPerformance, string _addOrgProp, int _numPerMonth, int _isDaily)
    {
        id = _id;
        taskId = _taskId;
        orgId = _orgId;
        if (!_publicPost.Equals ("0"))
        {
            publicPost = new List<int> ();
            string [] reg = _publicPost.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                publicPost.Add (tempId);
            }
        }
        else
            publicPost = null;
        if (!_getPost.Equals ("0"))
        {
            getPost = new List<int> ();
            string [] reg = _getPost.Split (';');
            foreach (string str in reg)
            {
                int tempId = int.Parse (str);
                getPost.Add (tempId);
            }
        }
        else
            getPost = null;
        addPerformance = _addPerformance;
        if (!_addOrgProp.Equals ("0"))
        {
            addOrgProp = new List<string> ();
            string [] reg = _addOrgProp.Split (';');
            foreach (string str in reg)
            {
                addOrgProp.Add (str);
            }
        }
        else
            addOrgProp = null;
        numPerMonth = _numPerMonth;

        isDaily = _isDaily == 1 ? true : false;
    }
}