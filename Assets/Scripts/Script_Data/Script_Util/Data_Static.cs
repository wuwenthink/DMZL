// ====================================================================================== 
// 文件名         ：    Data_Static.cs                                                         
// 版本号         ：    v1.0.0.0                                                 
// 作者           ：    wuwenthink
// 修改人         ：    wuwenthink                                                          
// 创建日期       ：                                                                      
// 最后修改日期   ：    2017-10-10 15:51:09                                                          
// ====================================================================================== 
// 功能描述       ：                                                                    
// ======================================================================================

using UnityEngine;

public static class Data_Static
{
    //存档路径
    public static string save_pathMain = Application.dataPath + "/Resources/SaveData/";
    public static string save_pathScene_Main = Application.dataPath + "/Resources/SaveData/SceneMain/";
    public static string save_pathScene_Data = Application.dataPath + "/Resources/SaveData/SceneData/";
    public static string save_pathScene_Grid = Application.dataPath + "/Resources/SaveData/SceneGrid/";
    public static string save_DataName = "/SaveData.json";

    //Resources读取路径
    public static string Res_pathScene_Main = "SaveData/SceneMain/";
    public static string Res_pathScene_Data = "SaveData/SceneData/";
    public static string Res_pathScene_Grid = "SaveData/SceneGrid/";

    //各UI界面prefab路径
    //BUSINESS
    public static string UIpath_Account = "Prefab/Prefab_UI/UI_Business/UI_Account";
    public static string UIpath_BoothDes = "Prefab/Prefab_UI/UI_Business/UI_BoothDes";
    public static string UIpath_BuyMenu = "Prefab/Prefab_UI/UI_Business/UI_BuyMenu";
    public static string UIpath_Deal = "Prefab/Prefab_UI/UI_Business/UI_Deal";
    public static string UIpath_DealChoose = "Prefab/Prefab_UI/UI_Business/UI_DealChoose";
    public static string UIpath_Haggle = "Prefab/Prefab_UI/UI_Business/UI_Haggle";
    public static string UIpath_MarketInfo = "Prefab/Prefab_UI/UI_Business/UI_MarketInfo";
    public static string UIpath_SellTable = "Prefab/Prefab_UI/UI_Business/UI_SellTable";
    public static string UIpath_SupplyAgreement = "Prefab/Prefab_UI/UI_Business/UI_SupplyAgreement";
    public static string UIpath_Warehouse = "Prefab/Prefab_UI/UI_Business/UI_Warehouse";
    //SYSTEM
    public static string UIpath_Role = "Prefab/Prefab_UI/UI_System/UI_Role";
    public static string UIpath_Bag = "Prefab/Prefab_UI/UI_System/UI_Bag";
    public static string UIpath_Making = "Prefab/Prefab_UI/UI_System/UI_MakeRecipe";
    public static string UIpath_SystemMenu = "Prefab/Prefab_UI/UI_System/UI_SystemMenu";
    public static string UIpath_Shop = "Prefab/Prefab_UI/UI_System/UI_Shop";
    public static string UIpath_SceneLoading = "Prefab/Prefab_UI/UI_System/UI_SceneLoading";
    public static string UIpath_News = "Prefab/Prefab_UI/UI_System/UI_News";
    public static string UIpath_Task = "Prefab/Prefab_UI/UI_System/UI_Task";
    public static string UIpath_IdentityAll = "Prefab/Prefab_UI/UI_System/UI_IdentityAll";
    public static string UIpath_LoadGame = "Prefab/Prefab_UI/UI_System/UI_LoadGame";
    //TIPS
    public static string UIpath_DesTips = "Prefab/Prefab_UI/UI_Tips/UI_DesTips";
    public static string UIpath_ItemTips = "Prefab/Prefab_UI/UI_Tips/UI_ItemTips";
    public static string UIpath_LableTips = "Prefab/Prefab_UI/UI_Tips/UI_LableTips";
    public static string UIpath_NameTips = "Prefab/Prefab_UI/UI_Tips/UI_NameTips";
    public static string UIpath_NearTips = "Prefab/Prefab_UI/UI_Tips/UI_NearTips";
    public static string UIpath_SystemTips = "Prefab/Prefab_UI/UI_Tips/UI_SystemTips";
    public static string UIpath_TalkTips = "Prefab/Prefab_UI/UI_Tips/UI_TalkTips";
    public static string UIpath_ButtonTips = "Prefab/Prefab_UI/UI_Tips/UI_ButtonTips";
    public static string UIpath_NewTips = "Prefab/Prefab_UI/UI_Tips/UI_NewTips";
    public static string UIpath_GameOver = "Prefab/Prefab_UI/UI_Tips/UI_GameOver";
    public static string UIpath_ProgressAnimation = "Prefab/Prefab_UI/UI_Tips/UI_ProgressAnimation"; 
    //INFO
    public static string UIpath_IdentityInfo = "Prefab/Prefab_UI/UI_Info/UI_IdentityInfo";
    public static string UIpath_OrganizeInfo = "Prefab/Prefab_UI/UI_Info/UI_OrganizeInfo";
    public static string UIpath_RoleInfo = "Prefab/Prefab_UI/UI_Info/UI_RoleInfo";
    public static string UIpath_ForceInfo = "Prefab/Prefab_UI/UI_Info/UI_ForceInfo";
    public static string UIpath_AreaInfo = "Prefab/Prefab_UI/UI_Info/UI_AreaInfo";
    public static string UIpath_NewsInfo = "Prefab/Prefab_UI/UI_Info/UI_NewDetails";
    public static string UIpath_KnowledgeInfo = "Prefab/Prefab_UI/UI_Info/UI_KnowledgeInfo";
    public static string UIpath_TaskInfo = "Prefab/Prefab_UI/UI_Info/UI_TaskInfo";
    //ICON
    public static string UIpath_Button_ItemEX = "Prefab/Prefab_UI/UI_Icon/Button_ItemEX";
    public static string UIpath_Button_PropEX = "Prefab/Prefab_UI/UI_Icon/Button_PropEX";
    public static string UIpath_Label_NoneEX = "Prefab/Prefab_UI/UI_Icon/Label_NoneEX";
    public static string UIpath_Price_MoneyEX = "Prefab/Prefab_UI/UI_Icon/Price_MoneyEX";
    public static string UIpath_Button_RoleIcon = "Prefab/Prefab_UI/UI_Icon/Button_RoleIcon";
    public static string UIpath_Button_SkillEX = "Prefab/Prefab_UI/UI_Icon/Button_SkillEX";
    public static string UIpath_Button_IdenThingEX = "Prefab/Prefab_UI/UI_Icon/Button_IdenThingEX";
    public static string UIpath_Button_IdentityEX = "Prefab/Prefab_UI/UI_Icon/Button_IdentityEX";
    public static string UIpath_Button_WayEX = "Prefab/Prefab_UI/UI_Icon/Button_WayEX";

