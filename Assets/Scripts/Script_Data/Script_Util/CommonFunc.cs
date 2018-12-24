// ======================================================================================
// 文 件 名 称：CommonFunc.cs
// 版 本 编 号：v1.2.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-29 11:40:13
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-18
// ======================================================================================
// 功 能 描 述：通用方法
// ======================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tiled2Unity;
using System.Text;

public class CommonFunc : MonoBehaviour
{
    private static CommonFunc _instance;

    public static CommonFunc GetInstance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake ()
    {
        _instance = this;
    }

    public GameObject OpenWindow { get; set; }

    /// <summary>
    /// 地图实例化
    /// </summary>
    /// <param name="_prefabPath"></param>
    /// <param name="_parent"></param>
    /// <param name="_localPosion"></param>
    /// <param name="_localScale"></param>
    /// <returns></returns>
    public Transform Map_Instantiate(string _prefabPath, Transform _parent)
    {
        Transform target = (Resources.Load(_prefabPath) as GameObject).transform;
        if (!target)
        {
            Debug.LogError(_prefabPath + " Not Found!!!");
            return null;
        }
        Transform clone = Instantiate(target, _parent);
        float posX = clone.GetComponent<TiledMap>().MapWidthInPixels * -1f / 200f;
        float posY = clone.GetComponent<TiledMap>().MapHeightInPixels / 200f;
        clone.localPosition = new Vector3(posX, posY,0);
        clone.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        OpenWindow = clone.gameObject;
        return clone;
    }

    /// <summary>
    /// 地图实例化
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_parent"></param>
    /// <param name="_localPosion"></param>
    /// <param name="_localScale"></param>
    /// <returns></returns>
    public Transform Map_Instantiate(Transform _target, Transform _parent)
    {
        Transform clone = Instantiate(_target, _parent);
        float posX = clone.GetComponent<TiledMap>().MapWidthInPixels * (-1 / 200);
        float posY = clone.GetComponent<TiledMap>().MapHeightInPixels / 200;
        clone.localPosition = new Vector3(posX, posY, 0);
        clone.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        return clone;
    }

    /// <summary>
    /// UI实例化
    /// </summary>
    /// <param name="_prefabPath"></param>
    /// <param name="_parent"></param>
    /// <param name="_localPosion"></param>
    /// <param name="_localScale"></param>
    /// <returns></returns>
    public Transform UI_Instantiate (string _prefabPath, Transform _parent, Vector3 _localPosion, Vector3 _localScale)
    {
        Transform target = (Resources.Load (_prefabPath) as GameObject).transform;
        if (!target)
        {
            Debug.LogError (_prefabPath + " Not Found!!!");
            return null;
        }
        Transform clone = Instantiate (target, _parent);
        clone.localPosition = _localPosion;
        clone.localScale = _localScale;

        OpenWindow = clone.gameObject;
        return clone;
    }

    /// <summary>
    /// UI实例化
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_parent"></param>
    /// <param name="_localPosion"></param>
    /// <param name="_localScale"></param>
    /// <returns></returns>
    public Transform UI_Instantiate (Transform _target, Transform _parent, Vector3 _localPosion, Vector3 _localScale)
    {
        Transform clone = Instantiate (_target, _parent);
        clone.localPosition = _localPosion;
        clone.localScale = _localScale;
        return clone;
    }

