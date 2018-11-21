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

    public GameObject GameObject_SceneModel;
    public GameObject Button_Close;
    public UIInput Input_Name;
    public UIInput Input_Num;
    public GameObject Sprite_SignRight;
    public GameObject Sprite_SignWrong;
    public UIInput Input_SizeX;
    public GameObject Button_ReduceX;
    public GameObject Button_AddX;
    public UIInput Input_SizeY;
    public GameObject Button_ReduceY;
    public GameObject Button_AddY;
    public GameObject Button_OK;

    public GameObject GameObject_SceneInfo;
    public GameObject Button_Closei;
    public UIText Label_Name;
    public GameObject Texture_ItemPici;
    public UIText Label_Model;
    public UIText Label_SizeX;
    public UIText Label_SizeY;
    public GameObject Button_Yes;

    public GameObject GameObject_GroundBox;
    public UIText Label_partName;
    public GameObject Texture_PartSign;
    public GameObject Button_Force;
    public UISprite Sprite_ForceIcon;
    public UIText Label_ForceName;
    public GameObject Button_FangYu;
    public GameObject Button_ZhiAn;
    public GameObject Button_WeiSheng;
    public GameObject Button_FanRong;
    public GameObject Button_Road;
    public UISprite Sprite_BuildingIcon;
    public UIText Label_BuildingName;
    public UIText Label_PartSizeX;
    public UIText Label_PartSizeY;
    public UIText Label_CountNow;
    public UIText Label_CountMax;
    public GameObject Button_Reduce_FangYu;
    public GameObject Button_Add_FangYu;
    public GameObject Button_Reduce_ZhiAn;
    public GameObject Button_Add_ZhiAn;
    public GameObject Button_Reduce_WeiSheng;
    public GameObject Button_Add_WeiSheng;
    public GameObject Button_Reduce_FanRong;
    public GameObject Button_Add_FanRong;
    public GameObject Button_BuildingEX;
    public GameObject GameObject_BuildingPos;
    public GameObject Button_AddBuilding;
    public GameObject Sprite_Add;

    public GameObject GameObject_BuildingList;
    public GameObject Button_CloseB;
    public UIText Label_Title;
    public GameObject Button_ArrowLeft;
    public GameObject Button_ArrowRight;
    public GameObject Button_BuildingAddEX;
    public GameObject GameObject_BuildingAddPos;

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
    private Dictionary<int, Map_SceneModel> Have_SceneModel;
    /// <summary>
    /// 单元格大小
    /// </summary>
    public float GridSize;

    SystemTipsUI systemTips;
    ButtonTipsUI buttonTips;
    void Start () 
	{
        Have_SceneModel = new Dictionary<int, Map_SceneModel>();
        GridSize = 0.64f;

        GameObject_SceneModel.SetActive(false);
        GameObject_SceneInfo.SetActive(false);
        GameObject_GroundBox.SetActive(false);
        GameObject_Part.SetActive(false);
        GameObject_Info.SetActive(false);
        IsSelected = false;

        ClickControl();
        //判断是否为开发者模式，可以创建新的场景，否则只能修改已有的场景。
        isDeveloperPattern = true;
        //非开发者的入口在主菜单或者游戏内，不显示场景创建的选项，而是从入口处选择参数。
        if (isDeveloperPattern)
        {
            //默认读取模板数据
            RefreshSceneList(Button_New);
        }
        systemTips = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_SystemTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject.GetComponent<SystemTipsUI>();
        systemTips.gameObject.SetActive(false);

        buttonTips = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_ButtonTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject.GetComponent<ButtonTipsUI>();
        buttonTips.gameObject.SetActive(false);

        fixedSavePart = FixedSavePart.tooMin;
    }
	
	
	void Update () 
	{
        MouseControl();
    }


    private void FixedUpdate()
    {

    }

    /// <summary>
    /// 创建模板的界面（基于表格数据）
    /// </summary>
    /// <param name="btn"></param>
    void NewModelInfo(GameObject btn)
    {
        GameObject_SceneModel.SetActive(true);
        UIEventListener.Get(Button_OK).onClick = CreateNewModel;
    }
    /// <summary>
    /// 修改模板场景的界面（基于已经完成的模板）
    /// </summary>
    public void LoadModel(GameObject btn)
    {
        GameObject_SceneInfo.SetActive(true);
        int id = int.Parse(btn.name);
        Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(id);
        Label_Name.SetText(false, ms.writeName);
        UITexture ut = Texture_ItemPici.GetComponent<UITexture>();
        ut.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_SceneModel + ms.icon);
        ut.width = 64;
        ut.height = 64;
        Label_Model.SetText(false, ms.id.ToString());
        Label_SizeX.SetText(false, ms.maxSize_X.ToString());
        Label_SizeY.SetText(false, ms.maxSize_Y.ToString());
        Button_Yes.GetComponentInChildren<UIText>().SetText(false, "读取模板");

        SceneName = ms.writeName;
        SceneNum = ms.id;
        SceneSizeX = ms.maxSize_X;
        SceneSizeY = ms.maxSize_Y;
        ModelID = id;
        UIEventListener.Get(Button_Yes).onClick = GetSceneFile;
        //读取json数据，创建场景
    }

    /// <summary>
    /// 刷新模板场景列表
    /// </summary>
    /// <param name="btn"></param>
    void RefreshSceneList(GameObject btn)
    {
        Button_ItemEX.SetActive(true);
        NGUITools.DestroyChildren(GameObject_Pos.transform);
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
                UIEventListener.Get(go.gameObject).onClick = LoadModel;
            }
            GameObject_Pos.GetComponent<UIGrid>().enabled = true;
        }
        else
        {
            CommonFunc.GetInstance.SetButtonState(Button_New, false);
        }
        Button_ItemEX.SetActive(false);
    }


    /// <summary>
    /// 保存场景模板，
    /// 读取时用“+”分割主属性，
    /// 用“,”和“;”分割格子属性，
    /// 用“[”和“]”分割建筑属性，
    /// 用“《”和“》”分割NPC属性
    /// </summary>
    /// <param name="btn"></param>
    void SaveScene(GameObject btn)
    {
        GameObject_SceneInfo.SetActive(true);
        int id = int.Parse(btn.name);
        Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(id);
        Label_Name.SetText(false, ms.writeName);
        UITexture ut = Texture_ItemPici.GetComponent<UITexture>();
        ut.mainTexture = Resources.Load<Texture>(Data_Static.MapPic_SceneModel + ms.icon);
        ut.width = 64;
        ut.height = 64;
        Label_Model.SetText(false, ms.id.ToString());
        Label_SizeX.SetText(false, ms.maxSize_X.ToString());
        Label_SizeY.SetText(false, ms.maxSize_Y.ToString());
        Button_Yes.GetComponentInChildren<UIText>().SetText(false, "保存场景");

        //创建文本提示
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        //保存主数据、建筑数据、格子数据到一个文件中
        Dictionary<int, string> dic_AllPartData = new Dictionary<int, string>();
        string Main = "";
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
                    //用“+”分割模块的主属性。
                    Main = item.Value.id + "+" + item.Value.writeName + "+" + item.Value.sizeX + "+" + item.Value.sizeY + "+" + item.Value.modelID + "+" + item.Value.upScene + "+";
                    Grid = Dic_SavePart_Grid[item.Value.id] + "+";
                    dic_AllPartData.Add(item.Value.id, Main + Grid + Building);

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
                    //用“+”分割模块的主属性。
                    Main = item.Value.id + "+" + item.Value.writeName + "+" + item.Value.modelID + "+" + item.Value.upScene + "+";
                    Grid = Dic_SavePart_Grid[item.Value.id] + "+";
                    dic_AllPartData.Add(item.Value.id, Main + Grid + Building);

                    go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_14"), 
                        string.Format(LanguageMgr.GetInstance.GetText("BuildingMode_15"), newScene.Dic_MapPart.Count));
                }

            }

        }
        JsonReader.WriteJson(Data_Static.save_pathScene + newScene.id + ".json", dic_AllPartData);
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

    }
    /// <summary>
    /// 全部重置
    /// </summary>
    /// <param name="btn"></param>
    void Return(GameObject btn)
    {

    }

    void IsInputOK()
    {
        //判断输入内容正误
        if (isNameOK && isNumOK && isSizeXOK && isSizeYOK)
        {
            CommonFunc.GetInstance.SetButtonState(Button_OK, true);
        }
        else
        {

            CommonFunc.GetInstance.SetButtonState(Button_OK, false);
        }
    }


    void CloseModel(GameObject btn)
    {
        GameObject_SceneModel.SetActive(false);
    }
    void CloseInfo(GameObject btn)
    {
        GameObject_SceneInfo.SetActive(false);
    }

    private void ClickControl()
    {
        UIEventListener.Get(Button_New).onClick = NewModelInfo;
        UIEventListener.Get(Button_Old).onClick = RefreshSceneList;
        UIEventListener.Get(Button_Nomal).onClick = ChoosePart_Nomal;
        UIEventListener.Get(Button_Special).onClick = ChoosePart_Special;
        UIEventListener.Get(Button_Clear).onClick = Clear;
        UIEventListener.Get(Button_Return).onClick = Return;
        UIEventListener.Get(Button_Save).onClick = SaveScene;

        UIEventListener.Get(Button_ReduceX).onClick = ReduceCountX;
        UIEventListener.Get(Button_AddX).onClick = AddCountX;
        UIEventListener.Get(Button_ReduceY).onClick = ReduceCountY;
        UIEventListener.Get(Button_AddY).onClick = AddCountY;

        UIEventListener.Get(Button_Close).onClick = CloseModel;
        UIEventListener.Get(Button_Closei).onClick = CloseInfo;

        UIEventListener.Get(Button_SavePart).onClick = SavePart;
        UIEventListener.Get(Button_PartInfo).onClick = PartInfo;
        UIEventListener.Get(Button_DeletePart).onClick = DeletePart;

        //输入初始化
        Input_Name.value = "请填写名称";
        Input_Name.characterLimit = 10;
        Input_Name.inputType = UIInput.InputType.Standard;
        Input_Name.validation = UIInput.Validation.AlphabetIntChinese;
        EventDelegate.Add(Input_Name.onChange, SetInput_Name);
        SetInput_Name();

        Input_Num.value = (Have_SceneModel.Count + 1).ToString();
        Input_Num.characterLimit = 5;
        Input_Num.inputType = UIInput.InputType.Standard;
        Input_Num.validation = UIInput.Validation.Integer;
        EventDelegate.Add(Input_Num.onChange, SetInput_Num);
        SetInput_Num();

        Input_SizeX.value = 10.ToString();
        Input_SizeX.characterLimit = 3;
        Input_SizeX.inputType = UIInput.InputType.Standard;
        Input_SizeX.validation = UIInput.Validation.Integer;
        EventDelegate.Add(Input_SizeX.onChange, SetInput_SizeX);
        SetInput_SizeX();

        Input_SizeY.value = 10.ToString();
        Input_SizeY.characterLimit = 3;
        Input_SizeY.inputType = UIInput.InputType.Standard;
        Input_SizeY.validation = UIInput.Validation.Integer;
        EventDelegate.Add(Input_SizeY.onChange, SetInput_SizeY);
        SetInput_SizeY();
    }

    private bool isNameOK;
    private bool isNumOK;
    private bool isSizeXOK;
    private bool isSizeYOK;
    /// <summary>
    /// 监听名称输入
    /// </summary>
    void SetInput_Name()
    {
        if (Input_Name.value == "" || Input_Name.value.Contains(" ") || Input_Name.value == null)
        {
            isNameOK = false;
        }
        else
        {
            SceneName = Input_Name.value;
            isNameOK = true;
        }
        IsInputOK();
    }


    /// <summary>
    /// 监听编号输入
    /// </summary>
    void SetInput_Num()
    {
        int inputNumSuccess;
        if (int.TryParse(Input_Num.value, out inputNumSuccess))
        {
            int sNum = int.Parse(Input_Num.value);
            //临时修改，只能创建表格内有的模板
            if (Have_SceneModel.ContainsKey(sNum))
            {
                Sprite_SignWrong.SetActive(false);
                Sprite_SignRight.SetActive(true);
                SceneNum = sNum;
                isNumOK = true;
            }
            else
            {
                Sprite_SignWrong.SetActive(true);
                Sprite_SignRight.SetActive(false);
                isNumOK = false;
            }
        }
        else
        {
            isNumOK = false;
        }
        IsInputOK();
    }


    /// <summary>
    /// 监听数字X输入
    /// </summary>
    void SetInput_SizeX()
    {
        int inputSizeXSuccess;
        if (int.TryParse(Input_SizeX.value, out inputSizeXSuccess))
        {
            int Num = int.Parse(Input_SizeX.value);
            if (Num > 128)
            {
                Input_SizeX.value = 128.ToString();
            }
            else if (Num <= 10)
            {
                Input_SizeX.value = 10.ToString();
            }
            if (Num > 10)
            {
                SceneSizeX = int.Parse(Input_SizeX.value);
                isSizeXOK = true;
            }
            else
            {
                isSizeXOK = false;
            }
        }
        else
        {
            isSizeXOK = false;
        }
        IsInputOK();
    }
    //减少X数量
    void ReduceCountX(GameObject btn)
    {
        int Num = int.Parse(Input_SizeX.value);
        if (Num <= 10)
        {
            Input_SizeX.value = 10.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_ReduceX, false);
        }
        else
        {
            Num = Num - 1;
            Input_SizeX.value = Num.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_AddX, true);
            CommonFunc.GetInstance.SetButtonState(Button_ReduceX, true);
        }
        SetInput_SizeX();
    }
    //增加X数量
    void AddCountX(GameObject btn)
    {
        int Num = int.Parse(Input_SizeX.value);
        if (Num >= 128)
        {
            Input_SizeX.value = 128.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_AddX, false);
        }
        else if (Num >= 10)
        {
            Num = Num + 1;
            Input_SizeX.value = Num.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_AddX, true);
            CommonFunc.GetInstance.SetButtonState(Button_ReduceX, true);
        }
        SetInput_SizeX();
    }

    /// <summary>
    /// 监听数字Y输入
    /// </summary>
    void SetInput_SizeY()
    {
        int inputSizeYSuccess;
        if (int.TryParse(Input_SizeY.value, out inputSizeYSuccess))
        {
            int Num = int.Parse(Input_SizeY.value);
            if (Num > 128)
            {
                Input_SizeY.value = 128.ToString();
            }
            else if (Num <= 10)
            {
                Input_SizeY.value = 10.ToString();
            }
            if (Num > 10)
            {
                SceneSizeY = int.Parse(Input_SizeY.value);
                isSizeYOK = true;
            }
            else
            {
                isSizeYOK = false;
            }
        }
        else
        {
            isSizeYOK = false;
        }
        IsInputOK();
    }
    //减少Y数量
    void ReduceCountY(GameObject btn)
    {
        int Num = int.Parse(Input_SizeY.value);
        if (Num <= 10)
        {
            Input_SizeY.value = 10.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_ReduceY, false);
        }
        else
        {
            Num = Num - 1;
            Input_SizeY.value = Num.ToString();
            SceneSizeY = int.Parse(Input_SizeY.value);
            CommonFunc.GetInstance.SetButtonState(Button_AddY, true);
            CommonFunc.GetInstance.SetButtonState(Button_ReduceY, true);
        }
        SetInput_SizeY();
    }
    //增加Y数量
    void AddCountY(GameObject btn)
    {
        int Num = int.Parse(Input_SizeY.value);
        if (Num >= 128)
        {
            Input_SizeY.value = 128.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_AddY, false);
        }
        else if (Num >=10)
        {
            Num = Num + 1;
            Input_SizeY.value = Num.ToString();
            CommonFunc.GetInstance.SetButtonState(Button_AddY, true);
            CommonFunc.GetInstance.SetButtonState(Button_ReduceY, true);
        }
        SetInput_SizeY();
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
    /// 错误的格子列表
    /// </summary>
    public GameObject Part_Wrong;
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

    public int ModelID;
    public string SceneName;
    public int SceneNum;
    public int SceneSizeX;
    public int SceneSizeY;

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
    int selectedModelID;

    private void CreateNewModel(GameObject btn)
    {
        GameObject_SceneModel.SetActive(false);
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
        if (isNew)
        {
            newScene.id = SceneNum;
            newScene.writeName = SceneName;
            newScene.modelID = SceneNum;
            newScene.Dic_MapPart = new Dictionary<int, MapPart>();
        }
        else
        {
            Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(ModelID);

            Dic_SavePart_Grid = new Dictionary<int, string>();
            Dic_SavePart_Building = new Dictionary<int, string>();

            newScene = new MapScene();
            newScene.id = ModelID;
            newScene.writeName = ms.writeName;
            newScene.modelID = ModelID;
            newScene.Dic_MapPart = new Dictionary<int, MapPart>();

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
        //创建一个模块
        newPart = new MapPart();
        newPart.id = newScene.Dic_MapPart.Count + 1;
        newPart.writeName = mp.name;
        newPart.upScene = newScene.id;
        newPart.Dic_Building = new Dictionary<int, PartBuilding>();
        newPart.Dic_MapBox = new Dictionary<string, MapBox>();

        tipLabel = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        tipLabel.SetActive(false);

        GameObject_Info.SetActive(true);
        Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_5"));

        wrongGrids = 0;
        tempGrids = new Dictionary<string, GameObject>();
    }

    GameObject tipLabel;
    /// <summary>
    /// 保存模块的限制条件
    /// </summary>
    enum FixedSavePart:int
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
        /// 有障碍
        /// </summary>
        other = 3,
    }
    FixedSavePart fixedSavePart;
    int wrongGrids;
    /// <summary>
    /// 临时生成的格子列表
    /// </summary>
    Dictionary<string,GameObject> tempGrids;


    /// <summary>
    /// 鼠标控制（主方法）
    /// </summary>
    void MouseControl()
    {
        Map_PartModel mp = SelectDao.GetDao().SelectMap_PartModel(selectedModelID);
        //在选中了模块的情况下
        if (IsSelected)
        {
            Button_PartInfo.SetActive(false);
            Button_SavePart.SetActive(true);
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
                //点击鼠标右键删除选中的部分区域
                if (Input.GetMouseButton(1))
                {
                    if (!AllowToPlace())
                    {
                        //如果点击到了已有的格子，删除当前模块中格子所在的行以及下方的所有行的数据，更新矩形，删除已经生成的游戏物体。
                        if (GridGo != null && newPart.Dic_MapBox.ContainsKey(GridGo.name))
                        {
                            Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_8"));
                            Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(166 / 255, 40 / 255, 40 / 255, 255 / 255);
                            List<string> posList = new List<string>();
                            //计算所有格子里面最大的Y值
                            List<float> posYListA = new List<float>();
                            foreach (var item in newPart.Dic_MapBox)
                            {
                                float y = float.Parse(item.Key.Split('_')[1]);
                                posYListA.Add(y);
                            }
                            float maxY = maxValue(posYListA);
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
                                    newPart.Dic_MapBox.Remove(item);
                                    Dic_AllGrid[item] = -1;
                                    Destroy(tempGrids[item]);
                                    tempGrids.Remove(item);
                                }
                                //如果点击到最上面一行，则删除下方所有格子和本行点击位置的右侧所有格子。
                                else if (itemPosY == boxPosY && itemPosY == maxY && itemPosX > boxPosX)
                                {
                                    newPart.Dic_MapBox.Remove(item);
                                    Dic_AllGrid[item] = -1;
                                    Destroy(tempGrids[item]);
                                    tempGrids.Remove(item);
                                }
                            }
                            //再次更新矩形数据，改变矩形大小和位置。
                            List<float> posXList = new List<float>();
                            List<float> posYList = new List<float>();
                            foreach (var item in newPart.Dic_MapBox)
                            {
                                float x = float.Parse(item.Key.Split('_')[0]);
                                float y = float.Parse(item.Key.Split('_')[1]);
                                posXList.Add(x);
                                posYList.Add(y);
                            }
                            Vector3 LeftBottomPos = new Vector3(minValue(posXList), minValue(posYList), 0);
                            Vector3 RightTopPos = new Vector3(maxValue(posXList), maxValue(posYList), 0);
                            //根据2个坐标创建一个符合范围的矩形
                            SelectedBox.SetActive(true);
                            SpriteRenderer srB = SelectedBox.GetComponent<SpriteRenderer>();
                            srB.sprite = Resources.Load<Sprite>(Data_Static.MapPic_Nomal + "Grid_Right");
                            //根据矩形的左下角坐标和大小创建矩形
                            float distanceX = (RightTopPos.x - LeftBottomPos.x) / GridSize + 1;
                            float distanceY = (RightTopPos.y - LeftBottomPos.y) / GridSize + 1;
                            int countX = (int)Mathf.Abs(distanceX);
                            int countY = (int)Mathf.Abs(distanceY);
                            SelectedBox.transform.localScale = new Vector3(distanceX, distanceY, 1);
                            SelectedBox.transform.localPosition = LeftBottomPos + new Vector3(((distanceX - 1) * GridSize) / 2, ((distanceY - 1) * GridSize) / 2, 0);
                        }
                    }
                    else
                    {

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
                        //根据鼠标点击到的位置更新矩形信息。
                        CreateBoxArea(boxPosX, boxPosY);

                    }
                    //如果点击后有其他格子
                    else
                    {

                    }
                }
            }       
        }
        //处理未创建模块时的操作
        else
        {
            SelectedGrid.SetActive(false);
            SelectedBox.SetActive(false);
            GameObject_Part.SetActive(true);
            GameObject_List.SetActive(true);
            Button_PartInfo.SetActive(true);
            Button_SavePart.SetActive(false);
            //点击鼠标右键等于删除一行
            if (Input.GetMouseButton(1))
            {

            }
            //当遇到碰撞体时，判断模块信息
            if (!AllowToPlace())
            {
                //如果碰到的为已生成的模块，读取信息
                if (PartGo != null)
                {
                    int ID = int.Parse(PartGo.name);
                    MapPart part = newScene.Dic_MapPart[ID];
                    Button_DeletePart.name = PartGo.name;
                    Button_PartInfo.name = PartGo.name;
                    Label_CreatingName.SetText(false, part.writeName);
                    Label_PartAreaSizeX.SetText(false, part.sizeX.ToString());
                    Label_PartAreaSizeY.SetText(false, part.sizeY.ToString());
                }
                else
                {

                }

            }
        }
    }

    /// <summary>
    /// 创建矩形区域范围
    /// </summary>
    /// <param name="boxPosX">当前鼠标坐标X</param>
    /// <param name="boxPosY">当前鼠标坐标Y</param>
    void CreateBoxArea(float boxPosX, float boxPosY)
    {
        Map_PartModel mp = SelectDao.GetDao().SelectMap_PartModel(selectedModelID);
        string posName = posLabelChange(boxPosX, boxPosY);
        Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_1"));
        Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(0 / 255, 91 / 255, 8 / 255, 255 / 255);

        //刷新格子信息到模块数据中的字典里,如果不是-1，表示已经是其他模块的格子了。
        if (Dic_AllGrid[posName] == -1 && Dic_AllGrid[posName] == newPart.id)
        {
            Dic_AllGrid[posName] = newPart.id;
        }
        //拒绝重复的格子，添加新格子信息进入新模块
        if (!newPart.Dic_MapBox.ContainsKey(posName))
        {
            //给格子的属性赋值
            newMapBox = new MapBox();
            newMapBox.Pos = posName;
            newMapBox.upPart = newPart.id;
            newPart.Dic_MapBox.Add(posName, newMapBox);
            createGrid(boxPosX, boxPosY, Data_Static.MapPic_PartModel + mp.mapIcon, Part_Temp);
        }

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
        Vector3 LeftBottomPos = new Vector3(minValue(posXList), minValue(posYList), 0);
        Vector3 RightTopPos = new Vector3(maxValue(posXList), maxValue(posYList), 0);
        //根据2个坐标创建一个符合范围的矩形
        SelectedBox.SetActive(true);
        SpriteRenderer srB = SelectedBox.GetComponent<SpriteRenderer>();
        srB.sprite = Resources.Load<Sprite>(Data_Static.MapPic_Nomal + "Grid_Right");
        //根据矩形的左下角坐标和大小创建矩形
        float distanceX = (RightTopPos.x - LeftBottomPos.x) / GridSize + 1;
        float distanceY = (RightTopPos.y - LeftBottomPos.y) / GridSize + 1;
        int countX = (int)Mathf.Abs(distanceX);
        int countY = (int)Mathf.Abs(distanceY);
        SelectedBox.transform.localScale = new Vector3(distanceX, distanceY, 1);
        SelectedBox.transform.localPosition = LeftBottomPos + new Vector3(((distanceX - 1) * GridSize) / 2, ((distanceY - 1) * GridSize) / 2, 0);

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
                if (!newPart.Dic_MapBox.ContainsKey(gridName))
                {
                    if (Dic_AllGrid.ContainsKey(gridName))
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
                            if (!tempGrids.ContainsKey(gridName))
                            {
                                createGrid(gridPosX, gridPosY, Data_Static.MapPic_PartModel + mp.mapIcon, Part_Temp);
                            }
                        }
                        //判断选中区域内有其他模块的格子为障碍物
                        else
                        {
                            if (Dic_AllGrid[gridName] != newPart.id)
                            {
                                //在错误格子的位置创建红色的标识格子
                                createGrid(gridPosX, gridPosY, Data_Static.MapPic_Nomal + "Grid_Wrong", Part_Wrong);
                                wrongGrids++;
                                fixedSavePart = FixedSavePart.other;
                                Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_4"));
                                Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(166 / 255, 40 / 255, 40 / 255, 255 / 255);
                                srB.sprite = Resources.Load<Sprite>(Data_Static.MapPic_Nomal + "Grid_Wrong");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("找不到此格子的坐标信息：" + gridName);
                    }


                }
            }
        }

        //给状态赋值
        newPart.sizeX = countX;
        newPart.sizeY = countY;
        Label_CreatingName.SetText(false, mp.name);
        Label_PartAreaSizeX.SetText(false, countX.ToString());
        Label_PartAreaSizeY.SetText(false, countY.ToString());




        //判断最小格子数量限制
        int minGridNum = int.Parse(SelectDao.GetDao().SelectSystem_Config(2).values);
        if (newPart.Dic_MapBox.Count < minGridNum)
        {
            fixedSavePart = FixedSavePart.tooMin;
            Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_3") + minGridNum);
            Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(166 / 255, 40 / 255, 40 / 255, 255 / 255);
            srB.sprite = Resources.Load<Sprite>(Data_Static.MapPic_Nomal + "Grid_Wrong");
        }
        //判断最大格子数量限制
        int maxGridNum = int.Parse(SelectDao.GetDao().SelectSystem_Config(1).values);
        //如果发现框选的格子数量超过限制，则弹出警告，取消框选
        if (newPart.Dic_MapBox.Count > maxGridNum)
        {
            fixedSavePart = FixedSavePart.tooMax;
            Label_Tip.SetText(false, LanguageMgr.GetInstance.GetText("BuildingMode_2") + maxGridNum);
            Label_Tip.gameObject.GetComponent<UILabel>().color = new Color(166 / 255, 40 / 255, 40 / 255, 255 / 255);
            srB.sprite = Resources.Load<Sprite>(Data_Static.MapPic_Nomal + "Grid_Wrong");
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
    /// <summary>
    /// 计算最大值
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
    /// 碰撞到的格子
    /// </summary>
    GameObject GridGo;
    /// <summary>
    /// 碰撞到的已生成模块
    /// </summary>
    GameObject PartGo;
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
                //如果射线穿到的碰撞体的layer为“8：Ground”
                if (item.gameObject.layer == 8)
                {
                    GridGo = item.gameObject;
                }
                //如果射线穿到的碰撞体的layer为“9：World”
                else if (item.gameObject.layer == 9)
                {
                    PartGo = item.gameObject;
                }
            }
            return false;
        }
        else
        {
            GridGo = null;
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
            tempGrids.Add(grid.name, grid);
        }

    }

    /// <summary>
    /// 已完成的格子列表
    /// </summary>
    Dictionary<int, GameObject> alreadyGrids;
    /// <summary>
    /// 保存模块
    /// </summary>
    /// <param name="btn"></param>
    void SavePart(GameObject btn)
    {
        string dic_Grid = "";

        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;
        if (newPart.Dic_MapBox.Count>0)
        {
            go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_11"), string.Format(LanguageMgr.GetInstance.GetText("BuildingMode_12"),
              newPart.writeName, newPart.id, newPart.Dic_MapBox.Count));
            foreach (var item in newPart.Dic_MapBox)
            {
                //用“,”分割一个格子的pos和所属模块的属性，用“;”分割不同格子的数据。
                dic_Grid = dic_Grid + item.Key + "," + item.Value.upPart +";";
            }
            //在场景类中写入模块信息。
            newScene.Dic_MapPart.Add(newPart.id, newPart);
            Dic_SavePart_Grid.Add(newPart.id, dic_Grid);

            //将矩形放入已完成目录下，并且生成碰撞体
            GameObject already = SelectedBox;
            already.AddComponent<BoxCollider2D>();
            already.transform.parent = Part_Already.transform;
            already.name = newPart.id.ToString();
            alreadyGrids.Add(newPart.id, already);
        }
        else
        {
            go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_11"), LanguageMgr.GetInstance.GetText("BuildingMode_13"));
        }
    }

    /// <summary>
    /// 打开模块信息界面
    /// </summary>
    /// <param name="btn"></param>
    void PartInfo(GameObject btn)
    {
        int id = int.Parse(btn.name);
    }

    /// <summary>
    /// 删除模块
    /// </summary>
    /// <param name="btn"></param>
    void DeletePart(GameObject btn)
    {
        GameObject go = CommonFunc.GetInstance.UI_Instantiate(Data_Static.UIpath_LableTips, FindObjectOfType<UIRoot>().transform, Vector3.zero, Vector3.one).gameObject;

        //删除当前的相关数据
        if (IsSelected)
        {
            go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_9"), string.Format(LanguageMgr.GetInstance.GetText("BuildingMode_10"),
                newPart.writeName, newPart.id, newPart.Dic_MapBox.Count));
            foreach (var item in newPart.Dic_MapBox)
            {
                Dic_AllGrid[item.Key] = -1;
            }
            NGUITools.DestroyChildren(Part_Temp.transform);
            NGUITools.DestroyChildren(Part_Wrong.transform);
            newPart.Dic_MapBox.Clear();
        }
        //删除保存后的相关数据
        else
        {
            int partID = int.Parse(btn.name);
            newScene.Dic_MapPart.Remove(partID);
            Dic_SavePart_Grid.Remove(partID);
            Dic_SavePart_Building.Remove(partID);
            Destroy(alreadyGrids[partID]);
            alreadyGrids.Remove(partID);
            go.GetComponent<LableTipsUI>().SetAll(true, LanguageMgr.GetInstance.GetText("BuildingMode_9"), string.Format(LanguageMgr.GetInstance.GetText("BuildingMode_10"),
                  newPart.writeName, newPart.id, newPart.Dic_MapBox.Count));
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
    public int writeName { get; set; }
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
    public PartBuilding(int _id, int _writeName, int _modelID, int _upPart, Dictionary<int, buildingFixedNPC> _Dic_FixedNPC)
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

