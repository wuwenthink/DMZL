// ====================================================================================== 
// 文件名              ：    BuildModeUI.cs                                                         
// 版本号              ：    v1 .1.1                                                 
// 作者                  ：    xic                                                          
// 创建日期           ：    2017-9-18                                           
// 最后修改日期     ：    2017-10-07 17:33:12                                                          
// ====================================================================================== 
// 功能描述           ：     建筑模式UI                                                                 
// ======================================================================================

using System.Collections.Generic;
using UnityEngine;
using LitJson;
using SimpleJSON;
using System.Text.RegularExpressions;

public class BuildModeUI : MonoBehaviour 
{
    public GameObject GameObject_Out;
    public GameObject Button_New;
    public GameObject Button_Old;

    public GameObject GameObject_Part;
    public GameObject Button_Nomal;
    public GameObject Button_Special;
    public GameObject Button_Clear;
    public GameObject Button_Return;
    public GameObject Button_Save;

    public GameObject GameObject_List;
    public GameObject Button_ItemEX;
    public GameObject GameObject_Pos;

    public GameObject GameObject_SceneInfo;
    public GameObject Button_Closei;
    public UIInput Input_SceneName;
    public GameObject Texture_ItemPici;
    public UIText Label_Model;
    public UIText Label_SizeX;
    public UIText Label_SizeY;
    public GameObject Button_Yes;

    public GameObject GameObject_Info;
    public GameObject Button_SceneInfo;
    public GameObject GameObject_PartCreating;
    public GameObject Button_SavePart;
    public UIText Label_CreatingName;
    public UIText Label_PartAreaSizeX;
    public UIText Label_PartAreaSizeY;
    public GameObject Button_PartInfo;
    public GameObject Button_DeletePart;
    public UIText Label_Tip;

    /// <summary>
    /// 是否为开发者模式
    /// </summary>
    private bool isDeveloperPattern;
    /// <summary>
    /// 是否在选择场景界面
    /// </summary>
    private bool isOut;
    private Dictionary<int, Map_SceneModel> Have_SceneModel;
    /// <summary>
    /// 单元格大小
    /// </summary>
    public float GridSize;
    Dictionary<string, string> Dic_AllSceneMain;
    string sceneName_Now;
    SystemTipsUI systemTips;
    ButtonTipsUI buttonTips;
    LableTipsUI tipLabel;
    void Start () 
	{
        GridSize = 0.64f;
        ClickControl();

        InitModel();
        systemTips = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_SystemTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject.GetComponent<SystemTipsUI>();
        systemTips.gameObject.SetActive(false);

        buttonTips = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_ButtonTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject.GetComponent<ButtonTipsUI>();
        buttonTips.gameObject.SetActive(false);

    }
	
	
	void Update () 
	{
        if (UICamera.isOverUI)
        {
            return;
        }
        else
        {
            MouseControl();
        }

    }

    private void FixedUpdate()
    {

    }

    /// <summary>
    /// 场景初始化
    /// </summary>
    void InitModel()
    {
        GameObject_Out.SetActive(true);
        GameObject_Part.SetActive(false);
        isOut = true;
        GameObject_SceneInfo.SetActive(false);
        GameObject_GroundBox.SetActive(false);
        GameObject_Info.SetActive(false);
        GameObject_PartCreating.SetActive(false);
        IsSelected = false;
        Have_SceneModel = new Dictionary<int, Map_SceneModel>();
        Dic_NowScene = new Dictionary<int, MapScene>();

        //判断是否为开发者模式，可以创建新的场景，否则只能修改已有的场景。
        isDeveloperPattern = true;
        //非开发者的入口在主菜单或者游戏内，不显示场景创建的选项，而是从入口处选择参数。
        if (isDeveloperPattern)
        {
            //默认读取模板数据
            RefreshSceneList(Button_New);
        }

    }