    //THINGS
    public static string UIpath_ChooseItem = "Prefab/Prefab_UI/UI_Things/UI_ChooseItem";
    public static string UIpath_Gift = "Prefab/Prefab_UI/UI_Things/UI_Gift";
    public static string UIpath_ChangePay = "Prefab/Prefab_UI/UI_Things/UI_ChangePay";
    public static string UIpath_ChangeRole = "Prefab/Prefab_UI/UI_Things/UI_ChooseRole";
    public static string UIpath_IdentityThing = "Prefab/Prefab_UI/UI_Things/UI_IdentityThing";
    public static string UIpath_OrganizeDes = "Prefab/Prefab_UI/UI_Things/UI_OrganizeDes";
    public static string UIpath_OrganizeAdmin = "Prefab/Prefab_UI/UI_Things/UI_OrganizeAdmin";
    public static string UIpath_OrgTask_Fixed = "Prefab/Prefab_UI/UI_Things/UI_OrgTask_Fixed";
    public static string UIpath_OrgTask_Get = "Prefab/Prefab_UI/UI_Things/UI_OrgTask_Get";
    public static string UIpath_OrgRest_Shop = "Prefab/Prefab_UI/UI_Things/UI_OrgRest_Shop";
    public static string UIpath_ContactLeader = "Prefab/Prefab_UI/UI_Things/UI_ContactLeader";
    public static string UIpath_Holiday = "Prefab/Prefab_UI/UI_Things/UI_Holiday";
    //OTHER
    public static string UIpath_BuildMode = "Prefab/Prefab_UI/UI_Scene/UI_BuildMode";
    public static string UIpath_GMTools = "Prefab/Prefab_Tools/GameObject_GMTools";
    //SCENE
    public static string UIpath_Fighting = "Prefab/Prefab_UI/UI_Scene/UI_Fighting";
    public static string UIpath_FightEnter = "Prefab/Prefab_UI/UI_Scene/UI_FightEnter";
    //MAP界面
    public static string UIpath_WorldMapSetting = "Prefab/Prefab_UI/UI_Maps/UI_WorldMapSetting";

    //各个UI图集
    public static string UIAtlas_UI_Main = "Atlas/UIAtlas/UI_Main";
    public static string UIAtlas_UI_MainBG = "Atlas/UIAtlas/UI_MainBG";
    public static string UIAtlas_Icon_Item1 = "Atlas/UIAtlas/Icon_Item1";
    public static string UIAtlas_Icon_RoleHead = "Atlas/UIAtlas/Icon_RoleHead";
    public static string UIAtlas_NowIcon = "Atlas/UIAtlas/NowIcon";

    public static string Atlas_Map = "Atlas/GroundAtlas/Atlas_Map";

    //其他图集
    //各个scene名称
    public static string SceneName_GameInfo_LOGO = "GameInfo_LOGO";
    public static string SceneName_Main_Menu = "Main_Menu";
    public static string SceneName_Map_World = "Map_World";
    public static string SceneName_TheWorld = "TheWorld";
    public static string SceneName_DesignPattern = "DesignPattern";
    public static string SceneName_TestUI = "TestUI";
 
    //地图相关素材路径（不包括文件名）
    public static string MapPic_Area = "Picture/Map_Area/";
    public static string MapPic_SceneModel = "Picture/Map_City/";
    public static string MapPic_PartModel = "Picture/Map_Part/";
    public static string MapPic_Nomal = "Picture/Map_Nomal/";
    public static string MapData_Main = "Json/MapData/";

    
    // 存档数据库表名
    public static string TableName_BuildingPart = "Save_BuildingPart";
    public static string TableName_Player = "Save_Player";
    public static string TableName_Title = "Save_Title";

}