    /// <summary>
    /// Loading方法。必须使用StartCoroutine()进行引用！
    /// </summary>
    /// <returns></returns>
    public IEnumerator ToSceneLoading (string sceneName)
    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation op = ToScence (sceneName);
        op.allowSceneActivation = false;
        Transform go = UI_Instantiate (Data_Static.UIpath_SceneLoading, FindObjectOfType<UIRoot> ().transform, Vector3.zero, Vector3.one);
        LoadingSceneUI loading_Animation = go.GetComponentInChildren<LoadingSceneUI> ();
        while (op.progress < 0.9f)
        {
            toProgress = (int) op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                loading_Animation.setAnimation (displayProgress);
                yield return new WaitForEndOfFrame ();
            }
        }

        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            loading_Animation.setAnimation (displayProgress);
            yield return new WaitForEndOfFrame ();
        }
        op.allowSceneActivation = true;
    }

    /// <summary>
    /// 跳转到场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    /// <returns></returns>
    public AsyncOperation ToScence (string sceneName)
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Single);
        return AO;
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void ToCloseGame ()
    {
        Application.Quit ();
    }

    /// <summary>
    /// 按钮选中效果——鼠标经过
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="isHover"></param>
    public void ButtonSelected_Hover (GameObject btn, bool isHover)
    {
        if (isHover)
        {
            foreach (Transform child in btn.GetComponentsInChildren<Transform> ())
            {
                if (child.name == "Sprite_Choose")
                {
                    child.gameObject.GetComponent<UISprite> ().enabled = true;
                }
            }
        }
        else
        {
            foreach (Transform child in btn.GetComponentsInChildren<Transform> ())
            {
                if (child.name == "Sprite_Choose")
                {
                    child.gameObject.GetComponent<UISprite> ().enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// 按钮选中效果——点击
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="isHover"></param>
    public void ButtonSelected_Click (GameObject btn)
    {
        foreach (Transform child in btn.GetComponentsInChildren<Transform> ())
        {
            if (child.name == "Sprite_Choose")
            {
                child.gameObject.GetComponent<UISprite> ().enabled = true;
            }
        }
    }

    ///// <summary>
    ///// 处理金钱显示
    ///// </summary>
    ///// <param name="moneyCount">金钱总数</param>
    ///// <param name="currencyDic">其他货币（id，数量），非背包时传入Null</param>
    ///// <param name="parent_GO">父物体</param>
    ///// <returns></returns>
    //public void MoneyDeal(int moneyCount, Dictionary<int, CurrencyHas> currencyDic, Transform parent_GO)
    //{
    //    int copper_value = moneyCount % 1000;
    //    int silver_value = moneyCount / 1000;

    //    int currencyTypeCount = -1;

    //    // 显示银子
    //    if (silver_value > 0)
    //    {
    //        ShowOneCurrency(parent_GO, currencyTypeCount, 2, silver_value);
    //    }

    //    // 显示铜板
    //    ShowOneCurrency(parent_GO, currencyTypeCount, 1, copper_value);

    //    // 背包情况，显示其他货币
    //    if (currencyDic != null)
    //    {
    //        foreach (int key in currencyDic.Keys)
    //        {
    //            if (key == 1 || key == 2)
    //                continue;
    //            ShowOneCurrency(parent_GO, currencyTypeCount, key, currencyDic[key].count);
    //        }
    //    }
    //}

    ///// <summary>
    ///// 显示一种货币
    ///// </summary>
    ///// <param name="parent_GO">父物体</param>
    ///// <param name="currencyTypeCount">货币的种类index</param>
    ///// <param name="currencyId">货币id</param>
    ///// <param name="count">数量</param>
    //private void ShowOneCurrency(Transform parent_GO, int currencyTypeCount, int currencyId, int count)
    //{
    //    GameObject currencyGo;
    //    currencyGo = UI_Instantiate(Data_Static.UIpath_Price_MoneyEX, parent_GO, new Vector3(-60 + 60 * ++currencyTypeCount % 4, -40 * currencyTypeCount / 4, 0), Vector3.one).gameObject;
    //    var sprite = currencyGo.GetComponentInChildren<UISprite>();
    //    sprite.atlas = FindObjectOfType<GameObject_Static>().UIAtlas_Icon_Item1;
    //    sprite.name = Constants.Currency_All[currencyId].icon;
    //    currencyGo.GetComponentInChildren<UILabel>().text = count.ToString();
    //}

    /// <summary>
    /// 生成道具说明Tips界面并隐藏
    /// </summary>
    /// <returns></returns>
    public GameObject Ins_ItemTips (GameObject ItemTip)
    {
        if (ItemTip != null)
        {
            ItemTip = FindObjectOfType<ItemTipsUI> ().gameObject;
        }
        else
        {
            ItemTip = UI_Instantiate (Data_Static.UIpath_ItemTips, FindObjectOfType<UIRoot> ().transform, new Vector3 (35, 35, 0), Vector3.one).gameObject;
        }
        ItemTip.SetActive (false);
        return ItemTip;
    }


    /// <summary>
    /// 设置界面UIPanel深度
    /// </summary>
    /// <param name="go"></param>
    public void SetUIPanel (GameObject go)
    {
        int depth = go.GetComponent<UIPanel> ().depth;
        foreach (UIPanel u in go.GetComponentsInChildren<UIPanel> ())
        {
            if (u.gameObject != go)
            {
                u.depth = depth + 1;
            }
            depth++;
        }
    }

    /// <summary>
    /// 设置按钮启用与否
    /// </summary>
    /// <param name="btn">按钮的GameObject</param>
    /// <param name="state">是否启用</param>
    public void SetButtonState (GameObject btn, bool state)
    {
        if (state)
        {
            btn.GetComponent<BoxCollider> ().enabled = true;
            btn.GetComponent<UIButton> ().SetState (UIButtonColor.State.Normal, true);
        }
        else
        {
            btn.GetComponent<BoxCollider> ().enabled = false;
            btn.GetComponent<UIButton> ().SetState (UIButtonColor.State.Disabled, true);
        }
    }

    /// <summary>
    /// 设置按钮按下与弹起
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="pressed"></param>
    public void SetButtonPress (GameObject btn, bool pressed)
    {
        if (pressed)
        {
            btn.GetComponent<BoxCollider> ().enabled = false;
            btn.GetComponent<UIButton> ().SetState (UIButtonColor.State.Pressed, true);
        }
        else
        {
            btn.GetComponent<BoxCollider> ().enabled = true;
            btn.GetComponent<UIButton> ().SetState (UIButtonColor.State.Normal, true);
        }
    }


    /// <summary>
    /// 计算最大值
    /// </summary>
    /// <param name="wo"></param>
    /// <returns></returns>
    public float maxValue(List<float> wo)
    {
        float max = wo[0];
        for (int x = 0; x < wo.Count; x++)
        {
            if (wo[x] > max)
            {
                max = wo[x];
            }
        }
        
        return max;
    }
    public int maxValue(List<int> wo)
    {
        int max = wo[0];
        for (int x = 0; x < wo.Count; x++)
        {
            if (wo[x] > max)
            {
                max = wo[x];
            }
        }

        return max;
    }
    /// <summary>
    /// 计算最小值
    /// </summary>
    /// <param name="wo"></param>
    /// <returns></returns>
    public float minValue(List<float> wo)
    {
        float min = wo[0];
        for (int x = 0; x < wo.Count; x++)
        {
            if (wo[x] < min)
            {
                min = wo[x]; 
            }
        }
        return min;
    }
    public int minValue(List<int> wo)
    {
        int min = wo[0];
        for (int x = 0; x < wo.Count; x++)
        {
            if (wo[x] < min)
            {
                min = wo[x];
            }
        }
        return min;
    }
    /// <summary>
    /// 判断UILable的文字颜色
    /// </summary>
    /// <param name="lable"></param>
    public void LableColor (UILabel lable)
    {
        int num = int.Parse (lable.text);
        if (num <= 0)
        {
            lable.color = Color.red;
        }
        else
        {
            lable.color = Color.black;
        }
    }

    /// <summary>
    /// 处理金钱的增减
    /// </summary>
    /// <param name="isAdd">是加还是减</param>
    /// <param name="moneyCount">金钱数量</param>
    public int ChangeMoney (bool isAdd, int moneyCount)
    {
        if (moneyCount < 0)
        {
            moneyCount = 0;
        }
        else if (moneyCount <= 100)
        {
            if (isAdd)
            {
                moneyCount += 1;
            }
            else
            {
                moneyCount -= 1;
            }
        }
        else if (moneyCount <= 1000)
        {
            if (isAdd)
            {
                moneyCount += 10;
            }
            else
            {
                moneyCount -= 10;
            }
        }
        else if (moneyCount <= 10000)
        {
            if (isAdd)
            {
                moneyCount += 100;
            }
            else
            {
                moneyCount -= 100;
            }
        }
        else if (moneyCount <= 100000)
        {
            if (isAdd)
            {
                moneyCount += 1000;
            }
            else
            {
                moneyCount -= 1000;
            }
        }
        else if (moneyCount <= 1000000)
        {
            if (isAdd)
            {
                moneyCount += 10000;
            }
            else
            {
                moneyCount -= 10000;
            }
        }
        else
        {
            if (isAdd)
            {
                moneyCount += 100000;
            }
            else
            {
                moneyCount -= 100000;
            }
        }
        return moneyCount;
    }

    /// <summary>
    /// 按身份生成随机NPC（工作人员）的数据
    /// </summary>
    /// <param name="_modelId">身份ID</param>
    /// <param name="_elite">精英概率</param>
    /// <returns></returns>
    //public People GenerateNpc (int _idenId, int _elite)
    //{
    //    Role_Main role;
    //    int min;
    //    int max;

    //    int id = RunTime_Data.Npc_Sequence++;

    //    var dao = SelectDao.GetDao ();
    //    var model = dao.SelectRole_GenerateByIden (_idenId);
    //    var rd = new System.Random (id);
    //    var lm = LanguageMgr.GetInstance;
    //    var male = lm.GetText ("Role_80");
    //    var female = lm.GetText ("Role_81");

    //    bool _isElite = rd.Next (0, 100) < _elite ? true : false;

    //    string gender = rd.Next (0, 100) > model.male ? female : male;
    //    string name = (dao.SelectRoleRandomInfo_Lastname ()) + (gender.Equals (male) ? dao.SelectRoleRandomInfo_Malename () : dao.SelectRoleRandomInfo_Femalname ());
    //    string word = dao.SelectRoleRandomInfo_Wordname ();
    //    string title = dao.SelectRoleRandomInfo_Titlename ();
    //    int birth = rd.Next (model.ageRange [0], model.ageRange [1]);
    //    string place = dao.SelectRoleRandomInfo_Birthfu ();
    //    //story
    //    var num = rd.Next (1, 30);
    //    string headIcon = "Icon_Role0" + (num > 10 ? num + "" : "0" + num);
    //    string dragenBonesName = "";
    //    int nature = rd.Next (1, 59);

    //    if (_isElite)
    //    {
    //        min = 40;
    //        max = 70;
    //    }
    //    else
    //    {
    //        min = 60;
    //        max = 90;
    //    }
    //    role = new Role_Main(id, name, gender, birth, place, "", headIcon, dragenBonesName, nature, rd.Next(min, max), rd.Next(min, max), rd.Next(min, max), rd.Next(min, max), rd.Next(min, max), rd.Next(min, max), rd.Next(min, max), rd.Next(min, max), rd.Next(min, max), rd.Next(min, max), rd.Next(min, max), 0);

    //    var knowledge = _isElite ? model.eliteKnowledge : model.knowledge;
    //    var skill = _isElite ? model.eliteSkill : model.skill;
    //    foreach (var item in knowledge)
    //    {
    //        string[] reg = item.Split(',');
    //        int kid = int.Parse(reg[0]);
    //        role.Learn_Knowledge(kid);
    //        int level = rd.Next(int.Parse(reg[1]), int.Parse(reg[2]) + 1);
    //        role.Learn_Knowledge(kid, role.KnowLedgeDic[kid].grow.growData[level].exp);
    //    }
    //    foreach (var item in skill)
    //    {
    //        string[] reg = item.Split(',');
    //        int sid = int.Parse(reg[0]);
    //        role.Learn_Skill(sid);
    //        int level = rd.Next(int.Parse(reg[1]), int.Parse(reg[2]) + 1);
    //        role.Learn_Skill(sid, role.SkillDic[sid].grow.growData[level].exp);
    //    }

    //    string [] salaryRange = (_isElite ? dao.SelectSystem_Config (70).value : dao.SelectSystem_Config (71).value).Split (';');
    //    int percent = rd.Next (int.Parse (salaryRange [0]), int.Parse (salaryRange [1]));
    //    int salary = dao.SelectIdentity (model.idenId).salary [0] * percent / 100;

    //    return new People (role, salary);
    //}

    /// <summary>
    /// 按模板生成随机NPC数据
    /// </summary>
    /// <param name="_modelId">模板ID</param>
    /// <returns></returns>
    //public Role_Main GenerateNpc (int _modelId)
    //{
    //    Role_Main role;
    //    int min;
    //    int max;

    //    int id = RunTime_Data.Npc_Sequence++;

    //    var dao = SelectDao.GetDao ();
    //    var model = dao.SelectRole_Generate (_modelId);
    //    var rd = new System.Random (id);
    //    var lm = LanguageMgr.GetInstance;
    //    var male = lm.GetText ("Role_80");
    //    var female = lm.GetText ("Role_81");
    //    string gender = rd.Next (0, 100) > model.male ? female : male;
    //    string name = (dao.SelectRoleRandomInfo_Lastname ()) + (gender.Equals (male) ? dao.SelectRoleRandomInfo_Malename () : dao.SelectRoleRandomInfo_Femalname ());
    //    string word = dao.SelectRoleRandomInfo_Wordname ();
    //    string title = dao.SelectRoleRandomInfo_Titlename ();
    //    int birth = rd.Next (model.ageRange [0], model.ageRange [1]);
    //    string place = dao.SelectRoleRandomInfo_Birthfu ();
    //    //story
    //    var num = rd.Next (1, 30);
    //    string headIcon = "Icon_Role0" + (num > 10 ? num + "" : "0" + num);
    //    string dragenBonesName = "";
    //    int nature = rd.Next (1, 59);

    //    min = 40;
    //    max = 70;
    //    role = new Role_Main (id, name,  gender, birth, place, "", headIcon, dragenBonesName, nature, rd.Next (min, max), rd.Next (min, max), rd.Next (min, max), rd.Next (min, max), rd.Next (min, max), rd.Next (min, max), rd.Next (min, max), rd.Next (min, max), rd.Next (min, max), rd.Next (min, max), rd.Next (min, max), 0);

    //    return role;
    //}
}