    /// <summary>
    /// 刷新模板场景列表
    /// </summary>
    /// <param name="btn"></param>
    void RefreshSceneList(GameObject btn)
    {

        Button_ItemEX.SetActive(true);
        NGUITools.DestroyChildren(GameObject_Pos.transform);
        //刷新表格中的模板数据，创建模板初始数据
        if (btn == Button_New)
        {
            //在列表中刷新读取表格中的模板数据
            Have_SceneModel = SelectDao.GetDao().SelectAllMap_SceneModel();
            if (Have_SceneModel.Count > 0)
            {
                CommonFunc.GetInstance.SetButtonState(Button_New, true);

                foreach (KeyValuePair<int, Map_SceneModel> item in Have_SceneModel)
                {
                    Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_ItemEX.transform, GameObject_Pos.transform, Vector3.zero, Vector3.one);
                    go.Find("Label_Name").GetComponent<UIText>().SetText(false, item.Value.writeName);
                    UITexture ut = go.Find("Texture_ItemPic").GetComponent<UITexture>();
                    ut.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_SceneModel + item.Value.icon);
                    ut.width = 64;
                    ut.height = 64;
                    go.name = item.Key.ToString();
                    UIEventListener.Get(go.gameObject).onClick = NewModelInfo;
                }
                GameObject_Pos.GetComponent<UIGrid>().enabled = true;
            }
            else
            {
                CommonFunc.GetInstance.SetButtonState(Button_New, false);
            }
            Button_ItemEX.SetActive(false);
        }
        //刷新已创建数据的模板列表，读取里面的文本
        else
        {
            Dic_NowScene.Clear();
            TextAsset[] ta = Resources.LoadAll(Data_Static.save_pathScene_Main) as TextAsset[];
            if (ta == null)
            {
                GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
                go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_37"), LanguageMgr.GetInstance.GetText("BuildingMode_36"));
                return;
            }

            foreach (var item in ta)
            {
                JsonData jd = JsonMapper.ToObject(item.text);
                MapScene sceneData = new MapScene();
                sceneData.id = (int)jd["id"];
                sceneData.writeName = jd["writeName"].ToString();
                sceneData.modelID = (int)jd["modelID"];
                sceneData.Dic_MapPart = new Dictionary<int, MapPart>();
                Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(sceneData.modelID);
                Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_ItemEX.transform, GameObject_Pos.transform, Vector3.zero, Vector3.one);
                go.Find("Label_Name").GetComponent<UIText>().SetText(false, sceneData.writeName);
                UITexture ut = go.Find("Texture_ItemPic").GetComponent<UITexture>();
                ut.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_SceneModel + ms.icon);
                ut.width = 64;
                ut.height = 64;
                go.name = sceneData.id.ToString();
                UIEventListener.Get(go.gameObject).onClick = LoadModel;
                Dic_NowScene.Add(sceneData.id, sceneData);
            }
            GameObject_Pos.GetComponent<UIGrid>().enabled = true;
            Button_ItemEX.SetActive(false);
        }
    }


    /// <summary>
    /// 创建新模板的界面（基于表格生成初始数据）
    /// </summary>
    public void NewModelInfo(GameObject btn)
    {
        GameObject_SceneInfo.SetActive(true);
        int id = int.Parse(btn.name);
        Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(id);
        //输入名称初始化
        sceneName_Now = "";
        Input_SceneName.value = ms.writeName;
        Input_SceneName.characterLimit = 12;
        Input_SceneName.validation = UIInput.Validation.Filename;
        EventDelegate.Add(Input_SceneName.onChange, Rename_SceneInfo);
        Rename_SceneInfo();

        UITexture ut = Texture_ItemPici.GetComponent<UITexture>();
        ut.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_SceneModel + ms.icon);
        ut.width = 64;
        ut.height = 64;
        Label_Model.SetText(false, ms.id.ToString());
        Label_SizeX.SetText(false, ms.maxSize_X.ToString());
        Label_SizeY.SetText(false, ms.maxSize_Y.ToString());
        Button_Yes.GetComponentInChildren<UIText>().SetText(false, "读取模板");

        ModelID = id;
        UIEventListener.Get(Button_Yes).onClick = CreateNewModel;
    }


    /// <summary>
    /// 修改模板场景的界面（基于表格和已经完成的数据）
    /// </summary>
    /// <param name="btn"></param>
    void LoadModel(GameObject btn)
    {
        GameObject_SceneInfo.SetActive(true);
        int id = int.Parse(btn.name);
        newScene = new MapScene();
        newScene = Dic_NowScene[id];
        Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(id);
        //输入名称初始化
        sceneName_Now = "";
        Input_SceneName.value = newScene.writeName;
        Input_SceneName.characterLimit = 12;
        Input_SceneName.validation = UIInput.Validation.Filename;
        EventDelegate.Add(Input_SceneName.onChange, Rename_SceneInfo);
        Rename_SceneInfo();

        UITexture ut = Texture_ItemPici.GetComponent<UITexture>();
        ut.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_SceneModel + ms.icon);
        ut.width = 64;
        ut.height = 64;
        Label_Model.SetText(false, ms.id.ToString());
        Label_SizeX.SetText(false, ms.maxSize_X.ToString());
        Label_SizeY.SetText(false, ms.maxSize_Y.ToString());
        Button_Yes.GetComponentInChildren<UIText>().SetText(false, "读取模板");

        ModelID = newScene.modelID;
        UIEventListener.Get(Button_Yes).onClick = GetSceneFile;
        //读取json数据，创建场景
    }

    /// <summary>
    /// 保存场景模板时弹出的界面
    /// </summary>
    /// <param name="btn"></param>
    void SaveScene(GameObject btn)
    {
        GameObject_SceneInfo.SetActive(true);
        int id = int.Parse(btn.name);
        Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(id);
        UITexture ut = Texture_ItemPici.GetComponent<UITexture>();
        ut.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_SceneModel + ms.icon);
        ut.width = 64;
        ut.height = 64;
        Label_Model.SetText(false, ms.id.ToString());
        Label_SizeX.SetText(false, ms.maxSize_X.ToString());
        Label_SizeY.SetText(false, ms.maxSize_Y.ToString());
        Button_Yes.GetComponentInChildren<UIText>().SetText(false, "保存场景");
        Button_Yes.name = btn.name;
        UIEventListener.Get(Button_Yes).onClick = SaveSceneB;
    }

    /// <summary>
    /// 重命名判断：场景名称
    /// </summary>
    void Rename_SceneInfo()
    {
        sceneName_Now = "";
        if (Input_SceneName.value == "" || Input_SceneName.value.Contains(" ") || Input_SceneName.value == null)
        {
            tipLabel = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject.GetComponent<LableTipsUI>();
            tipLabel.gameObject.SetActive(true);
            tipLabel.SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_21"), LanguageMgr.GetInstance.GetText("BuildingMode_20"));
            Input_SceneName.value = LanguageMgr.GetInstance.GetText("BuildingMode_19");
        }
        else
        {
            sceneName_Now = Input_SceneName.value;
        }
    }
    /// <summary>
    /// 保存场景
    /// </summary>
    /// <param name="btn"></param>
    void SaveSceneB(GameObject btn)
    {
        //创建文本提示
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        //保存主数据、建筑数据、格子数据到一个文件中
        Dictionary<string, string> dic_AllPartData = new Dictionary<string, string>();
        string Grid = "";
        string Building = "";
        foreach (var item in newScene.Dic_MapPart)
        {
            //如果模块数量为0，无法保存
            if (newScene.Dic_MapPart.Count <= 0)
            {
                go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_14"), LanguageMgr.GetInstance.GetText("BuildingMode_16"));
            }
            else
            {
                //如果建筑条目数量不等于模块数量，可以保存但是弹出提醒
                if (Dic_SavePart_Building.Count != newScene.Dic_MapPart.Count)
                {
                    if (item.Value.Dic_Building.Count > 0)
                    {
                        Building = Dic_SavePart_Building[item.Value.id];
                    }
                    else
                    {
                        Building = "";
                    }
                    go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_14"),
                        string.Format(LanguageMgr.GetInstance.GetText("BuildingMode_17"), newScene.Dic_MapPart.Count - Dic_SavePart_Building.Count));
                }
                else
                {
                    if (item.Value.Dic_Building.Count > 0)
                    {
                        Building = Dic_SavePart_Building[item.Value.id];
                    }
                    else
                    {
                        Building = "";
                    }
                    go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_14"),
                        string.Format(LanguageMgr.GetInstance.GetText("BuildingMode_15"), newScene.Dic_MapPart.Count));
                }

                Grid = Dic_SavePart_Grid[item.Value.id];
                ////存储字段和内容到json里。已知BUG：多模块内容显示错误（只保留最后一个的数据）
                //JSONClass json = new JSONClass();
                //json["id"] = item.Value.id.ToString();
                //json["writeName"] = item.Value.writeName;
                //json["sizeX"] = item.Value.sizeX.ToString();
                //json["sizeY"] = item.Value.sizeY.ToString();
                //json["modelID"] = item.Value.modelID.ToString();
                //json["upScene"] = item.Value.upScene.ToString();
                //json["Dic_MapBox"] = Grid;
                //json["Dic_Building"] = Building;

                //使用JsonWriter写入字段和内容，已知缺点：中文乱码
                JsonWriter json = new JsonWriter();
                json.WriteObjectStart();
                json.WritePropertyName("id"); json.Write(item.Value.id);
                json.WritePropertyName("writeName"); json.Write(item.Value.writeName);
                json.WritePropertyName("sizeX"); json.Write(item.Value.sizeX);
                json.WritePropertyName("sizeY"); json.Write(item.Value.sizeY);
                json.WritePropertyName("modelID"); json.Write(item.Value.modelID);
                json.WritePropertyName("upScene"); json.Write(item.Value.upScene);
                json.WritePropertyName("Dic_MapBox"); json.Write(Grid);
                json.WritePropertyName("Dic_Building"); json.Write(Building);
                json.WriteObjectEnd();
                string all = Regex.Unescape(json.ToString());
                dic_AllPartData.Add(item.Value.id.ToString(), all);
            }
        }
        //保存场景主信息
        JsonWriter scene = new JsonWriter();
        scene.WriteObjectStart();
        scene.WritePropertyName("id"); scene.Write(newScene.id);
        scene.WritePropertyName("writeName"); scene.Write(newScene.writeName);
        scene.WritePropertyName("modelID"); scene.Write(newScene.modelID);
        scene.WriteObjectEnd();
        string se = Regex.Unescape(scene.ToString());
        Dic_AllSceneMain = new Dictionary<string, string>();
        if (Dic_AllSceneMain.ContainsKey(newScene.id.ToString()))
        {
            Dic_AllSceneMain[newScene.id.ToString()] = se;
        }
        else
        {
            Dic_AllSceneMain.Add(newScene.id.ToString(), se);
        }
        JsonReader.WriteJson(Data_Static.save_pathScene_Main + newScene.id + ".json", Dic_AllSceneMain);
        JsonReader.WriteJson(Data_Static.save_pathScene_Data + newScene.id + ".json", dic_AllPartData);
        JsonReader.WriteJson(Data_Static.save_pathScene_Grid + newScene.id + ".json", Dic_AllGrid);
        
        GameObject_SceneInfo.SetActive(false);
        //弹窗提示是否返回
        systemTips.gameObject.SetActive(true);
        GameObject[] gameObjects = new GameObject[2];
        gameObjects = systemTips.SetTipDesc(LanguageMgr.GetInstance.GetText("BuildingMode_38"), LanguageMgr.GetInstance.GetText("Tips_System_23"), LanguageMgr.GetInstance.GetText("Tips_System_24"));
        UIEventListener.Get(gameObjects[0]).onClick = systemYes;
        UIEventListener.Get(gameObjects[1]).onClick = systemNo;
    }

    void systemYes(GameObject btn)
    {
        systemTips.gameObject.SetActive(false);
        InitModel();
    }
    void systemNo(GameObject btn)
    {
        systemTips.gameObject.SetActive(false);
    }

    /// <summary>
    /// 选择普通模块
    /// </summary>
    /// <param name="btn"></param>
    void ChoosePart_Nomal(GameObject btn)
    {
        Button_ItemEX.SetActive(true);
        NGUITools.DestroyChildren(GameObject_Pos.transform);
        //在列表中刷新读取表格中的模板数据
        List<Map_PartModel> mpN = SelectDao.GetDao().SelectMap_PartModelByIsSpecial(0);

        foreach (var item in mpN)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_ItemEX.transform, GameObject_Pos.transform, Vector3.zero, Vector3.one);
            go.Find("Label_Name").GetComponent<UIText>().SetText(false, item.name);
            UITexture ut = go.Find("Texture_ItemPic").GetComponent<UITexture>();
            ut.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_PartModel + item.mapIcon);
            ut.width = 64;
            ut.height = 64;
            go.name = item.id.ToString();
            UIEventListener.Get(go.gameObject).onClick = SelectPart;
        }
        GameObject_Pos.GetComponent<UIGrid>().enabled = true;
        
        Button_ItemEX.SetActive(false);
    }
    /// <summary>
    /// 选择特殊模块
    /// </summary>
    /// <param name="btn"></param>
    void ChoosePart_Special(GameObject btn)
    {
        Button_ItemEX.SetActive(true);
        NGUITools.DestroyChildren(GameObject_Pos.transform);
        //在列表中刷新读取表格中的模板数据
        List<Map_PartModel> mpS = SelectDao.GetDao().SelectMap_PartModelByIsSpecial(1);

        foreach (var item in mpS)
        {
            Transform go = CommonFunc.GetInstance.UI_Instantiate(Button_ItemEX.transform, GameObject_Pos.transform, Vector3.zero, Vector3.one);
            go.Find("Label_Name").GetComponent<UIText>().SetText(false, item.name);
            UITexture ut = go.Find("Texture_ItemPic").GetComponent<UITexture>();
            ut.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_PartModel + item.mapIcon);
            ut.width = 64;
            ut.height = 64;
            go.name = item.id.ToString();
            UIEventListener.Get(go.gameObject).onClick = SelectPart;
        }
        GameObject_Pos.GetComponent<UIGrid>().enabled = true;

        Button_ItemEX.SetActive(false);
    }
    /// <summary>
    /// 全部清空
    /// </summary>
    /// <param name="btn"></param>
    void Clear(GameObject btn)
    {
        NGUITools.DestroyChildren(Part_Already.transform);
        GetSceneFile(Button_Yes);
    }
    /// <summary>
    /// 全部重置
    /// </summary>
    /// <param name="btn"></param>
    void Return(GameObject btn)
    {

    }

    void CloseInfo(GameObject btn)
    {
        GameObject_SceneInfo.SetActive(false);
    }

    private void ClickControl()
    {
        UIEventListener.Get(Button_New).onClick = RefreshSceneList;
        UIEventListener.Get(Button_Old).onClick = RefreshSceneList;
        UIEventListener.Get(Button_Nomal).onClick = ChoosePart_Nomal;
        UIEventListener.Get(Button_Special).onClick = ChoosePart_Special;
        UIEventListener.Get(Button_Clear).onClick = Clear;
        UIEventListener.Get(Button_Return).onClick = Return;
        UIEventListener.Get(Button_Save).onClick = SaveScene;

        UIEventListener.Get(Button_Closei).onClick = CloseInfo;

        UIEventListener.Get(Button_SavePart).onClick = SavePart;
        UIEventListener.Get(Button_PartInfo).onClick = PartInfo;
        UIEventListener.Get(Button_DeletePart).onClick = DeletePart;

    }

    public Camera UI_camera;
    public Camera designPatternCamera;
    /// <summary>
    /// 地图底层平面
    /// </summary>
    public GameObject PlaneGround;
    /// <summary>
    /// 地图底图
    /// </summary>
    public SpriteRenderer GroundPic;
    /// <summary>
    /// 选中的模块格子
    /// </summary>
    public GameObject SelectedGrid;
    /// <summary>
    /// 当前选中的矩形
    /// </summary>
    public GameObject SelectedBox;
    /// <summary>
    /// 已经完成的模块
    /// </summary>
    public GameObject Part_Already;
    /// <summary>
    /// 正在生成中的模块
    /// </summary>
    public GameObject Part_Temp;
    /// <summary>
    /// 场景中所有的格子归属[坐标,所属的模块ID]
    /// </summary>
    private Dictionary<string, int> Dic_AllGrid;
    /// <summary>
    /// 存储模块格子数据的字典。
    /// </summary>
    Dictionary<int, string> Dic_SavePart_Grid;
    /// <summary>
    /// 存储模块建筑数据的字典。
    /// </summary>
    Dictionary<int, string> Dic_SavePart_Building;
    /// <summary>
    /// 选中场景的模板ID
    /// </summary>
    public int ModelID;
    /// <summary>
    /// 选中场景的ID
    /// </summary>
    public Dictionary<int,MapScene> Dic_NowScene;

    //当前选中操作的类对象
    MapScene newScene;
    MapPart newPart;
    PartBuilding newPartBuilding;
    buildingFixedNPC newFixedNPC;
    MapBox newMapBox;
    /// <summary>
    /// 是否有正在被选中的模块
    /// </summary>
    public bool IsSelected;
    /// <summary>
    /// 选中的模块模板ID
    /// </summary>
    int selectedModelID;
    /// <summary>
    /// 当前创建的模块的格子数量的X_Y
    /// </summary>
    string GridCountXY;
    /// <summary>
    /// 保存模块的限制条件
    /// </summary>
    enum FixedSavePart : int
    {
        /// <summary>
        /// 可以保存
        /// </summary>
        ok = 0,
        /// <summary>
        /// 太小
        /// </summary>
        tooMin = 1,
        /// <summary>
        /// 太大
        /// </summary>
        tooMax = 2,
        /// <summary>
        /// 格子数量不匹配
        /// </summary>
        gridCountWrong = 3,
    }
    FixedSavePart fixedSavePart;
    /// <summary>
    /// 临时生成的格子列表
    /// </summary>
    //Dictionary<string,GameObject> tempGrids;
    /// <summary>
    /// 是否正在查看模块信息
    /// </summary>
    bool isHaveBox;
    /// <summary>
    /// 碰撞到的已生成模块
    /// </summary>
    GameObject PartGo;
    /// <summary>
    /// 已完成的模块列表
    /// </summary>
    Dictionary<int, GameObject> alreadyParts;

    private void CreateNewModel(GameObject btn)
    {
        GameObject_SceneInfo.SetActive(false);
        SceneInfoInitial(true);
    }
    private void GetSceneFile(GameObject btn)
    {
        GameObject_SceneInfo.SetActive(false);
        SceneInfoInitial(false);
    }

    /// <summary>
    /// 地图信息初始化
    /// </summary>
    private void SceneInfoInitial(bool isNew)
    {
        GameObject_Out.SetActive(false);
        GameObject_Part.SetActive(true);
        isOut = false;
        if (!isNew)
        {
            Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(ModelID);
            GameObject_Info.SetActive(true);
            //读取场景模块数据
            TextAsset ta_Part = Resources.Load(Data_Static.save_pathScene_Data + newScene.id + ".json") as TextAsset;
            newScene.Dic_MapPart = new Dictionary<int, MapPart>();
            JsonData jd_p = JsonMapper.ToObject(ta_Part.text);
            Dic_SavePart_Grid = new Dictionary<int, string>();
            Dic_SavePart_Grid = JsonMapper.ToObject<Dictionary<int, string>>(jd_p["id"].ToString());
            Dic_SavePart_Building = new Dictionary<int, string>();
            //生成模块
            alreadyParts = new Dictionary<int, GameObject>();
            foreach (var item in newScene.Dic_MapPart)
            {
                //将矩形放入已完成目录下，并且生成碰撞体
                GameObject already = Instantiate(SelectedBox);
                already.transform.parent = Part_Already.transform;
                //already.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Data_Static.MapPic_PartModel + SelectDao.GetDao().SelectMap_PartModel(newPart.modelID).mapIcon);
                already.name = newPart.id.ToString();
                already.layer = 9;
                alreadyParts.Add(newPart.id, already);
            }
            isHaveBox = false;
            PartGo = null;
            Button_Save.name = newScene.id.ToString();
            float mapSizeX = (float)ms.maxSize_X * GridSize;
            float mapSizeY = (float)ms.maxSize_Y * GridSize;
            PlaneGround.transform.localScale = new Vector3(mapSizeX, mapSizeY, 0);
            PlaneGround.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(ms.maxSize_X, ms.maxSize_Y));
            GroundPic.sprite = Resources.Load<Sprite>(Data_Static.MapPic_SceneModel + ms.ground);

            //地图边界控制
            MapControl_Move mm = FindObjectOfType<MapControl_Move>();
            mm.limit_Bottom = -mapSizeX / 2f;
            mm.limit_Top = mapSizeX / 2f;
            mm.limit_Left = -mapSizeY / 2f;
            mm.limit_Right = mapSizeY / 2f;
            mm.isOpenMouse = false;

            //添加所有格子信息进入字典
            Dic_AllGrid = new Dictionary<string, int>();
            Dic_AllGrid = JsonReader.ReadJson<int>(Data_Static.save_pathScene_Grid + newScene.id + ".json");

        }
        else
        {
            Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(ModelID);
            GameObject_Info.SetActive(true);
            Dic_SavePart_Grid = new Dictionary<int, string>();
            Dic_SavePart_Building = new Dictionary<int, string>();
            alreadyParts = new Dictionary<int, GameObject>();
            isHaveBox = false;
            PartGo = null;
            newScene = new MapScene();
            newScene.id = ModelID;
            newScene.writeName = ms.writeName;
            newScene.modelID = ModelID;
            newScene.Dic_MapPart = new Dictionary<int, MapPart>();
            Button_Save.name = newScene.id.ToString();

            float mapSizeX = (float)ms.maxSize_X * GridSize;
            float mapSizeY = (float)ms.maxSize_Y * GridSize;
            PlaneGround.transform.localScale = new Vector3(mapSizeX, mapSizeY, 0);
            PlaneGround.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(ms.maxSize_X, ms.maxSize_Y));
            GroundPic.sprite = Resources.Load<Sprite>(Data_Static.MapPic_SceneModel + ms.ground);

            //地图边界控制
            MapControl_Move mm = FindObjectOfType<MapControl_Move>();
            mm.limit_Bottom = -mapSizeX / 2f;
            mm.limit_Top = mapSizeX / 2f;
            mm.limit_Left = -mapSizeY / 2f;
            mm.limit_Right = mapSizeY / 2f;
            mm.isOpenMouse = false;

            //添加所有格子初始信息进入字典，中心点为所有格子的一半
            Dic_AllGrid = new Dictionary<string, int>();
            for (int x = 0; x < ms.maxSize_X; x++)
            {
                for (int y = 0; y < ms.maxSize_Y; y++)
                {
                    string PosName = posLabelChange(mm.limit_Left + (x * GridSize), mm.limit_Bottom + (y * GridSize));
                    int index = -1;
                    Dic_AllGrid.Add(PosName, index);
                }
            }
        }
        ChoosePart_Nomal(Button_Nomal);
    }
    /// <summary>
    /// 转换坐标文本
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    string posLabelChange(float x,float y)
    {
        string all_x = x.ToString("f2");
        if (all_x == "0.00")
        {
            all_x = "0";
        }
        string all_Y = y.ToString("f2");
        if (all_Y == "0.00")
        {
            all_Y = "0";
        }
        return all_x + "_" + all_Y;
    }

    /// <summary>
    /// 选中一个模块
    /// </summary>
    /// <param name="btn"></param>
    void SelectPart(GameObject btn)
    {
        IsSelected = true;
        selectedModelID = int.Parse(btn.name);
        Map_PartModel mp = SelectDao.GetDao().SelectMap_PartModel(selectedModelID);
        GridCountXY = "";
        fixedSavePart = FixedSavePart.tooMin;
        //创建一个模块
        newPart = new MapPart();
        //判断当前场景中模块ID的最大值，用最大值+1作为新模块的ID
        if (newScene.Dic_MapPart.Count > 0)
        {
            List<int> list_id = new List<int>();
            foreach (var item in newScene.Dic_MapPart)
            {
                list_id.Add(item.Key);
            }
            int maxID = CommonFunc.GetInstance.maxValue(list_id);
            newPart.id = maxID + 1;
        }
        else
        {
            newPart.id = 1;
        }
        newPart.writeName = mp.name;
        newPart.modelID = selectedModelID;
        newPart.upScene = newScene.id;
        newPart.Dic_Building = new Dictionary<int, PartBuilding>();
        newPart.Dic_MapBox = new Dictionary<string, MapBox>();

        GameObject_PartCreating.SetActive(true);
        Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_5"));

        //tempGrids = new Dictionary<string, GameObject>();
    }
    /// <summary>
    /// 鼠标控制（主方法）
    /// </summary>
    void MouseControl()
    {
        Map_PartModel mp = SelectDao.GetDao().SelectMap_PartModel(selectedModelID);
        //在选中了模块的情况下
        if (IsSelected && !isOut)
        {
            Button_PartInfo.SetActive(false);
            Button_SavePart.SetActive(true);
            GameObject_PartCreating.SetActive(true);
            GameObject_Part.SetActive(false);
            GameObject_List.SetActive(false);

            if (UICamera.Raycast(Input.mousePosition) || UICamera.isOverUI)
            {
                return;
            }
            else
            {
                //随时获得鼠标位置
                Vector3 pos = Input.mousePosition;
                pos = designPatternCamera.ScreenToWorldPoint(pos);

                //在鼠标位置上取得格子数据
                float boxPosX = CreateOneGrid(pos).x;
                float boxPosY = CreateOneGrid(pos).y;
                string posName = posLabelChange(boxPosX, boxPosY);
                //点击鼠标右键删除选中的部分区域
                if (Input.GetMouseButton(1))
                {
                    if (!AllowToPlace())
                    {
                        //如果点击到了已有的格子，删除当前模块中格子所在的行以及下方的所有行的数据，更新矩形。
                        if (PartGo != null && PartGo.layer == 8)
                        {
                            Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_8"));
                            Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(166 / 255, 40 / 255, 40 / 255, 255 / 255);

                            //计算所有格子里面最大的Y值
                            List<float> posYListA = new List<float>();
                            List<float> posXListA = new List<float>();
                            foreach (var item in newPart.Dic_MapBox)
                            {
                                float x = float.Parse(item.Key.Split('_')[0]);
                                float y = float.Parse(item.Key.Split('_')[1]);
                                posXListA.Add(x);
                                posYListA.Add(y);
                            }
                            float mixX = CommonFunc.GetInstance.minValue(posXListA);
                            float maxY = CommonFunc.GetInstance.maxValue(posYListA);
                            //判断所有已有的格子里坐标小于最大Y值的格子
                            List<string> posList = new List<string>();
                            foreach (var item in newPart.Dic_MapBox)
                            {
                                posList.Add(item.Key);
                            }
                            foreach (var item in posList)
                            {
                                float itemPosX = float.Parse(item.Split('_')[0]);
                                float itemPosY = float.Parse(item.Split('_')[1]);
                                if (itemPosY < boxPosY)
                                {
                                    Dic_AllGrid[item] = -1;
                                    newPart.Dic_MapBox.Remove(item);
                                    Dic_AllGrid[item] = -1;
                                    //Destroy(tempGrids[item]);
                                    //tempGrids.Remove(item);
                                }
                                //如果点击到最上面一行，则删除本行点击位置的右侧所有格子。
                                else if (itemPosY == maxY && itemPosX > boxPosX)
                                {
                                    Dic_AllGrid[item] = -1;
                                    newPart.Dic_MapBox.Remove(item);
                                    Dic_AllGrid[item] = -1;
                                    //Destroy(tempGrids[item]);
                                    //tempGrids.Remove(item);
                                }
                                //如果点击到左上角的格子，则弹出提示询问是否删除模块
                                else if (itemPosX == boxPosX && itemPosY == boxPosY && itemPosY == maxY && itemPosX == mixX)
                                {
                                    buttonTips.gameObject.SetActive(true);
                                    //buttonTips.gameObject.AddComponent<UIFollow>().target = SelectedBox;
                                    //buttonTips.gameObject.GetComponent<UIFollow>().isTure = true;
                                    ButtonText[] names = new ButtonText[2];
                                    ButtonText name0 = new ButtonText();
                                    name0.Id = 1;
                                    name0.Text = LanguageMgr.GetInstance.GetText("BuildingMode_34");
                                    ButtonText name1 = new ButtonText();
                                    name1.Id = 2;
                                    name1.Text = LanguageMgr.GetInstance.GetText("BuildingMode_35");
                                    names[0] = name0;
                                    names[1] = name1;
                                    List<GameObject> btns = buttonTips.SetAll(true, names);
                                    UIEventListener.Get(btns[0]).onClick = MouseDeletePart_Yes;
                                    UIEventListener.Get(btns[1]).onClick = MouseDeletePart_No;
                                }
                            }
                            //更新矩形
                            CreateBoxArea();
                        }
                    }
                    //如果没有选择模块，则返回查看模式
                    else
                    {
                        if (newPart.Dic_MapBox.Count == 0)
                        {
                            IsSelected = false;
                            GameObject_PartCreating.SetActive(false);
                            SelectedBox.SetActive(false);
                        }

                    }
                }
                SelectedGrid.SetActive(true);
                //点击后，选中的图片跟着鼠标生成在合适的位置
                SelectedGrid.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Data_Static.MapPic_PartModel + mp.mapIcon);
                SelectedGrid.transform.localPosition = CreateOneGrid(pos);

                //按住鼠标判断此地是否允许放置，如果允许，则放下模块
                if (Input.GetMouseButton(0))
                {
                    if (AllowToPlace())
                    {
                        Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_1"));
                        Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(0 / 255, 91 / 255, 8 / 255, 255 / 255);
                        if (!Dic_AllGrid.ContainsKey(posName))
                        {
                            Debug.LogError("坐标转换错误：" + posName);
                            return;
                        }
                        //刷新格子信息到模块数据中的字典里,
                        if (Dic_AllGrid[posName] == -1)
                        {
                            Dic_AllGrid[posName] = newPart.id;
                            //拒绝重复的格子，添加新格子信息进入新模块
                            if (!newPart.Dic_MapBox.ContainsKey(posName))
                            {
                                //给格子的属性赋值
                                newMapBox = new MapBox();
                                newMapBox.Pos = posName;
                                newMapBox.upPart = newPart.id;
                                newPart.Dic_MapBox.Add(posName, newMapBox);
                                //createGrid(boxPosX, boxPosY, Data_Static.MapPic_PartModel + mp.mapIcon, Part_Temp);
                                //根据鼠标点击到的位置更新矩形信息。
                                CreateBoxArea();
                            }
                        }
                        //如果不是 - 1，也不是当前模块的ID，表示已经是其他模块的格子了。弹窗提醒并删除模块
                        else if (Dic_AllGrid[posName] != newPart.id)
                        {
                            tipLabel = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).GetComponent<LableTipsUI>();
                            tipLabel.SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_30"), LanguageMgr.GetInstance.GetText("BuildingMode_4"));
                            Button_DeletePart.name = newPart.id.ToString();
                            DeletePart(Button_DeletePart);
                        }
                        else if (Dic_AllGrid[posName] == newPart.id)
                        {

                        }
                        else
                        {
                            Debug.LogError("判断条件逻辑错误。");
                        }
                    }
                    //如果点击后有其他格子
                    else
                    {

                    }
                }
            }       
        }
        //处理未创建模块时的操作，情形1：初始化
        else if (!isHaveBox && !isOut)
        {
            SelectedGrid.SetActive(false);
            SelectedBox.SetActive(false);
            GameObject_Part.SetActive(true);
            GameObject_List.SetActive(true);
            Button_PartInfo.SetActive(true);
            GameObject_PartCreating.SetActive(false);
            Button_SavePart.SetActive(false);
            //点击鼠标右键处理
            if (Input.GetMouseButton(1))
            {

            }
            //鼠标左键点击遇到碰撞体时，判断模块信息
            if (Input.GetMouseButtonDown(0))
            {
                if (!AllowToPlace())
                {
                    RefreshPartInfo_Outside();
                }
            }
        }
        //情形2：正在查看模块信息
        else if (isHaveBox && !isOut)
        {
            //鼠标左键点击没有遇到碰撞体时，返回初始化
            if (Input.GetMouseButtonDown(0))
            {
                if (AllowToPlace())
                {
                    isHaveBox = false;
                }
                else
                {
                    RefreshPartInfo_Outside();
                }
            }
       }
    }

    void MouseDeletePart_Yes(GameObject btn)
    {
        Button_DeletePart.name = newPart.id.ToString();
        DeletePart(Button_DeletePart);
        buttonTips.gameObject.SetActive(false);
    }
    void MouseDeletePart_No(GameObject btn)
    {
        buttonTips.gameObject.SetActive(false);
    }

    /// <summary>
    /// 刷新外面显示的模块信息
    /// </summary>
    void RefreshPartInfo_Outside()
    {
        GameObject_PartCreating.SetActive(true);
        Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_18"));
        Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(0 / 255, 0 / 255, 0 / 255, 255 / 255);
        int ID;
        if (!int.TryParse(PartGo.name,out ID))
        {
            return;
        }
        MapPart part = newScene.Dic_MapPart[ID];
        Button_DeletePart.name = PartGo.name;
        Button_PartInfo.name = PartGo.name;
        Label_CreatingName.SetText(false, part.writeName);
        Label_PartAreaSizeX.SetText(false, part.sizeX.ToString());
        Label_PartAreaSizeY.SetText(false, part.sizeY.ToString());
        GameObject_PartCreating.SetActive(true);
        //如果碰到的为已生成的模块，读取信息
        if (PartGo != null)
        {
            isHaveBox = true;
        }
    }

    /// <summary>
    /// 更新矩形区域范围
    /// </summary>
    void CreateBoxArea()
    {
        Map_PartModel mp = SelectDao.GetDao().SelectMap_PartModel(selectedModelID);
        //计算字典中所有格子坐标的左下（XY最小）与右上（XY最大），判断出矩形大小与内部包括的格子数量。
        List<float> posXList = new List<float>();
        List<float> posYList = new List<float>();
        foreach (var item in newPart.Dic_MapBox)
        {
            float x = float.Parse(item.Key.Split('_')[0]);
            float y = float.Parse(item.Key.Split('_')[1]);
            posXList.Add(x);
            posYList.Add(y);
        }
        Vector3 LeftBottomPos = new Vector3(CommonFunc.GetInstance.minValue(posXList), CommonFunc.GetInstance.minValue(posYList), 0);
        Vector3 RightTopPos = new Vector3(CommonFunc.GetInstance.maxValue(posXList), CommonFunc.GetInstance.maxValue(posYList), 0);
        //根据矩形的左下角坐标和大小创建矩形
        float distanceX = (RightTopPos.x - LeftBottomPos.x) / GridSize + 1;
        float distanceY = (RightTopPos.y - LeftBottomPos.y) / GridSize + 1;
        int countX = (int)Mathf.Abs(distanceX);
        int countY = (int)Mathf.Abs(distanceY);

        //根据2个坐标创建一个符合范围的矩形
        SelectedBox.SetActive(true);
        SpriteRenderer srB = SelectedBox.GetComponent<SpriteRenderer>();
        srB.sprite = Resources.Load<Sprite>(Data_Static.MapPic_PartModel + mp.mapIcon);
        srB.drawMode = SpriteDrawMode.Tiled;
        srB.size = new Vector2(countX * GridSize, countY * GridSize);
        SelectedBox.GetComponent<BoxCollider2D>().size = new Vector2(countX * GridSize, countY * GridSize);
        //SelectedBox.transform.localScale = new Vector3(distanceX, distanceY, 1);
        SelectedBox.transform.localPosition = LeftBottomPos + new Vector3(((distanceX - 1) * GridSize) / 2, ((distanceY - 1) * GridSize) / 2, 0);
        GridCountXY = countX + "_" + countY;
        //重置碰撞体


        //根据不同格子的位置获得所有内部格子的信息
        for (int x = 0; x < countX; x++)
        {
            for (int y = 0; y < countY; y++)
            {
                Vector3 gridPos = LeftBottomPos + new Vector3(x * GridSize, GridSize * y, 0);
                float gridPosX = gridPos.x;
                float gridPosY = gridPos.y;
                string gridName = posLabelChange(gridPosX, gridPosY);
                //添加所有格子进入新模块，排除重复
                if (Dic_AllGrid.ContainsKey(gridName))
                {
                    if (!newPart.Dic_MapBox.ContainsKey(gridName))
                    {                        
                        //刷新格子信息到模块数据中的字典里,如果不是-1或者当前模块的ID，表示已经是其他模块的格子了。
                        if (Dic_AllGrid[gridName] == -1)
                        {
                            //给格子的属性赋值
                            newMapBox = new MapBox();
                            newMapBox.Pos = gridName;
                            newMapBox.upPart = newPart.id;
                            Dic_AllGrid[gridName] = newPart.id;
                            newPart.Dic_MapBox.Add(gridName, newMapBox);
                            //if (!tempGrids.ContainsKey(gridName))
                            //{
                            //    createGrid(gridPosX, gridPosY, Data_Static.MapPic_PartModel + mp.mapIcon, Part_Temp);
                            //}
                        }
                        //判断选中区域内有其他模块的格子为障碍物，则弹窗提示，删除模块，回到待创建状态
                        else
                        {
                            if (Dic_AllGrid[gridName] != newPart.id)
                            {
                                if (tipLabel == null)
                                {
                                    tipLabel = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject.GetComponent<LableTipsUI>();
                                    tipLabel.gameObject.SetActive(true);
                                    tipLabel.SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_30"), LanguageMgr.GetInstance.GetText("BuildingMode_4"));
                                }

                                Button_DeletePart.name = newPart.id.ToString();
                                DeletePart(Button_DeletePart);
                                Debug.Log("创建格子的时候发现障碍物，坐标为："+ gridName);
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError("找不到此格子的坐标信息：" + gridName);
                }
            }
        }
       
        //给状态赋值
        newPart.sizeX = countX;
        newPart.sizeY = countY;
        Label_CreatingName.SetText(false, mp.name);
        Label_PartAreaSizeX.SetText(false, countX.ToString());
        Label_PartAreaSizeY.SetText(false, countY.ToString());

        int minGridNum = int.Parse(SelectDao.GetDao().SelectSystem_Config(2).values);
        int maxGridNum = int.Parse(SelectDao.GetDao().SelectSystem_Config(1).values);

        //判断最小格子数量限制
        if (newPart.Dic_MapBox.Count < minGridNum)
        {
            fixedSavePart = FixedSavePart.tooMin;
            Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_3") + minGridNum);
            Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(166 / 255, 40 / 255, 40 / 255, 255 / 255);
            srB.sprite = Resources.Load<Sprite>(Data_Static.MapPic_Nomal + "Grid_Wrong");
            srB.drawMode = SpriteDrawMode.Tiled;
            srB.size = new Vector2(countX * GridSize, countY * GridSize);
            SelectedBox.GetComponent<BoxCollider2D>().size = new Vector2(countX * GridSize, countY * GridSize);
        }
        //判断最大格子数量限制
        else if (newPart.Dic_MapBox.Count > maxGridNum)
        {
            fixedSavePart = FixedSavePart.tooMax;
            Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_2") + maxGridNum);
            Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(166 / 255, 40 / 255, 40 / 255, 255 / 255);
            srB.sprite = Resources.Load<Sprite>(Data_Static.MapPic_Nomal + "Grid_Wrong");
            srB.drawMode = SpriteDrawMode.Tiled;
            srB.size = new Vector2(countX * GridSize, countY * GridSize);
            SelectedBox.GetComponent<BoxCollider2D>().size = new Vector2(countX * GridSize, countY * GridSize);
        }
        //判断格子数量是否匹配
        else if (newPart.Dic_MapBox.Count != countX * countY)
        {
            int countSX = int.Parse(GridCountXY.Split('_')[0]);
            int countSY = int.Parse(GridCountXY.Split('_')[1]);
            if (newPart.Dic_MapBox.Count != countSX * countSY)
            {
                fixedSavePart = FixedSavePart.gridCountWrong;
            }
        }
        else
        {
            fixedSavePart = FixedSavePart.ok;
        }
    }

    /// <summary>
    /// 判断鼠标坐标点匹配世界坐标点的格子坐标，生成对应的格子
    /// </summary>
    /// <returns></returns>
    private Vector3 CreateOneGrid(Vector3 Pos)
    {
        int x = Mathf.RoundToInt(Pos.x / GridSize);
        int y = Mathf.RoundToInt(Pos.y / GridSize);
        Vector3 aligned = new Vector3(x * GridSize, y * GridSize);
        return aligned;
    }

    /// <summary>
    /// 判断该位置是否允许放置
    /// </summary>
    /// <returns></returns>
    private bool AllowToPlace()
    {
        //创建一个穿透所有碰撞体的射线
        Collider2D[] col = Physics2D.OverlapPointAll(designPatternCamera.ScreenToWorldPoint(Input.mousePosition));
        if (col.Length > 0)
        {
            foreach (var item in col)
            {
                ////如果射线穿到的碰撞体的layer为“8：Ground”
                //if (item.gameObject.layer == 8)
                //{
                //    //GridGo = item.gameObject;
                //}
                ////如果射线穿到的碰撞体的layer为“9：World”
                //else if (item.gameObject.layer == 9)
                //{
                //    PartGo = item.gameObject;
                //}
                PartGo = item.gameObject;
            }
            return false;
        }
        else
        {
            //GridGo = null;
            PartGo = null;
            return true;
        }

    }

    /// <summary>
    /// 创建一个格子
    /// </summary>
    /// <param name="gridPosX">格子坐标X</param>
    /// <param name="gridPosY">格子坐标Y</param>
    /// <param name="PicPath">格子图片路径</param>
    /// <param name="parentGo">格子父物体</param>
    void createGrid(float gridPosX, float gridPosY, string PicPath ,GameObject parentGo)
    {
        GameObject grid = parentGo.AddChild(8);
        grid.name = posLabelChange(gridPosX, gridPosY);
        SpriteRenderer gsr = grid.AddComponent<SpriteRenderer>();
        gsr.sortingLayerName = "Grid";
        gsr.sortingOrder = 2;
        gsr.sprite = Resources.Load<Sprite>(PicPath);
        BoxCollider2D gbc = grid.AddComponent<BoxCollider2D>();
        gbc.size = new Vector2(GridSize, GridSize);
        grid.transform.localScale = new Vector3(1, 1, 1);
        grid.transform.localPosition = new Vector3(gridPosX, gridPosY, 0);
        if (parentGo == Part_Temp)
        {
            //tempGrids.Add(grid.name, grid);
        }

    }

    /// <summary>
    /// 保存模块
    /// </summary>
    /// <param name="btn"></param>
    void SavePart(GameObject btn)
    {
        string dic_Grid = "";
        int minGridNum = int.Parse(SelectDao.GetDao().SelectSystem_Config(2).values);
        int maxGridNum = int.Parse(SelectDao.GetDao().SelectSystem_Config(1).values);
        tipLabel = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).GetComponent<LableTipsUI>();
        if (newPart.Dic_MapBox.Count>0)
        {
            //判断保存模块的限制条件
            if (fixedSavePart == FixedSavePart.ok)
            {
                tipLabel.SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_11"), string.Format(LanguageMgr.GetInstance.GetText("BuildingMode_12"),
                         newPart.writeName, newPart.id, newPart.Dic_MapBox.Count));
                foreach (var item in newPart.Dic_MapBox)
                {
                    dic_Grid += JsonReader.ChangeStringToRegex(item.Value);
                }
                //在场景类中写入模块信息。
                newScene.Dic_MapPart.Add(newPart.id, newPart);
                Dic_SavePart_Grid.Add(newPart.id, dic_Grid);

                //将矩形放入已完成目录下，并且生成碰撞体
                GameObject already = Instantiate(SelectedBox);
                already.transform.parent = Part_Already.transform;
                //already.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Data_Static.MapPic_PartModel + SelectDao.GetDao().SelectMap_PartModel(newPart.modelID).mapIcon);
                already.name = newPart.id.ToString();
                already.layer = 9;
                alreadyParts.Add(newPart.id, already);

                IsSelected = false;
                GameObject_PartCreating.SetActive(false);
                //NGUITools.DestroyChildren(Part_Temp.transform);
            }
            else if (fixedSavePart == FixedSavePart.tooMin)
            {
                tipLabel.SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_11"), LanguageMgr.GetInstance.GetText("BuildingMode_31")+ minGridNum);
            }
            else if(fixedSavePart == FixedSavePart.tooMax)
            {
                tipLabel.SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_11"), LanguageMgr.GetInstance.GetText("BuildingMode_32")+ maxGridNum);
            }
            else if (fixedSavePart == FixedSavePart.gridCountWrong)
            {
                tipLabel.SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_11"), LanguageMgr.GetInstance.GetText("BuildingMode_33")+ newPart.Dic_MapBox.Count+" : "+ GridCountXY);
                Button_DeletePart.name = newPart.id.ToString();
                DeletePart(Button_DeletePart);
            }
            else
            {
                Debug.LogError("保存模块的限制条件为空。");
            }

        }
        else
        {
            tipLabel.SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_11"), LanguageMgr.GetInstance.GetText("BuildingMode_13"));
        }
    }

    /// <summary>
    /// 删除模块
    /// </summary>
    /// <param name="btn"></param>
    void DeletePart(GameObject btn)
    {
        //GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        foreach (var item in newPart.Dic_MapBox)
        {
            Dic_AllGrid[item.Key] = -1;
        }
        newPart.Dic_MapBox.Clear();
        //删除当前的相关数据
        if (IsSelected)
        {
            //go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_9"), string.Format(LanguageMgr.GetInstance.GetText("BuildingMode_10"),
            //    newPart.writeName, newPart.id, newPart.Dic_MapBox.Count));
            //NGUITools.DestroyChildren(Part_Temp.transform);
        }
        //删除保存后的相关数据
        else
        {
            int partID = int.Parse(btn.name);
            newScene.Dic_MapPart.Remove(partID);
            Dic_SavePart_Grid.Remove(partID);
            Dic_SavePart_Building.Remove(partID);
            if (alreadyParts.ContainsKey(partID))
            {
                Destroy(alreadyParts[partID]);
                alreadyParts.Remove(partID);
            }
            //go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_9"), string.Format(LanguageMgr.GetInstance.GetText("BuildingMode_10"),
            //      newPart.writeName, newPart.id, newPart.Dic_MapBox.Count));
        }

        IsSelected = false;
        GameObject_PartCreating.SetActive(false);
        SelectedBox.SetActive(false);
    }

    //模块信息界面
    public GameObject GameObject_GroundBox;
    public GameObject Button_SaveNowPart;
    public GameObject Button_CloseNowPart;
    public UIInput Input_NameTitle;
    public UITexture Texture_PartSign;
    public GameObject Button_Force;
    public UISprite Sprite_ForceIcon;
    public UIText Label_ForceName;
    public GameObject Button_FangYu;
    public GameObject Button_ZhiAn;
    public GameObject Button_WeiSheng;
    public GameObject Button_FanRong;
    public GameObject Button_Road;
    public UISprite Sprite_RoadIcon;
    public UIText Label_RoadName;
    public UIText Label_PartSizeX;
    public UIText Label_PartSizeY;
    public UIText Label_CountNow;
    public UIText Label_CountMax;
    public GameObject Button_BuildingEX;
    public GameObject GameObject_BuildingPos;
    public GameObject Button_AddBuilding;
    public GameObject Sprite_Add;

    public GameObject GameObject_BuildingList;
    public UIText Label_Title;
    public GameObject Button_ArrowLeft;
    public GameObject Button_ArrowRight;
    public GameObject Button_BuildingAddEX;
    public GameObject GameObject_BuildingAddPos;

    Dictionary<int, PartBuilding> tempPartBuilding;
    int choosePartID = 0;
    /// <summary>
    /// 已填好的名字
    /// </summary>
    string partName_Now;
    /// <summary>
    /// 暂时保存的建筑分类编号
    /// </summary>
    int buildingType = 0;
    /// <summary>
    /// 建筑当前所占大小
    /// </summary>
    int buildingSize;
    /// <summary>
    /// 打开模块信息界面
    /// </summary>
    /// <param name="btn"></param>
    void PartInfo(GameObject btn)
    {
        //初始化信息
        int id = int.Parse(btn.name);
        choosePartID = id;
        partName_Now = "";
        NGUITools.DestroyChildren(GameObject_BuildingPos.transform);
        tempPartBuilding = new Dictionary<int, PartBuilding>();
        if (newScene.Dic_MapPart[choosePartID].Dic_Building.Count > 0)
        {
            foreach (var item in newScene.Dic_MapPart[choosePartID].Dic_Building)
            {
                tempPartBuilding.Add(item.Key, item.Value);
            }
        }
        Button_BuildingEX.SetActive(false);
        GameObject_GroundBox.SetActive(true);
        GameObject_BuildingList.SetActive(false);
        UIEventListener.Get(Button_AddBuilding).onClick = OpenBuildingList;
        UIEventListener.Get(Button_SaveNowPart).onClick = SaveGroundBox;
        UIEventListener.Get(Button_CloseNowPart).onClick = CloseGroundBox;
        Button_SaveNowPart.name = btn.name;

        Map_PartModel mp = SelectDao.GetDao().SelectMap_PartModel(newScene.Dic_MapPart[id].modelID);
        //输入名称初始化
        Input_NameTitle.value = newScene.Dic_MapPart[id].writeName;
        Input_NameTitle.characterLimit = 12;
        Input_NameTitle.validation = UIInput.Validation.Filename;
        EventDelegate.Add(Input_NameTitle.onChange, Rename_Info);
        Rename_Info();
        Texture_PartSign.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_PartModel + mp.mapIcon);
        //势力信息

        //模块属性
        Button_FangYu.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, mp.fangyu.ToString());
        Button_ZhiAn.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, mp.zhian.ToString());
        Button_WeiSheng.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, mp.weisheng.ToString());
        Button_FanRong.transform.Find("Label_Name").GetComponent<UIText>().SetText(false, mp.fanrong.ToString());
        //道路信息
        Map_Terrain mt = SelectDao.GetDao().SelectMap_Terrain(mp.roadType);
        Sprite_RoadIcon.spriteName = mt.icon;
        Label_RoadName.SetText(false, mt.name);

        //模块尺寸
        Label_PartSizeX.SetText(false, newScene.Dic_MapPart[id].sizeX.ToString());
        Label_PartSizeY.SetText(false, newScene.Dic_MapPart[id].sizeY.ToString());

        buildingSize = 0;
        foreach (var item in newScene.Dic_MapPart[id].Dic_Building)
        {
            buildingSize += SelectDao.GetDao().SelectBuilding_Model(item.Value.modelID).size;
        }

        Label_CountNow.SetText(false, buildingSize.ToString());
        Label_CountMax.SetText(false, (newScene.Dic_MapPart[id].sizeX * newScene.Dic_MapPart[id].sizeY).ToString());

        if (newScene.Dic_MapPart[id].Dic_Building.Count > 0)
        {
            RefreshBuildingList();
        }
        else
        {
            Button_BuildingEX.transform.Find("Label_BuildingName").GetComponent<UIText>().SetText(true, "BuildingMode_22");
            Button_BuildingEX.transform.localPosition = Vector3.zero;
            UIEventListener.Get(Button_BuildingEX).onClick = OpenBuildingList;
        }

    }

    /// <summary>
    /// 重命名判断：建筑名称
    /// </summary>
    void Rename_Info()
    {
        partName_Now = "";
        if (Input_NameTitle.value == "" || Input_NameTitle.value.Contains(" ") || Input_NameTitle.value == null)
        {
            tipLabel = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject.GetComponent<LableTipsUI>();
            tipLabel.gameObject.SetActive(true);
            tipLabel.SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_21"), LanguageMgr.GetInstance.GetText("BuildingMode_20"));
            Input_NameTitle.value = LanguageMgr.GetInstance.GetText("BuildingMode_19");
        }
        else
        {
            partName_Now = Input_NameTitle.value;
        }
    }
    /// <summary>
    /// 保存模块详情
    /// </summary>
    /// <param name="btn"></param>
    void SaveGroundBox(GameObject btn)
    {
        int id = int.Parse(btn.name);
        if (partName_Now != "")
        {
            newScene.Dic_MapPart[id].writeName = partName_Now;
        }
        newScene.Dic_MapPart[id].Dic_Building = tempPartBuilding;
        string building = "";
        foreach (var item in tempPartBuilding)
        {
            building += JsonReader.ChangeStringToRegex(item.Value);
        }

        Dic_SavePart_Building.Remove(id);
        Dic_SavePart_Building.Add(id, building);
        GameObject_GroundBox.SetActive(false);

    }
    /// <summary>
    /// 关闭模块详情界面
    /// </summary>
    /// <param name="btn"></param>
    void CloseGroundBox(GameObject btn)
    {
        GameObject_GroundBox.SetActive(false);
    }


    /// <summary>
    /// 打开全部建筑列表
    /// </summary>
    /// <param name="btn"></param>
    void OpenBuildingList(GameObject btn)
    {
        GameObject_BuildingList.SetActive(!GameObject_BuildingList.activeSelf);
        buildingType = 0;
        if (GameObject_BuildingList.activeSelf)
        {
            Label_Title.SetText(true, "BuildingType_" + buildingType);
            RefreshAllBuilding(buildingType);
            UIEventListener.Get(Button_ArrowLeft).onClick = ChangeType_Left;
            UIEventListener.Get(Button_ArrowRight).onClick = ChangeType_Right;

            Vector3 rotationVector3 = new Vector3(0f, 0f, 90f);
            Quaternion rotation = Quaternion.Euler(rotationVector3);
            Button_AddBuilding.transform.Find("Sprite_Add").localRotation = rotation;
        }
        else
        {
            Vector3 rotationVector3 = new Vector3(0f, 0f, -90f);
            Quaternion rotation = Quaternion.Euler(rotationVector3);
            Button_AddBuilding.transform.Find("Sprite_Add").localRotation = rotation;
        }
    }
    /// <summary>
    /// 建筑Type切换：向左
    /// </summary>
    /// <param name="btn"></param>
    void ChangeType_Left(GameObject btn)
    {
        if (buildingType == 0)
        {
            buildingType = 8;
            RefreshAllBuilding(buildingType);
        }
        else
        {
            buildingType--;
            RefreshAllBuilding(buildingType);
        }
    }
    /// <summary>
    /// 建筑Type切换：向右
    /// </summary>
    /// <param name="btn"></param>
    void ChangeType_Right(GameObject btn)
    {
        if (buildingType == 8)
        {
            buildingType = 0;
            RefreshAllBuilding(buildingType);
        }
        else
        {
            buildingType++;
            RefreshAllBuilding(buildingType);
        }
    }
    /// <summary>
    /// 刷新外建筑列表
    /// </summary>
    void RefreshAllBuilding(int type)
    {
        NGUITools.DestroyChildren(GameObject_BuildingAddPos.transform);
        Label_Title.SetText(true, "BuildingType_" + buildingType);
        Button_BuildingAddEX.SetActive(true);
        int count = 0;
        foreach (var item in SelectDao.GetDao().SelectBuildingModelByType(type))
        {
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Button_BuildingAddEX.transform, GameObject_BuildingAddPos.transform, 
                new Vector3(0, -count * 55), Vector3.one).gameObject;
            go.transform.Find("Sprite_BuildingIcon").GetComponent<UISprite>().spriteName = item.mapIcon;
            go.transform.Find("Label_BuildingName").GetComponent<UIText>().SetText(false, item.name);
            go.transform.Find("Label_BuildingState").GetComponent<UIText>().SetText(true, "BuildingMode_23");
            go.transform.Find("Label_BuildingCost").GetComponent<UIText>().SetText(false, item.size.ToString());
            go.name = item.id.ToString();
            UIEventListener.Get(go).onClick = AddBuilding;
            count++;
        }
        Button_BuildingAddEX.SetActive(false);
    }
    /// <summary>
    /// 添加建筑到模块中
    /// </summary>
    /// <param name="btn"></param>
    void AddBuilding(GameObject btn)
    {
        int id = int.Parse(btn.name);
        Building_Model bm = SelectDao.GetDao().SelectBuilding_Model(id);
        PartBuilding pb = new PartBuilding();
        if (choosePartID != 0)
        {
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
            if (buildingSize + bm.size > (newScene.Dic_MapPart[choosePartID].sizeX * newScene.Dic_MapPart[choosePartID].sizeY))
            {
                go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_26"), LanguageMgr.GetInstance.GetText("BuildingMode_28"));
            }
            else
            {
                pb.id = tempPartBuilding.Count + 1;
                pb.modelID = id;
                pb.writeName = bm.name;
                pb.upPart = choosePartID;

                buildingSize += bm.size;
                tempPartBuilding.Add(pb.id, pb);
                RefreshBuildingList();
                go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_26"), LanguageMgr.GetInstance.GetText("BuildingMode_27"));
            }

        }
        else
        {
            Debug.LogError("错误：选中模块ID为0.");
        }
    }

    /// <summary>
    /// 刷新内建筑列表
    /// </summary>
    void RefreshBuildingList()
    {
        NGUITools.DestroyChildren(GameObject_BuildingPos.transform);
        Button_BuildingEX.SetActive(true);

        int count = 0;
        foreach (var item in tempPartBuilding)
        {
            Building_Model bm = SelectDao.GetDao().SelectBuilding_Model(item.Value.modelID);
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Button_BuildingEX.transform, GameObject_BuildingPos.transform,
                new Vector3(82f * (count % 5), -82f * Mathf.FloorToInt(count / 5), 0), Vector3.one).gameObject;
            go.transform.Find("Sprite_BuildingIcon").GetComponent<UISprite>().spriteName = bm.mapIcon;
            go.transform.Find("Label_BuildingName").GetComponent<UIText>().SetText(false, bm.name);
            go.name = item.Value.id.ToString();
            UIEventListener.Get(go).onClick = ChooseBuilding_Have;
            count++;
        }
        Button_BuildingEX.SetActive(false);

        Label_CountNow.SetText(false, buildingSize.ToString());
    }
    /// <summary>
    /// 选中已有的建筑，删除之
    /// </summary>
    /// <param name="btn"></param>
    void ChooseBuilding_Have(GameObject btn)
    {
        int id = int.Parse(btn.name);
        Building_Model bm = SelectDao.GetDao().SelectBuilding_Model(id);
        if (tempPartBuilding.ContainsKey(id))
        {
            buildingSize -= bm.size;
            tempPartBuilding.Remove(id);
            RefreshBuildingList();
            GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
            go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_29"), LanguageMgr.GetInstance.GetText("BuildingMode_25"));
        }
        else
        {
            Debug.LogError("错误：不包含此建筑");
        }


    }

}
/// <summary>
/// 地图格子（用于寻路）
/// </summary>
public class MapBox
{
    /// <summary>
    /// 格子坐标
    /// </summary>
    public string Pos { get; set; }
    /// <summary>
    /// 格子所属的模块ID
    /// </summary>
    public int upPart { get; set; }

