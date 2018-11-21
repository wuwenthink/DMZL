using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapUI : MonoBehaviour
{
    public GameObject GameObject_AreaLablePos;
    public GameObject Button_AreaLableEX;

    public UISlider Slider_MapScale;
    public UILabel Label_ScaleNum;
    public GameObject Texture_ShengMap;
    public GameObject Texture_FuMap;
    public GameObject Texture_XianMapArea;
    public GameObject Button_ShengAreaEX;
    public GameObject Button_FuAreaEX;
    public GameObject Button_XianAreaEX;

    //大地图信息
    public GameObject GameObject_Sheng;
    public GameObject GameObject_AreaPos;
    public GameObject GameObject_Effect;
    public TweenPosition GameObject_left;
    public TweenPosition GameObject_Right;
    enum AreaState
    {
        sheng,
        fu,
        xian
    }
    AreaState areaState;

    public int areaLimit_X;
    public int areaLimit_Y;

    public GameObject GameObject_MapInfo;
    public GameObject GameObject_AreaTips;
    public UILabel Label_AreaName;
    public GameObject Sprite_AreaName;
    public UILabel Label_adminName;
    public GameObject Sprite_adminName;
    public UILabel Label_Tips_Capital;
    public GameObject Sprite_Tips_Capital;
    public UILabel Label_AreaDes;
    public GameObject Button_CloseTips;

    public GameObject Button_seeAll;
    public GameObject Button_SeeBack;
    public GameObject Button_SeeInfo;
    public GameObject Button_backMainMenu;

    public GameObject GameObject_Map;
    public GameObject GameObject_AreaMap;
    public GameObject GameObject_AreaLableListScroll;
    public UISprite Sprite_TitleBG;
    public UILabel Label_TitleBG;


    //剧本（故事）信息
    public GameObject Sprite_DramaIcon;
    public UILabel Label_DramaName;
    public UILabel Label_TCTime;
    public UILabel Label_ADTime;
    public UILabel Label_ShengNum;
    public UILabel Label_XianNum;
    public UILabel Label_PersonNameEX;
    public UILabel Label_PersonNum;
    public GameObject Button_StoryDes;
    public GameObject Button_BigDes;

    //县志信息
    public UILabel Label_TitleMain;
    public UILabel Label_TitleSign;
    public UILabel Label_TitleDes;
    public UILabel Label_TitleTips;
    public GameObject Button_ToMap;
    public UILabel Label_ToMap;
    public GameObject Button_To5Or1;
    public UILabel Label_To5Or1;
    public GameObject Button_CreatPerson;
    public GameObject Button_backMainMap;
    public GameObject Button_Enter;
    public GameObject GameObject_AMScroll;
    public GameObject GameObject_PersonScroll;
    public GameObject Sprite_BG_AMS;
    public GameObject Sprite_BG_PS;
    public GameObject GameObject_PerosonPos;
    public GameObject GameObject_CityMapPos;
    public GameObject Button_PersonEX;

    public GameObject Texture_CityMapEX;

    //角色信息
    public GameObject GameObject_PersonInfo;
    public GameObject Button_Back;
    public UILabel Label_PTitleName;
    public UILabel Label_HistoryTime;
    public UILabel Label_PersonCommonName;
    public UILabel Label_PersonWordName;
    public UILabel Label_PersonTitleName;
    public UILabel Label_birthday;
    public UILabel Label_place;
    public GameObject GameObject_StoryScroll;
    public GameObject GameObject_StoryPos;
    public UILabel Lable_StoryEX;

    public GameObject Texture_Role;
    public UILabel Labe_TiZhi;
    public UILabel Labe_XingDong;
    public UILabel Labe_WuLi;
    public UILabel Labe_NaiLi;
    public UILabel Labe_PoLi;
    public UILabel Labe_YiLi;
    public UILabel Labe_MouLue;
    public UILabel Labe_YingBian;
    public UILabel Lable_gender;
    public UILabel Lable_nature;
    public UILabel Lable_national;
    public UILabel Lable_force;

    public float mspeed = 0.1f;
    Vector3 StartPosition;
    Vector3 previousPosition;
    Vector3 offset;
    Vector3 PosScale;
    float scale;

    string DramaID;
    public string ClickAreaID;

    List<string> ShengCount;
    List<string> XianCount;
    List<string> RoleCount;

    List<GameObject> List_Sheng;
    List<GameObject> List_Fu;
    List<GameObject> List_Xian;
    List<GameObject> List_AreaLable;
    List<GameObject> List_AreaTexture;
    List<GameObject> PersonList;
    List<GameObject> choosePersonList;

    enum WindowState
    {
        chengWindow,
        shanWindow,
        RoleWindow,
    }
    WindowState windowState;

    bool canMove;

    //void Start ()
    //{
    //    GameObject_Map.GetComponent<UIPanel> ().depth = gameObject.GetComponent<UIPanel> ().depth + 1;
    //    GameObject_AreaLableListScroll.GetComponent<UIPanel> ().depth = GameObject_Map.GetComponent<UIPanel> ().depth + 1;
    //    GameObject_AreaMap.GetComponent<UIPanel> ().depth = GameObject_AreaLableListScroll.GetComponent<UIPanel> ().depth + 1;
    //    GameObject_AMScroll.GetComponent<UIPanel> ().depth = GameObject_AreaMap.GetComponent<UIPanel> ().depth + 1;
    //    GameObject_PersonScroll.GetComponent<UIPanel> ().depth = GameObject_AreaMap.GetComponent<UIPanel> ().depth + 1;
    //    GameObject_PersonInfo.GetComponent<UIPanel> ().depth = GameObject_AMScroll.GetComponent<UIPanel> ().depth + 1;
    //    GameObject_StoryScroll.GetComponent<UIPanel> ().depth = GameObject_PersonInfo.GetComponent<UIPanel> ().depth + 1;

    //    StartPosition = Input.mousePosition;
    //    previousPosition = Input.mousePosition;


    //    GameObject_AreaTips.SetActive (false);
    //    GameObject_AreaMap.SetActive (false);
    //    GameObject_PersonInfo.SetActive (false);

    //    ShengCount = new List<string> ();
    //    XianCount = new List<string> ();
    //    RoleCount = new List<string> ();

    //    //DramaID = CommonFunc.GetInstanceDramaID;

    //    List_Sheng = new List<GameObject> ();
    //    List_Fu = new List<GameObject> ();
    //    List_Xian = new List<GameObject> ();
    //    List_AreaLable = new List<GameObject> ();
    //    List_AreaTexture = new List<GameObject> ();
    //    PersonList = new List<GameObject> ();
    //    choosePersonList = new List<GameObject> ();

    //    GameObject_Effect.SetActive (false);
    //    Button_Enter.GetComponent<BoxCollider> ().enabled = false;

    //    areaState = AreaState.sheng;
    //    canMove = true;

    //    //剧本信息计数
    //    foreach (KeyValuePair<string, Class_Drama_1> dic in SD_Drama_1.Class_Dic)
    //    {
    //        if (dic.Value.type == "2")
    //        {
    //            if (SD_Map_Area.Class_Dic [dic.Value.modelID].areaLv == "2")
    //            {
    //                ShengCount.Add (dic.Value.name);
    //            }
    //            else if (SD_Map_Area.Class_Dic [dic.Value.modelID].areaLv == "4")
    //            {
    //                XianCount.Add (dic.Value.name);
    //            }
    //        }
    //        else if (dic.Value.type == "5")
    //        {
    //            RoleCount.Add (dic.Value.name);
    //        }
    //    }
    //    //剧本信息
    //    Sprite_DramaIcon.GetComponent<UISprite> ().spriteName = SD_Drama_Main.Class_Dic [DramaID].icon;
    //    Label_DramaName.text = SD_Drama_Main.Class_Dic [DramaID].name;
    //    Label_TCTime.text = SD_Drama_Main.Class_Dic [DramaID].dynasty
    //        + "·"
    //        + SD_Drama_Main.Class_Dic [DramaID].Reigntitle
    //        + SD_Drama_Main.Class_Dic [DramaID].TCYear
    //        + "年";
    //    Label_ADTime.text = "公元" + SD_Drama_Main.Class_Dic [DramaID].ADYear + "年";
    //    Label_ShengNum.text = ShengCount [0] + "等" + ShengCount.Count + "省";
    //    Label_XianNum.text = XianCount [0] + "等" + XianCount.Count + "县";
    //    Label_PersonNameEX.text = RoleCount [0] + "等";
    //    Label_PersonNum.text = RoleCount.Count + "人";

    //    ClickControl ();
    //    CloneArea (DramaID);
    //}


    //void Update ()
    //{
    //    areaStateF ();

    //    if (canMove)
    //    {
    //        MapMove ();
    //    }
    //}

    ////地区状态判断
    //void areaStateF ()
    //{
    //    switch (areaState)
    //    {
    //        case AreaState.sheng:
    //            //areaLimit_X = Mathf.Clamp(areaLimit_X, 695, -695);
    //            //areaLimit_Y = Mathf.Clamp(areaLimit_Y, -670, 670);

    //            Texture_ShengMap.SetActive (true);
    //            Texture_FuMap.SetActive (false);
    //            Texture_XianMapArea.SetActive (false);



    //            break;
    //        case AreaState.fu:
    //            //areaLimit_X = Mathf.Clamp(areaLimit_X, 695, -695);
    //            //areaLimit_Y = Mathf.Clamp(areaLimit_Y, -670, 670);



    //            Texture_ShengMap.SetActive (false);
    //            Texture_FuMap.SetActive (true);
    //            Texture_XianMapArea.SetActive (false);
    //            break;
    //        case AreaState.xian:
    //            //areaLimit_X = Mathf.Clamp(areaLimit_X, 695, -695);
    //            //areaLimit_Y = Mathf.Clamp(areaLimit_Y, -670, 670);



    //            Texture_ShengMap.SetActive (false);
    //            Texture_FuMap.SetActive (false);
    //            Texture_XianMapArea.SetActive (true);
    //            break;
    //    }
    //}

    ////地区移动
    //public void MapMove ()
    //{
    //    //边界限制
    //    //PosScale = new Vector3(Mathf.Clamp(PosScale.x, 0.13f, 1), Mathf.Clamp(PosScale.x, 0.13f, 1), Mathf.Clamp(PosScale.x, 0.13f, 1));
    //    //GameObject_AreaPos.transform.localPosition = new Vector3(areaLimit_X, areaLimit_Y, 0);
    //    Vector3 mousePos = NGUITools.FindCameraForLayer (FindObjectOfType<UIRoot> ().gameObject.layer).ScreenToViewportPoint (Input.mousePosition);
    //    Vector3 limitPos = mousePos * 10000;
    //    if (limitPos.x > 1200 && limitPos.x < 9800 && limitPos.y > 1300 && limitPos.y < 9000)
    //    {

    //        //用键盘移动
    //        if (Input.GetKey (KeyCode.W))//按W往上
    //        {
    //            GameObject_AreaPos.transform.Translate (Vector3.down * Time.deltaTime * mspeed * 10);
    //        }
    //        if (Input.GetKey (KeyCode.S))//按S往下
    //        {
    //            GameObject_AreaPos.transform.Translate (Vector3.up * Time.deltaTime * mspeed * 10);
    //        }
    //        if (Input.GetKey (KeyCode.A))//按A往左
    //        {
    //            GameObject_AreaPos.transform.Translate (Vector3.right * Time.deltaTime * mspeed * 10);
    //        }
    //        if (Input.GetKey (KeyCode.D))//按A往右
    //        {
    //            GameObject_AreaPos.transform.Translate (Vector3.left * Time.deltaTime * mspeed * 10);
    //        }

    //        //鼠标移动
    //        if (Input.GetMouseButtonDown (0))
    //        {
    //            StartPosition = Input.mousePosition;
    //            previousPosition = Input.mousePosition;
    //        }
    //        if (Input.GetMouseButton (0))
    //        {
    //            offset = Input.mousePosition - previousPosition;
    //            previousPosition = Input.mousePosition;
    //            GameObject_AreaPos.transform.Translate (offset * mspeed * Time.deltaTime);
    //        }

    //        //鼠标滚轮放大缩小
    //        //if (Input.GetAxis("Mouse ScrollWheel") < 0)
    //        //{
    //        //    PosScale += (Vector3.one / 20) * -mspeed * 3;
    //        //}
    //        //if (Input.GetAxis("Mouse ScrollWheel") > 0)
    //        //{
    //        //    PosScale += (Vector3.one / 20) * +mspeed * 3;
    //        //}
    //        //GameObject_AreaPos.transform.localScale = PosScale;
    //    }

    //    //Slider_MapScale.value = (1 - GameObject_AreaPos.transform.localScale.x) * (1 / 0.87f);


    //    //float scale = PosScale.x;
    //    //if (scale >= 0.13f && scale <= 0.5f)
    //    //{
    //    //    foreach (GameObject go in List_ShengAreaNear)
    //    //    {
    //    //        go.SetActive(false);
    //    //    }
    //    //    foreach (GameObject go in List_ShengAreaFar)
    //    //    {
    //    //        go.SetActive(true);
    //    //        go.transform.localScale = Vector3.one * (1 / scale);
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    foreach (GameObject go in List_ShengAreaNear)
    //    //    {
    //    //        go.SetActive(true);
    //    //    }
    //    //    foreach (GameObject go in List_ShengAreaFar)
    //    //    {
    //    //        go.SetActive(false);
    //    //    }
    //    //}

    //}



    ////赋值给地图
    //public void CloneArea (string drama_id)
    //{
    //    //全国地图
    //    if (areaState == AreaState.sheng)
    //    {

    //        GameObject_AreaPos.transform.localPosition = new Vector3 (-275, -150);

    //        Button_ShengAreaEX.SetActive (true);
    //        Button_AreaLableEX.SetActive (true);
    //        //遍历剧本1表
    //        foreach (KeyValuePair<string, Class_Drama_1> dic in SD_Drama_1.Class_Dic)
    //        {
    //            //类型为2且场景表中场景等级为2的场景
    //            if (dic.Value.value6 == "1" && SD_Map_Area.Class_Dic [dic.Value.modelID].areaLv == "2")
    //            {
    //                string id = dic.Key;
    //                string forceID = SD_Drama_1.Class_Dic [id].value2;
    //                string forceName = SD_Force.Class_Dic [forceID].flag;
    //                string shengName = SD_Drama_1.Class_Dic [id].name;

    //                int r = SD_Force.Class_Dic [forceID]._colorR ();
    //                int g = SD_Force.Class_Dic [forceID]._colorG ();
    //                int b = SD_Force.Class_Dic [forceID]._colorB ();
    //                int a = SD_Force.Class_Dic [forceID]._colorA ();
    //                Color colorValue = FindObjectOfType<GameObject_Static> ().ColorDeal (r, g, b, a);


    //                //地区按钮的赋值
    //                GameObject btn_AreaLable = (GameObject) Instantiate (Button_AreaLableEX);
    //                btn_AreaLable.transform.localPosition = Vector3.zero;
    //                btn_AreaLable.transform.parent = GameObject_AreaLablePos.transform;
    //                btn_AreaLable.transform.localScale = Vector3.one;
    //                btn_AreaLable.name = id;
    //                GameObject_AreaLablePos.GetComponent<UIGrid> ().enabled = true;
    //                List_AreaLable.Add (btn_AreaLable);
    //                foreach (Transform child in btn_AreaLable.GetComponentsInChildren<Transform> ())
    //                {
    //                    if (child.name == "Sprite_ALForce")
    //                    {
    //                        child.GetComponentInChildren<UISprite> ().color = colorValue;
    //                    }
    //                    if (child.name == "Label_ALForce")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = forceName;
    //                    }
    //                    if (child.name == "Label_AreaLableDes")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = shengName;
    //                    }
    //                }
    //                UIEventListener.Get (btn_AreaLable).onClick = AreaLableClick;
    //                UIEventListener.Get (btn_AreaLable).onHover = AreaLableHover;


    //                //省的赋值
    //                GameObject btn_sheng = (GameObject) Instantiate (Button_ShengAreaEX);
    //                btn_sheng.transform.localPosition = Vector3.zero;
    //                btn_sheng.transform.parent = GameObject_AreaPos.transform;
    //                btn_sheng.name = id;
    //                btn_sheng.transform.localScale = Vector3.one;
    //                float posX = SD_Drama_1.Class_Dic [id]._posX ();
    //                float posY = SD_Drama_1.Class_Dic [id]._posY ();
    //                float posZ = SD_Drama_1.Class_Dic [id]._posZ ();
    //                btn_sheng.transform.localPosition = new Vector3 (posX, posY, posZ);
    //                List_Sheng.Add (btn_sheng);
    //                UIEventListener.Get (btn_sheng).onHover = AreaLableHover;
    //                UIEventListener.Get (btn_sheng).onClick = AreaLableClick;
    //                //各省赋值
    //                foreach (Transform child in btn_sheng.GetComponentsInChildren<Transform> ())
    //                {
    //                    if (child.name == "GameObject_Info")
    //                    {
    //                        int info_x = int.Parse (dic.Value.value7);
    //                        int info_y = int.Parse (dic.Value.value8);
    //                        child.localPosition = new Vector3 (info_x, info_y, 0);
    //                        btn_sheng.GetComponent<BoxCollider> ().center = new Vector3 (info_x, info_y, 0);
    //                    }
    //                    if (child.name == "Sprite_Force")
    //                    {
    //                        child.GetComponentInChildren<UISprite> ().color = colorValue;
    //                    }
    //                    if (child.name == "Label_Force")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = forceName;
    //                    }
    //                    if (child.name == "Label_ShengName")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = shengName;
    //                    }
    //                    if (child.name == "Texture_ShengMap")
    //                    {
    //                        string path = Data_Static.MapPic_Area + SD_Map_Area.Class_Dic [dic.Value.modelID].mapImg;
    //                        child.GetComponentInChildren<UITexture> ().mainTexture = Resources.Load<Texture> (path);
    //                        int sizeX = int.Parse (dic.Value.value4);
    //                        int sizeY = int.Parse (dic.Value.value5);
    //                        child.GetComponentInChildren<UITexture> ().width = sizeX;
    //                        child.GetComponentInChildren<UITexture> ().height = sizeY;
    //                    }
    //                    if (child.name == "Texture_ShengUp")
    //                    {
    //                        string path = Data_Static.MapPic_Area + SD_Map_Area.Class_Dic [dic.Value.modelID].mapImg.Replace ("Area", "Choose");
    //                        child.GetComponentInChildren<UITexture> ().mainTexture = Resources.Load<Texture> (path);
    //                        int sizeX = int.Parse (dic.Value.value4);
    //                        int sizeY = int.Parse (dic.Value.value5);
    //                        child.GetComponentInChildren<UITexture> ().width = sizeX;
    //                        child.GetComponentInChildren<UITexture> ().height = sizeY;
    //                        child.name = id;
    //                        List_AreaTexture.Add (child.gameObject);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    //各省地图
    //    else if (areaState == AreaState.fu)
    //    {
    //        GameObject_AreaPos.transform.localPosition = new Vector3 (0, -450, 0);

    //        Button_FuAreaEX.SetActive (true);
    //        Button_AreaLableEX.SetActive (true);
    //        //遍历剧本1表
    //        foreach (KeyValuePair<string, Class_Drama_1> dic in SD_Drama_1.Class_Dic)
    //        {
    //            //类型为2，场景表中场景等级为3，上级地图为点击区域ID的场景
    //            if (dic.Value.value6 == "1" && SD_Map_Area.Class_Dic [dic.Value.modelID].areaLv == "3" && SD_Map_Area.Class_Dic [dic.Value.modelID].UpArea == ClickAreaID)
    //            {
    //                string id = dic.Key;
    //                string forceID = SD_Drama_1.Class_Dic [id].value2;
    //                string forceName = SD_Force.Class_Dic [forceID].flag;
    //                string shengName = SD_Drama_1.Class_Dic [id].name;

    //                int r = SD_Force.Class_Dic [forceID]._colorR ();
    //                int g = SD_Force.Class_Dic [forceID]._colorG ();
    //                int b = SD_Force.Class_Dic [forceID]._colorB ();
    //                int a = SD_Force.Class_Dic [forceID]._colorA ();
    //                Color colorValue = FindObjectOfType<GameObject_Static> ().ColorDeal (r, g, b, a);

    //                //地区按钮的赋值
    //                GameObject btn_AreaLable = (GameObject) Instantiate (Button_AreaLableEX);
    //                btn_AreaLable.transform.localPosition = Vector3.zero;
    //                btn_AreaLable.transform.parent = GameObject_AreaLablePos.transform;
    //                btn_AreaLable.transform.localScale = Vector3.one;
    //                btn_AreaLable.name = id;
    //                GameObject_AreaLablePos.GetComponent<UIGrid> ().enabled = true;
    //                List_AreaLable.Add (btn_AreaLable);
    //                foreach (Transform child in btn_AreaLable.GetComponentsInChildren<Transform> ())
    //                {
    //                    if (child.name == "Sprite_ALForce")
    //                    {
    //                        child.GetComponentInChildren<UISprite> ().color = colorValue;
    //                    }
    //                    if (child.name == "Label_ALForce")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = forceName;
    //                    }
    //                    if (child.name == "Label_AreaLableDes")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = shengName;
    //                    }
    //                }
    //                UIEventListener.Get (btn_AreaLable).onClick = AreaLableClick;
    //                UIEventListener.Get (btn_AreaLable).onHover = AreaLableHover;


    //                //府的赋值
    //                GameObject btn_fu = (GameObject) Instantiate (Button_FuAreaEX);
    //                btn_fu.transform.localPosition = Vector3.zero;
    //                btn_fu.transform.parent = GameObject_AreaPos.transform;
    //                btn_fu.name = id;
    //                btn_fu.transform.localScale = Vector3.one;
    //                float posX = SD_Drama_1.Class_Dic [id]._posX ();
    //                float posY = SD_Drama_1.Class_Dic [id]._posY ();
    //                float posZ = SD_Drama_1.Class_Dic [id]._posZ ();
    //                btn_fu.transform.localPosition = new Vector3 (posX, posY, posZ);
    //                List_Fu.Add (btn_fu);
    //                UIEventListener.Get (btn_fu).onHover = AreaLableHover;
    //                UIEventListener.Get (btn_fu).onClick = AreaLableClick;
    //                //各府赋值
    //                foreach (Transform child in btn_fu.GetComponentsInChildren<Transform> ())
    //                {
    //                    if (child.name == "GameObject_Info")
    //                    {
    //                        int info_x = int.Parse (dic.Value.value7);
    //                        int info_y = int.Parse (dic.Value.value8);
    //                        child.localPosition = new Vector3 (info_x, info_y, 0);
    //                        btn_fu.GetComponent<BoxCollider> ().center = new Vector3 (info_x, info_y, 0);
    //                    }
    //                    if (child.name == "Sprite_Force")
    //                    {
    //                        child.GetComponentInChildren<UISprite> ().color = colorValue;
    //                    }
    //                    if (child.name == "Label_Force")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = forceName;
    //                    }
    //                    if (child.name == "Label_FuName")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = shengName;
    //                    }
    //                    if (child.name == "Texture_FuMap")
    //                    {
    //                        string path = Data_Static.MapPic_Area + "Area" + ClickAreaID + "/" + SD_Map_Area.Class_Dic [dic.Value.modelID].mapImg;
    //                        child.GetComponentInChildren<UITexture> ().mainTexture = Resources.Load<Texture> (path);
    //                        int sizeX = int.Parse (dic.Value.value4);
    //                        int sizeY = int.Parse (dic.Value.value5);
    //                        child.GetComponentInChildren<UITexture> ().width = sizeX;
    //                        child.GetComponentInChildren<UITexture> ().height = sizeY;
    //                    }
    //                    if (child.name == "Texture_FuUp")
    //                    {
    //                        string path = Data_Static.MapPic_Area + "Area" + ClickAreaID + "/" + SD_Map_Area.Class_Dic [dic.Value.modelID].mapImg.Replace ("Area", "Choose");
    //                        child.GetComponentInChildren<UITexture> ().mainTexture = Resources.Load<Texture> (path);
    //                        int sizeX = int.Parse (dic.Value.value4);
    //                        int sizeY = int.Parse (dic.Value.value5);
    //                        child.GetComponentInChildren<UITexture> ().width = sizeX;
    //                        child.GetComponentInChildren<UITexture> ().height = sizeY;
    //                        child.name = id;
    //                        List_AreaTexture.Add (child.gameObject);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    //各府地图
    //    else if (areaState == AreaState.xian)
    //    {
    //        GameObject_AreaPos.transform.localPosition = new Vector3 (250, -175, 0);

    //        Button_XianAreaEX.SetActive (true);
    //        Button_AreaLableEX.SetActive (true);
    //        //遍历剧本1表
    //        foreach (KeyValuePair<string, Class_Drama_1> dic in SD_Drama_1.Class_Dic)
    //        {
    //            //类型为2，场景表中场景等级为4，上级地图为点击区域ID的场景
    //            if (dic.Value.value6 == "1" && SD_Map_Area.Class_Dic [dic.Value.modelID].areaLv == "4" && SD_Map_Area.Class_Dic [dic.Value.modelID].UpArea == SD_Drama_1.Class_Dic [ClickAreaID].modelID)
    //            {
    //                string id = dic.Key;
    //                string forceID = SD_Drama_1.Class_Dic [id].value2;
    //                string forceName = SD_Force.Class_Dic [forceID].flag;
    //                string shengName = SD_Drama_1.Class_Dic [id].name;

    //                int r = SD_Force.Class_Dic [forceID]._colorR ();
    //                int g = SD_Force.Class_Dic [forceID]._colorG ();
    //                int b = SD_Force.Class_Dic [forceID]._colorB ();
    //                int a = SD_Force.Class_Dic [forceID]._colorA ();
    //                Color colorValue = FindObjectOfType<GameObject_Static> ().ColorDeal (r, g, b, a);


    //                //地区按钮的赋值
    //                GameObject btn_AreaLable = (GameObject) Instantiate (Button_AreaLableEX);
    //                btn_AreaLable.transform.localPosition = Vector3.zero;
    //                btn_AreaLable.transform.parent = GameObject_AreaLablePos.transform;
    //                btn_AreaLable.transform.localScale = Vector3.one;
    //                btn_AreaLable.name = id;
    //                List_AreaLable.Add (btn_AreaLable);
    //                GameObject_AreaLablePos.GetComponent<UIGrid> ().enabled = true;

    //                foreach (Transform child in btn_AreaLable.GetComponentsInChildren<Transform> ())
    //                {
    //                    if (child.name == "Sprite_ALForce")
    //                    {
    //                        child.GetComponentInChildren<UISprite> ().color = colorValue;
    //                    }
    //                    if (child.name == "Label_ALForce")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = forceName;
    //                    }
    //                    if (child.name == "Label_AreaLableDes")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = shengName;
    //                    }
    //                }
    //                UIEventListener.Get (btn_AreaLable).onClick = AreaLableClick;


    //                //县的赋值
    //                GameObject btn_Xian = (GameObject) Instantiate (Button_XianAreaEX);
    //                btn_Xian.transform.localPosition = Vector3.zero;
    //                btn_Xian.transform.parent = GameObject_AreaPos.transform;
    //                btn_Xian.name = id;
    //                btn_Xian.transform.localScale = Vector3.one;
    //                float posX = SD_Drama_1.Class_Dic [id]._posX ();
    //                float posY = SD_Drama_1.Class_Dic [id]._posY ();
    //                float posZ = SD_Drama_1.Class_Dic [id]._posZ ();
    //                btn_Xian.transform.localPosition = new Vector3 (posX, posY, posZ);
    //                List_Xian.Add (btn_Xian);
    //                UIEventListener.Get (btn_Xian).onClick = AreaLableClick;
    //                //各县赋值
    //                foreach (Transform child in btn_Xian.GetComponentsInChildren<Transform> ())
    //                {
    //                    if (child.name == "Sprite_Force")
    //                    {
    //                        child.GetComponentInChildren<UISprite> ().color = colorValue;
    //                    }
    //                    if (child.name == "Label_Force")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = forceName;
    //                    }
    //                    if (child.name == "Label_ShengName")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = shengName;
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    foreach (GameObject item in List_AreaTexture)
    //    {
    //        item.SetActive (false);
    //    }

    //    Button_ShengAreaEX.SetActive (false);
    //    Button_FuAreaEX.SetActive (false);
    //    Button_XianAreaEX.SetActive (false);
    //    Button_AreaLableEX.SetActive (false);
    //}

    //void AreaLableHover (GameObject btn, bool isOver)
    //{
    //    if (isOver)
    //    {
    //        foreach (GameObject item in List_AreaTexture)
    //        {
    //            if (item.name == btn.name)
    //            {
    //                item.SetActive (true);
    //            }
    //            else
    //            {
    //                item.SetActive (false);
    //            }
    //        }
    //    }
    //    //else
    //    //{
    //    //    foreach (GameObject item in List_AreaTexture)
    //    //    {
    //    //        item.SetActive(false);
    //    //    }
    //    //}
    //}

    ////点击地区菜单按钮
    //void AreaLableClick (GameObject btn)
    //{
    //    foreach (GameObject item in List_AreaTexture)
    //    {
    //        if (item.name == btn.name)
    //        {
    //            item.SetActive (true);
    //        }
    //        else
    //        {
    //            item.SetActive (false);
    //        }
    //    }
    //    float scaleX = GameObject_AreaPos.transform.localScale.x;
    //    float scaleY = GameObject_AreaPos.transform.localScale.y;
    //    float scaleZ = GameObject_AreaPos.transform.localScale.z;
    //    string id = btn.name;
    //    ClickAreaID = id;
    //    float posX = SD_Drama_1.Class_Dic [id]._posX ();
    //    float posY = SD_Drama_1.Class_Dic [id]._posY ();
    //    float posZ = SD_Drama_1.Class_Dic [id]._posZ ();
    //    Vector3 nowPos = GameObject_AreaPos.transform.localPosition;
    //    Vector3 TargetPos = new Vector3 (posX * scaleX, posY * scaleY, posZ * scaleZ) * -1;
    //    GameObject_AreaPos.GetComponent<TweenPosition> ().from = nowPos;
    //    GameObject_AreaPos.GetComponent<TweenPosition> ().to = TargetPos;
    //    GameObject_AreaPos.GetComponent<TweenPosition> ().duration = 1f;

    //    GameObject_AreaPos.GetComponent<TweenPosition> ().enabled = true;
    //    GameObject_AreaPos.GetComponent<TweenPosition> ().ResetToBeginning ();
    //    GameObject_AreaPos.GetComponent<TweenPosition> ().PlayForward ();

    //    AreaButtonClick (btn);
    //}

    ////初始地图大小和位置
    //void seeAll (GameObject btn)
    //{
    //    //PosScale = new Vector3(0.13f, 0.13f, 0.13f);
    //    //GameObject_AreaPos.transform.localPosition = new Vector3(-5500 * 0.13f, 1800 * 0.13f, 0);
    //}

    ////点击地区按钮
    //void AreaButtonClick (GameObject btn)
    //{
    //    setAreaTips (btn.name);
    //}

    ////地区说明赋值
    //void setAreaTips (string ID)
    //{
    //    string areaID = SD_Drama_1.Class_Dic [ID].modelID;

    //    GameObject_AreaTips.SetActive (true);
    //    GameObject_AreaTips.GetComponent<TweenPosition> ().enabled = true;
    //    GameObject_AreaTips.GetComponent<TweenPosition> ().ResetToBeginning ();
    //    GameObject_AreaTips.GetComponent<TweenPosition> ().PlayForward ();

    //    Button_SeeInfo.name = areaID;
    //    Button_SeeBack.name = areaID;
    //    if (SD_Drama_1.Class_Dic [ID].value3 == "1")
    //    {
    //        Button_SeeInfo.GetComponent<UIButton> ().SetState (UIButtonColor.State.Normal, true);
    //        Button_SeeInfo.GetComponent<BoxCollider> ().enabled = true;
    //        UIEventListener.Get (Button_SeeInfo).onClick = AreaInfoEffect;
    //    }
    //    else
    //    {
    //        Button_SeeInfo.GetComponent<UIButton> ().SetState (UIButtonColor.State.Disabled, true);
    //        Button_SeeInfo.GetComponent<BoxCollider> ().enabled = false;
    //    }
    //    UIEventListener.Get (Button_SeeBack).onClick = AreaBackEffect;

    //    string areaName = SD_Map_Area.Class_Dic [areaID].areaName;
    //    string adminName = SD_Map_Area.Class_Dic [areaID].adminName;
    //    string Tips_Capital = SD_Map_Area.Class_Dic [areaID].center;
    //    string AreaDes = SD_Map_Area.Class_Dic [areaID].des;
    //    string areaLv = SD_Map_Area.Class_Dic [areaID].areaLv;

    //    Label_AreaName.text = areaName;
    //    Label_adminName.text = adminName;
    //    Label_Tips_Capital.text = Tips_Capital;
    //    Label_AreaDes.text = AreaDes;

    //    Sprite_adminName.GetComponent<UISprite> ().atlas = FindObjectOfType<GameObject_Static> ().Atlas_Map;
    //    Sprite_Tips_Capital.GetComponent<UISprite> ().atlas = FindObjectOfType<GameObject_Static> ().Atlas_Map;

    //    if (areaLv == "2")//地区-省
    //    {
    //        Button_SeeBack.SetActive (false);
    //        Button_SeeInfo.GetComponentInChildren<UILabel> ().text = "放大舆图";
    //        Sprite_adminName.GetComponent<UISprite> ().spriteName = Data_Static.Atlas_Map_map_QiZhi1;
    //        Sprite_Tips_Capital.GetComponent<UISprite> ().spriteName = Data_Static.Atlas_Map_Map_City3;
    //    }
    //    else if (areaLv == "3")//府
    //    {
    //        Button_SeeBack.SetActive (true);
    //        Button_SeeInfo.GetComponentInChildren<UILabel> ().text = "放大舆图";
    //        Button_SeeBack.GetComponentInChildren<UILabel> ().text = "缩小舆图";
    //        Sprite_adminName.GetComponent<UISprite> ().spriteName = Data_Static.Atlas_Map_map_QiZhi1;
    //        Sprite_Tips_Capital.GetComponent<UISprite> ().spriteName = Data_Static.Atlas_Map_Map_City1;
    //    }
    //    else if (areaLv == "4")//县
    //    {
    //        Button_SeeBack.SetActive (true);
    //        Button_SeeInfo.GetComponentInChildren<UILabel> ().text = "打开县志";
    //        Button_SeeBack.GetComponentInChildren<UILabel> ().text = "缩小舆图";
    //        Sprite_adminName.GetComponent<UISprite> ().spriteName = Data_Static.Atlas_Map_map_QiZhi1;
    //        Sprite_Tips_Capital.GetComponent<UISprite> ().spriteName = Data_Static.Atlas_Map_Map_City1;
    //    }

    //}

    ////地域详情——查看详情
    //void AreaInfoEffect (GameObject btn)
    //{
    //    closeTips (btn);
    //    GameObject_Effect.SetActive (true);
    //    GameObject_left.enabled = true;
    //    GameObject_Right.enabled = true;
    //    GameObject_left.PlayForward ();
    //    GameObject_Right.PlayForward ();


    //    string id = btn.name;
    //    StartCoroutine (AreaInfo (id));
    //}

    //IEnumerator AreaInfo (string areaID)
    //{
    //    yield return new WaitForSeconds (GameObject_Right.duration);

    //    GameObject_left.enabled = true;
    //    GameObject_Right.enabled = true;
    //    GameObject_left.PlayReverse ();
    //    GameObject_Right.PlayReverse ();

    //    //点击某省的查看详情的按钮后
    //    if (SD_Map_Area.Class_Dic [areaID].areaLv == "2")
    //    {
    //        foreach (GameObject item in List_Sheng)
    //        {
    //            Destroy (item);
    //        }
    //        foreach (GameObject item in List_AreaLable)
    //        {
    //            Destroy (item);
    //        }
    //        foreach (GameObject item in List_AreaTexture)
    //        {
    //            Destroy (item);
    //        }
    //        List_Sheng.Clear ();
    //        List_AreaLable.Clear ();
    //        List_AreaTexture.Clear ();

    //        areaState = AreaState.fu;
    //        CloneArea (DramaID);
    //    }
    //    //点击某府的查看详情的按钮后
    //    else if (SD_Map_Area.Class_Dic [areaID].areaLv == "3")
    //    {
    //        foreach (GameObject item in List_Fu)
    //        {
    //            Destroy (item);
    //        }
    //        foreach (GameObject item in List_AreaLable)
    //        {
    //            Destroy (item);
    //        }
    //        foreach (GameObject item in List_AreaTexture)
    //        {
    //            Destroy (item);
    //        }
    //        List_Fu.Clear ();
    //        List_AreaLable.Clear ();
    //        List_AreaTexture.Clear ();

    //        areaState = AreaState.xian;
    //        CloneArea (DramaID);
    //    }
    //    //点击某县的查看详情的按钮后
    //    else if (SD_Map_Area.Class_Dic [areaID].areaLv == "4")
    //    {
    //        canMove = false;
    //        windowState = WindowState.chengWindow;
    //        XianZhi (areaID);
    //    }
    //    GameObject_Effect.SetActive (false);
    //}


    ////地域详情——返回上级
    //void AreaBackEffect (GameObject btn)
    //{
    //    closeTips (btn);
    //    GameObject_Effect.SetActive (true);
    //    GameObject_left.enabled = true;
    //    GameObject_Right.enabled = true;
    //    GameObject_left.PlayForward ();
    //    GameObject_Right.PlayForward ();


    //    string id = btn.name;
    //    StartCoroutine (AreaBack (id));
    //}
    //IEnumerator AreaBack (string areaID)
    //{
    //    yield return new WaitForSeconds (GameObject_Right.duration);

    //    GameObject_left.enabled = true;
    //    GameObject_Right.enabled = true;
    //    GameObject_left.PlayReverse ();
    //    GameObject_Right.PlayReverse ();

    //    //点击某县的返回上级的按钮后
    //    if (SD_Map_Area.Class_Dic [areaID].areaLv == "4")
    //    {
    //        foreach (GameObject item in List_Xian)
    //        {
    //            Destroy (item);
    //        }
    //        foreach (GameObject item in List_AreaLable)
    //        {
    //            Destroy (item);
    //        }
    //        foreach (GameObject item in List_AreaTexture)
    //        {
    //            Destroy (item);
    //        }
    //        List_Xian.Clear ();
    //        List_AreaLable.Clear ();
    //        List_AreaTexture.Clear ();
    //        ClickAreaID = SD_Map_Area.Class_Dic [SD_Map_Area.Class_Dic [areaID].UpArea].UpArea;
    //        areaState = AreaState.fu;
    //        CloneArea (DramaID);
    //    }
    //    //点击某府的返回上级的按钮后
    //    else if (SD_Map_Area.Class_Dic [areaID].areaLv == "3")
    //    {
    //        foreach (GameObject item in List_Fu)
    //        {
    //            Destroy (item);
    //        }
    //        foreach (GameObject item in List_AreaLable)
    //        {
    //            Destroy (item);
    //        }
    //        foreach (GameObject item in List_AreaTexture)
    //        {
    //            Destroy (item);
    //        }
    //        List_Fu.Clear ();
    //        List_AreaLable.Clear ();
    //        List_AreaTexture.Clear ();
    //        areaState = AreaState.sheng;
    //        CloneArea (DramaID);
    //    }
    //    GameObject_Effect.SetActive (false);
    //}

    ////界面状态
    //void XianZhi (string areaID)
    //{

    //    Button_Enter.GetComponent<BoxCollider> ().enabled = false;
    //    Button_Enter.GetComponent<UIButton> ().SetState (UIButtonColor.State.Disabled, true);
    //    //城池图界面
    //    if (windowState == WindowState.chengWindow)
    //    {
    //        foreach (GameObject item in PersonList)
    //        {
    //            Destroy (item);
    //        }
    //        PersonList.Clear ();
    //        choosePersonList.Clear ();

    //        GameObject_AMScroll.SetActive (true);
    //        Sprite_BG_AMS.SetActive (true);
    //        GameObject_PersonScroll.SetActive (false);
    //        Sprite_BG_PS.SetActive (false);
    //        GameObject_AreaMap.SetActive (true);
    //        GameObject_PersonInfo.SetActive (false);
    //        Label_TitleTips.gameObject.SetActive (false);
    //        Button_CreatPerson.SetActive (false);
    //        Button_PersonEX.SetActive (false);
    //        Button_ToMap.SetActive (true);
    //        Button_To5Or1.SetActive (true);
    //        Texture_CityMapEX.SetActive (true);

    //        Label_TitleMain.text = SD_Map_Area.Class_Dic [areaID].areaName + "志第一卷·地理志";
    //        Label_TitleSign.text = "图经·城池";
    //        Label_TitleDes.text = SD_WordTranslate.Class_Dic ["20002"].chinese;

    //        Label_ToMap.text = "山川地理图";
    //        Label_To5Or1.text = "第五卷·人物志";

    //        string path_cityMap = Data_Static.MapPic_Plane + SD_Map_Area.Class_Dic [areaID].cityImg;
    //        Texture_CityMapEX.GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> (path_cityMap);
    //        Texture_CityMapEX.transform.parent = GameObject_CityMapPos.transform;
    //        Texture_CityMapEX.transform.localScale = Vector3.one;
    //        Texture_CityMapEX.transform.localPosition = Vector3.zero;
    //        GameObject_CityMapPos.transform.localPosition = Vector3.zero;

    //        Button_ToMap.name = areaID;
    //        Button_To5Or1.name = areaID;

    //    }
    //    //山川地理图界面
    //    else if (windowState == WindowState.shanWindow)
    //    {
    //        foreach (GameObject item in PersonList)
    //        {
    //            Destroy (item);
    //        }
    //        PersonList.Clear ();
    //        choosePersonList.Clear ();

    //        GameObject_AMScroll.SetActive (true);
    //        Sprite_BG_AMS.SetActive (true);
    //        GameObject_PersonScroll.SetActive (false);
    //        Sprite_BG_PS.SetActive (false);
    //        GameObject_AreaMap.SetActive (true);
    //        GameObject_PersonInfo.SetActive (false);
    //        Label_TitleTips.gameObject.SetActive (false);
    //        Button_CreatPerson.SetActive (false);
    //        Button_PersonEX.SetActive (false);
    //        Button_ToMap.SetActive (true);
    //        Button_To5Or1.SetActive (false);
    //        Texture_CityMapEX.SetActive (true);

    //        Label_TitleMain.text = SD_Map_Area.Class_Dic [areaID].areaName + "志第一卷·地理志";
    //        Label_TitleSign.text = "山川地理图";
    //        Label_TitleDes.text = SD_Map_Area.Class_Dic [areaID].geoDes;

    //        Label_ToMap.text = "图经·城池";
    //        Label_To5Or1.text = "第五卷·人物志";

    //        string path_HillMap = Data_Static.MapPic_Plane + SD_Map_Area.Class_Dic [areaID].hillImg;
    //        Texture_CityMapEX.GetComponent<UITexture> ().mainTexture = Resources.Load<Texture> (path_HillMap);
    //        Texture_CityMapEX.transform.parent = GameObject_CityMapPos.transform;
    //        Texture_CityMapEX.transform.localScale = Vector3.one;
    //        Texture_CityMapEX.transform.localPosition = Vector3.zero;
    //        GameObject_CityMapPos.transform.localPosition = Vector3.zero;

    //        Button_ToMap.name = areaID;
    //        Button_To5Or1.name = areaID;
    //    }
    //    //人物志界面
    //    else if (windowState == WindowState.RoleWindow)
    //    {
    //        foreach (GameObject item in PersonList)
    //        {
    //            Destroy (item);
    //        }
    //        PersonList.Clear ();
    //        choosePersonList.Clear ();

    //        GameObject_AMScroll.SetActive (false);
    //        Sprite_BG_AMS.SetActive (false);
    //        GameObject_PersonScroll.SetActive (true);
    //        Sprite_BG_PS.SetActive (true);
    //        GameObject_AreaMap.SetActive (true);
    //        GameObject_PersonInfo.SetActive (false);
    //        Label_TitleTips.gameObject.SetActive (true);
    //        Button_CreatPerson.SetActive (true);
    //        Button_PersonEX.SetActive (true);
    //        Button_ToMap.SetActive (false);
    //        Button_To5Or1.SetActive (true);
    //        Texture_CityMapEX.SetActive (false);

    //        Label_TitleTips.text = "注：游戏内地方志人物志之人物列表与真实典籍记载人物志内容有异，仅包含玩家可选人物。";
    //        Label_TitleMain.text = SD_Map_Area.Class_Dic [areaID].areaName + "志第五卷·人物志";
    //        Label_TitleSign.text = "可选人物列表";
    //        Label_TitleDes.text = "志曰：人物之生，地之灵也，国之光也。九州定鼎，成天华风气萃交之地，是以雄杰不群、高行奇节、著勋名而显天下者，多产于其间，故志人物。 ";

    //        Label_ToMap.text = "图经·城池";
    //        Label_To5Or1.text = "第一卷·地理志";

    //        //GameObject_PerosonPos.transform.localPosition = Vector3.zero;
    //        int counts = -1;
    //        foreach (KeyValuePair<string, Class_Drama_1> dic in SD_Drama_1.Class_Dic)
    //        {
    //            if (dic.Value.type == "5" && dic.Value.value7 == areaID && dic.Value.value8 == "1")
    //            {
    //                counts++;
    //                //选人按钮的赋值
    //                GameObject btn_Person = (GameObject) Instantiate (Button_PersonEX);
    //                btn_Person.transform.parent = GameObject_PerosonPos.transform;
    //                btn_Person.transform.localScale = Vector3.one;
    //                btn_Person.transform.localPosition = new Vector3 (0 - counts * 120, 0, 0);
    //                //GameObject_PerosonPos.GetComponent<UIGrid>().enabled = true;
    //                PersonList.Add (btn_Person);

    //                btn_Person.name = dic.Key;
    //                UIEventListener.Get (btn_Person).onClick = PersonClick;

    //                foreach (Transform child in btn_Person.GetComponentsInChildren<Transform> ())
    //                {
    //                    if (child.name == "Label_PersonName")
    //                    {
    //                        child.GetComponentInChildren<UILabel> ().text = dic.Value.name;
    //                    }
    //                    if (child.name == "Sprite_PersonIcon")
    //                    {
    //                        string roleID = dic.Value.modelID;
    //                        child.GetComponentInChildren<UISprite> ().spriteName = SD_Role_Main.Class_Dic [roleID].headIcon;
    //                    }
    //                    if (child.name == "Label_PersonSign")
    //                    {
    //                        string id = dic.Value.value2;
    //                        child.GetComponentInChildren<UILabel> ().text = SD_Role_Identity.Class_Dic [id].identity;
    //                    }
    //                    if (child.name == "Button_Info")
    //                    {
    //                        child.name = dic.Key;
    //                        UIEventListener.Get (child.gameObject).onClick = PersonInfoClick;
    //                    }
    //                    if (child.name == "Sprite_Choose")
    //                    {
    //                        child.gameObject.GetComponent<UISprite> ().enabled = false;
    //                        choosePersonList.Add (child.gameObject);
    //                    }
    //                }
    //            }
    //        }
    //        Button_PersonEX.SetActive (false);
    //    }
    //    UIEventListener.Get (Button_backMainMap).onClick = BackMap;
    //    UIEventListener.Get (Button_ToMap).onClick = ToMapClick;
    //    UIEventListener.Get (Button_To5Or1).onClick = To5Or1Click;
    //}

    ////地图互相切换按钮
    //void ToMapClick (GameObject btn)
    //{
    //    if (windowState == WindowState.chengWindow)
    //    {
    //        windowState = WindowState.shanWindow;
    //    }
    //    else if (windowState == WindowState.shanWindow)
    //    {
    //        windowState = WindowState.chengWindow;
    //    }
    //    XianZhi (btn.name);
    //}
    ////地图角色互相切换按钮
    //void To5Or1Click (GameObject btn)
    //{
    //    if (windowState == WindowState.RoleWindow)
    //    {
    //        windowState = WindowState.chengWindow;
    //    }
    //    else if (windowState == WindowState.chengWindow)
    //    {
    //        windowState = WindowState.RoleWindow;
    //    }
    //    else if (windowState == WindowState.chengWindow)
    //    {
    //        windowState = WindowState.RoleWindow;
    //    }
    //    XianZhi (btn.name);
    //}

    ////返回地图
    //void BackMap (GameObject btn)
    //{
    //    canMove = true;
    //    GameObject_AreaMap.SetActive (false);
    //}

    ////角色信息界面
    //void PersonInfoClick (GameObject btn)
    //{
    //    Lable_StoryEX.gameObject.SetActive (true);
    //    GameObject_PersonInfo.SetActive (true);
    //    Button_backMainMap.SetActive (false);
    //    string d_id = btn.name;
    //    string r_id = SD_Drama_1.Class_Dic [d_id].modelID;

    //    Label_PTitleName.text = SD_Role_Main.Class_Dic [r_id].commonName + "传";
    //    Label_HistoryTime.text = "本朝";
    //    Label_PersonCommonName.text = SD_Role_Main.Class_Dic [r_id].commonName;
    //    Label_PersonWordName.text = SD_Role_Main.Class_Dic [r_id].wordName;
    //    Label_PersonTitleName.text = SD_Role_Main.Class_Dic [r_id].titleName;
    //    Label_birthday.text = "万历" + (18 - (1590 - SD_Role_Main.Class_Dic [r_id]._brithday ())) + "年";
    //    Label_place.text = SD_Role_Main.Class_Dic [r_id].place + "人";

    //    //Texture_Role
    //    Labe_TiZhi.text = SD_Role_Main.Class_Dic [r_id].TiZhi;
    //    Labe_XingDong.text = SD_Role_Main.Class_Dic [r_id].XingDong;
    //    Labe_WuLi.text = SD_Role_Main.Class_Dic [r_id].WuLi;
    //    Labe_NaiLi.text = SD_Role_Main.Class_Dic [r_id].NaiLi;
    //    Labe_PoLi.text = SD_Role_Main.Class_Dic [r_id].PoLi;
    //    Labe_YiLi.text = SD_Role_Main.Class_Dic [r_id].YiLi;
    //    Labe_MouLue.text = SD_Role_Main.Class_Dic [r_id].MouLue;
    //    Labe_YingBian.text = SD_Role_Main.Class_Dic [r_id].YingBian;
    //    Lable_gender.text = SD_Role_Main.Class_Dic [r_id].gender;
    //    Lable_nature.text = SD_Role_Main.Class_Dic [r_id].nature;
    //    Lable_national.text = SD_Role_Main.Class_Dic [r_id].national;
    //    Lable_force.text = SD_Drama_1.Class_Dic [d_id].value1;
    //    Label_birthday.text = SD_Role_Main.Class_Dic [r_id].brithday;

    //    //GameObject_StoryPos
    //    byte [] story_byte = System.Text.Encoding.UTF8.GetBytes (SD_Role_Main.Class_Dic [r_id].story);
    //    string story_word = System.Text.Encoding.UTF8.GetString (story_byte);
    //    int line_LableCount = 15;
    //    int LineCount = Mathf.CeilToInt (story_word.Length / line_LableCount);
    //    for (int i = 0; i < LineCount + 1; i++)
    //    {
    //        string text = "";
    //        if (i == LineCount)
    //        {
    //            text = story_word.Substring (i * line_LableCount);
    //        }
    //        else
    //        {
    //            text = story_word.Substring (i * line_LableCount, line_LableCount);
    //        }
    //        UILabel ls = Instantiate<UILabel> (Lable_StoryEX);
    //        ls.text = text;
    //        ls.gameObject.transform.parent = GameObject_StoryPos.transform;
    //        ls.gameObject.transform.localScale = Vector3.one;
    //        GameObject_StoryPos.GetComponent<UIGrid> ().enabled = true;
    //    }

    //    Lable_StoryEX.gameObject.SetActive (false);

    //    UIEventListener.Get (Button_Back).onClick = BackPerson;
    //}

    ////选中人物
    //void PersonClick (GameObject btn)
    //{
    //    Button_Enter.name = btn.name;
    //    foreach (Transform child in btn.GetComponentsInChildren<Transform> ())
    //    {
    //        if (child.name == "Sprite_Choose")
    //        {
    //            if (child.gameObject.GetComponent<UISprite> ().isActiveAndEnabled == false)
    //            {
    //                foreach (GameObject go in choosePersonList)
    //                {
    //                    go.GetComponent<UISprite> ().enabled = false;
    //                }
    //                child.gameObject.GetComponent<UISprite> ().enabled = true;
    //                Button_Enter.GetComponent<BoxCollider> ().enabled = true;
    //                Button_Enter.GetComponent<UIButton> ().SetState (UIButtonColor.State.Normal, true);
    //                UIEventListener.Get (Button_Enter).onClick = EnterDrama;
    //            }
    //        }
    //    }
    //    Button_Enter.GetComponent<BoxCollider> ().enabled = true;

    //    UIEventListener.Get (Button_Enter).onClick = EnterDrama;
    //}

    ////进入剧本
    //void EnterDrama (GameObject btn)
    //{
    //    //临时变量重置
    //    ShengCount.Clear ();
    //    XianCount.Clear ();
    //    RoleCount.Clear ();
    //    List_Sheng.Clear ();
    //    List_Fu.Clear ();
    //    List_Xian.Clear ();
    //    List_AreaLable.Clear ();
    //    List_AreaTexture.Clear ();
    //    choosePersonList.Clear ();


    //    //FindObjectOfType<GameStart>().Drama_RoleID = btn.name;
    //    StartCoroutine (CommonFunc.GetInstance.ToSceneLoading (Data_Static.SceneName_TheWorld));
    //}

    ////关闭角色详情窗口
    //void BackPerson (GameObject btn)
    //{
    //    NGUITools.DestroyChildren (GameObject_StoryPos.transform);
    //    Button_backMainMap.SetActive (true);
    //    GameObject_PersonInfo.SetActive (false);
    //}

    ////隐藏地区说明
    //void closeTips (GameObject btn)
    //{
    //    GameObject_AreaTips.SetActive (true);
    //    GameObject_AreaTips.GetComponent<TweenPosition> ().enabled = true;
    //    GameObject_AreaTips.GetComponent<TweenPosition> ().PlayReverse ();
    //}

    ////回主菜单
    //void BackMainMenu (GameObject btn)
    //{
    //    ShengCount.Clear ();
    //    XianCount.Clear ();
    //    RoleCount.Clear ();
    //    StartCoroutine (CommonFunc.GetInstance.ToSceneLoading (Data_Static.SceneName_Main_Menu));
    //}

    ////故事大概
    //void StoryDes (GameObject btn)
    //{
    //    Transform SD = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_DesTips, FindObjectOfType<UIRoot> ().transform, Vector3.one, Vector3.zero);
    //    string title = "故事纪要";
    //    string Lable = SD_Drama_Main.Class_Dic [DramaID].Story;
    //    SD.GetComponent<DesTipsUI> ().setDes (title, Lable);
    //}

    ////历史大概
    //void BigDes (GameObject btn)
    //{
    //    Transform BD = CommonFunc.GetInstance.UI_Instantiate (Data_Static.UIpath_DesTips, FindObjectOfType<UIRoot> ().transform, Vector3.one, Vector3.zero);
    //    string title = "史实纪要";
    //    string Lable = SD_Drama_Main.Class_Dic [DramaID].History;
    //    BD.GetComponent<DesTipsUI> ().setDes (title, Lable);
    //}

    //void ClickControl ()
    //{
    //    UIEventListener.Get (Button_CloseTips).onClick = closeTips;
    //    UIEventListener.Get (Button_seeAll).onClick = seeAll;
    //    UIEventListener.Get (Button_backMainMenu).onClick = BackMainMenu;
    //    UIEventListener.Get (Button_StoryDes).onClick = StoryDes;
    //    UIEventListener.Get (Button_BigDes).onClick = BigDes;
    //}
}