    public MapBox(int _PosX, int _PosY, int _upPart)
    {
        Pos = (_PosX + "_" + _PosY).ToString();
        upPart = _upPart;
    }
    public MapBox()
    {

    }
}

/// <summary>
/// 建筑中的固定NPC
/// </summary>
public class buildingFixedNPC
{
    /// <summary>
    /// NPC的ID
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// NPC角色模板ID
    /// </summary>
    public int modelID { get; set; }
    /// <summary>
    /// NPC所属的建筑ID
    /// </summary>
    public int upBuilding { get; set; }
    public buildingFixedNPC(int _id, int _modelID,int _upBuilding)
    {
        id = _id;
        modelID = _modelID;
        upBuilding = _upBuilding;
    }
    public buildingFixedNPC()
    {

    }
}
/// <summary>
/// 模块中的建筑
/// </summary>
public class PartBuilding
{
    /// <summary>
    /// 建筑ID
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 建筑可修改名称
    /// </summary>
    public string writeName { get; set; }
    /// <summary>
    /// 建筑模板ID
    /// </summary>
    public int modelID { get; set; }
    /// <summary>
    /// 建筑所属的模块ID
    /// </summary>
    public int upPart { get; set; }
    /// <summary>
    /// 建筑中的固定NPC
    /// </summary>
    public Dictionary<int, buildingFixedNPC> Dic_FixedNPC { get; set; }
    public PartBuilding(int _id, string _writeName, int _modelID, int _upPart, Dictionary<int, buildingFixedNPC> _Dic_FixedNPC)
    {
        writeName = _writeName;
        id = _id;
        modelID = _modelID;
        upPart = _upPart;
        Dic_FixedNPC = _Dic_FixedNPC;
    }
    public PartBuilding()
    {

    }
}

/// <summary>
/// 地图中的模块（存储信息）
/// </summary>
public class MapPart
{
    /// <summary>
    /// 模块（区域）ID
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 模块（区域）可修改名称
    /// </summary>
    public string writeName { get; set; }
    /// <summary>
    /// 模块X大小
    /// </summary>
    public int sizeX { get; set; }
    /// <summary>
    /// 模块Y大小
    /// </summary>
    public int sizeY { get; set; }
    /// <summary>
    /// 模块模板ID
    /// </summary>
    public int modelID { get; set; }
    /// <summary>
    /// 建筑所属的场景ID
    /// </summary>
    public int upScene { get; set; }
    /// <summary>
    /// 所包含的格子列表
    /// </summary>
    public Dictionary<string, MapBox> Dic_MapBox { get; set; }
    /// <summary>
    /// 所包含的建筑列表
    /// </summary>
    public Dictionary<int, PartBuilding> Dic_Building { get; set; }
    public MapPart(int _id, string _writeName, int _sizeX, int _sizeY, int _modelID, int _upScene, Dictionary<string, MapBox> _Dic_MapBox, Dictionary<int, PartBuilding> _Dic_Building)
    {
        id = _id;
        writeName = _writeName;
        sizeX = _sizeX;
        sizeY = _sizeY;
        modelID = _modelID;
        upScene = _upScene;
        Dic_MapBox = _Dic_MapBox;
        Dic_Building = _Dic_Building;
    }
    public MapPart()
    {

    }
}
/// <summary>
/// 地图中的场景
/// </summary>
public class MapScene
{
    /// <summary>
    /// 场景（城市/野外）ID
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 场景（城市/野外）可修改名称
    /// </summary>
    public string writeName { get; set; }
    /// <summary>
    /// 场景模板ID
    /// </summary>
    public int modelID { get; set; }
    /// <summary>
    /// 所包含的模块列表
    /// </summary>
    public Dictionary<int, MapPart> Dic_MapPart { get; set; }

    public MapScene(int _id, string _writeName, int _modelID, Dictionary<int, MapPart> _Dic_MapPart)
    {
        id = _id;
        writeName = _writeName;
        modelID = _modelID;
        Dic_MapPart = _Dic_MapPart;
    }

    public MapScene()
    {

    }
}

