// ======================================================================================
// 文 件 名 称：SelectDao.cs
// 版 本 编 号：v1.6.1
// 原 创 作 者：xic
// 创 建 日 期：2017-9-28 11:49:41
// 更 新 日 期：2017-10-31
// ======================================================================================
// 功 能 描 述：数据库：查询
// ======================================================================================

using Mono.Data.Sqlite;
using System.Collections.Generic;

/// <summary>
/// 数据库查询
/// </summary>
public class SelectDao
{
    private SqliteDataReader reader;
    private SqliteDbService dbService_game;
    private SqliteDbService dbService_save;
    private static SelectDao selectDao;

    private SelectDao()
    {
        dbService_game = BasicData.GetDataDbService;
        dbService_save = BasicData.GetSaveDbService;
    }

    /// <summary>
    /// 单例
    /// </summary>
    /// <returns></returns>
    public static SelectDao GetDao()
    {
        if (selectDao == null)
            selectDao = new SelectDao();
        return selectDao;
    }

    /*   存档模块的代码  */


    /// <summary>
    /// 根据建筑组件的位置查询跳转室内的信息
    /// (查询存档)
    /// </summary>
    /// <param name="_posX"></param>
    /// <param name="_posY"></param>
    /// <returns></returns>
    public BuildingPart SelectIndoorRoom(float _posX, float _posY, int _sceneId)
    {
        BuildingPart buildingPart = null;
        if (dbService_save.TableExist(Data_Static.TableName_BuildingPart))
        {
            string sql = "SELECT * FROM BuildingPart WHERE position='" + _posX + "," + _posY + "' AND sceneId='" + _sceneId + "';";
            reader = dbService_save.ExecuteReader(sql);
            while (reader.Read())
            {
                buildingPart = new BuildingPart(float.Parse(reader.GetString(0).Split(',')[0]), float.Parse(reader.GetString(0).Split(',')[1]), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5));
            }
        }
        return buildingPart;
    }


    /*   常规数据表的代码  */


    /// <summary>
    /// 通过id查询技能主表
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public skill_Main SelectSkillMain(int _id)
    {
        skill_Main go = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM skill_Main WHERE id=" + _id + ";");
        while (reader.Read())
        {
            go = new skill_Main(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetString(15), reader.GetString(16), reader.GetString(17), reader.GetString(18));
        }
        return go;
    }

    /// <summary>
    /// 通过归属武学查询技能主表
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public List<skill_Main> SelectSkillMain_ByGongfu(int _gongfu)
    {
        List<skill_Main> list = new List<skill_Main>();
        reader = dbService_game.ExecuteReader("SELECT * FROM skill_Main WHERE gongfu=" + _gongfu);
        while (reader.Read())
        {
            var go = new skill_Main(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetString(15), reader.GetString(16), reader.GetString(17), reader.GetString(18));
            list.Add(go);
        }
        return list;
    }

    /// <summary>
    /// 通过武器查询技能主表
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public List<skill_Main> SelectSkillMain_ByWeapon(int _weapon)
    {
        List<skill_Main> list = new List<skill_Main>();
        reader = dbService_game.ExecuteReader("SELECT * FROM skill_Main WHERE gongfu=" + _weapon);
        while (reader.Read())
        {
            var go = new skill_Main(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetString(15), reader.GetString(16), reader.GetString(17), reader.GetString(18));
            list.Add(go);
        }
        return list;
    }


    /// <summary>
    /// 通过id查询技能BUFF表
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public skill_Buff SelectSkill_Buff(int _id)
    {
        skill_Buff go = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM skill_Buff WHERE id=" + _id);
        while (reader.Read())
        {
            go = new skill_Buff(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
        }
        return go;
    }

    /// <summary>
    /// 通过id查询机构
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Organize SelectOrganize(int _id)
    {
        Organize org = null;
        reader = dbService_game.ExecuteReader("SELECT id, oName, upID, icon, des, leader, force FROM Organize WHERE id=" + _id);
        while (reader.Read())
        {
            org = new Organize(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
        }
        return org;
    }

    /// <summary>
    /// 查询所有机构
    /// </summary>
    /// <returns></returns>
    public List<Organize> SelectAllOrganize()
    {
        List<Organize> list = new List<Organize>();
        reader = dbService_game.ExecuteReader("SELECT id, oName, upID, icon, des, leader, force FROM Organize ;");
        while (reader.Read())
        {
            var org = new Organize(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
            list.Add(org);
        }
        return list;
    }

    /// <summary>
    /// 通过id查询势力
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Force SelectForce(int _id)
    {
        Force force = null;
        reader = dbService_game.ExecuteReader("SELECT id, name, type, icon, flag, capital, geography, history, colorR, colorG, colorB, colorA FROM Force WHERE id=" + _id);
        while (reader.Read())
        {
            force = new Force(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11));
        }
        return force;
    }

    /// <summary>
    /// 通过id查询区域
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Map_Area SelectArea(int _id)
    {
        Map_Area area = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Map_Area WHERE id=" + _id);
        while (reader.Read())
        {
            area = new Map_Area(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13));
        }
        return area;
    }

    /// <summary>
    /// 通过id查询消息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public News SelectNews(int _id)
    {
        News news = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM News WHERE id=" + _id);
        while (reader.Read())
        {
            news = new News(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13), reader.GetString(14));
        }
        return news;
    }

    /// <summary>
    /// 通过id查询事务
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Task SelectTask(int _id)
    {
        Task task = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Task  WHERE id=" + _id);
        while (reader.Read())
        {
            task = new Task(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetInt32(7), reader.GetString(8), reader.GetInt32(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13), reader.GetString(14), reader.GetString(15), reader.GetString(16), reader.GetInt32(17));
        }
        return task;
    }

    /// <summary>
    /// 按类型随机获取若干条事务
    /// </summary>
    /// <param name="_type">类型</param>
    /// <param name="_num">要得到的数量（查所有填-1）</param>
    /// <returns></returns>
    public List<Task> SelectRandomTaskByType(int _type, int _num)
    {
        List<Task> taskList = new List<Task>();
        if (_num == -1)
            reader = dbService_game.ExecuteReader("SELECT * FROM Task WHERE type=" + _type + ";");
        else
            reader = dbService_game.ExecuteReader("SELECT * FROM Task WHERE type=" + _type + " ORDER BY random() LIMIT " + _num + ";");
        while (reader.Read())
        {
            taskList.Add(new Task(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetInt32(7), reader.GetString(8), reader.GetInt32(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13), reader.GetString(14), reader.GetString(15), reader.GetString(16), reader.GetInt32(17)));
        }
        return taskList;
    }

    /// <summary>
    /// 通过id查询学识
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Knowledge SelectKnowledge(int _id)
    {
        Knowledge knowlege = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Knowledge WHERE id=" + _id);
        while (reader.Read())
        {
            knowlege = new Knowledge(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13));
        }
        return knowlege;
    }

    /// <summary>
    /// 按类型查询学识
    /// </summary>
    /// <returns></returns>
    public List<Knowledge> SelectKnowByType(int _type)
    {
        List<Knowledge> list = new List<Knowledge>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Knowledge WHERE type=" + _type + ";");
        while (reader.Read())
        {
            var by = new Knowledge(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13));
            list.Add(by);
        }
        return list;
    }

    /// <summary>
    /// 按上级学识查询学识
    /// </summary>
    /// <returns></returns>
    public List<Knowledge> SelectKnowByClass(int _class)
    {
        List<Knowledge> list = new List<Knowledge>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Knowledge WHERE class=" + _class + ";");
        while (reader.Read())
        {
            var by = new Knowledge(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13));
            list.Add(by);
        }
        return list;
    }

    /// <summary>
    /// 查询所有的学识
    /// </summary>
    /// <returns></returns>
    public List<Knowledge> SelectAllKnowledge()
    {
        var knowledgeList = new List<Knowledge>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Knowledge ;");
        while (reader.Read())
        {
            var knowlege = new Knowledge(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13));
            knowledgeList.Add(knowlege);
        }
        return knowledgeList;
    }

    /// <summary>
    /// 通过成长索引查询学识成长表
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Knowledge_Grow SelectKnowledge_Grow(int _id)
    {
        Knowledge_Grow grow = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Knowlege_Grow  WHERE grow=" + _id);
        while (reader.Read())
        {
            if (grow == null)
            {
                grow = new Knowledge_Grow(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6));
            }
            else
                grow.growData.Add(reader.GetInt32(2), new Grow(reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6)));
        }
        return grow;
    }

    /// <summary>
    /// 通过id查询技法
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Skill SelectSkill(int _id)
    {
        Skill skill = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Skill WHERE id=" + _id);
        while (reader.Read())
        {
            skill = new Skill(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
        }
        return skill;
    }


    /// <summary>
    /// 按类型查询技法
    /// </summary>
    /// <returns></returns>
    public List<Skill> SelectSkillByType(int _type)
    {
        List<Skill> list = new List<Skill>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Skill WHERE type=" + _type + ";");
        while (reader.Read())
        {
            var by = new Skill(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
            list.Add(by);
        }
        return list;
    }

    /// <summary>
    /// 按上级学识查询技法
    /// </summary>
    /// <returns></returns>
    public List<Skill> SelectSkillByClass(int _class)
    {
        List<Skill> list = new List<Skill>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Skill WHERE bigClass=" + _class + ";");
        while (reader.Read())
        {
            var by = new Skill(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
            list.Add(by);
        }
        return list;
    }

    /// <summary>
    /// 查询全部的技法
    /// </summary>
    /// <returns></returns>
    public List<Skill> SelectAllSkill()
    {
        var list = new List<Skill>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Skill;");
        while (reader.Read())
        {
            var skill = new Skill(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
            list.Add(skill);
        }
        return list;
    }

    /// <summary>
    /// 通过成长索引查询技法成长表
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Skill_Grow SelectSkill_Grow(int _id)
    {
        Skill_Grow grow = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Skill_Grow  WHERE grow=" + _id);
        while (reader.Read())
        {
            if (grow == null)
            {
                grow = new Skill_Grow(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5));
            }
            else
                grow.growData.Add(reader.GetInt32(2), new SkillGrowData(reader.GetInt32(3), reader.GetString(4), reader.GetString(5)));
        }
        return grow;
    }

    /// <summary>
    /// 查询全部配方
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, MakeRecipe> SelectAllProduce()
    {
        Dictionary<int, MakeRecipe> produce_All = new Dictionary<int, MakeRecipe>();
        reader = dbService_game.ExecuteReader("SELECT * FROM MakeRecipe ;");
        while (reader.Read())
        {
            var produce = new MakeRecipe(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetString(11), reader.GetString(12), reader.GetInt32(13), reader.GetString(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17));
            produce_All.Add(reader.GetInt32(0), produce);
        }
        return produce_All;
    }

    /// <summary>
    /// 按ID查询配方
    /// </summary>
    /// <returns></returns>
    public MakeRecipe SelectProduce(int _id)
    {
        MakeRecipe produce = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM MakeRecipe WHERE id=" + _id);
        while (reader.Read())
        {
            produce = new MakeRecipe(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetString(11), reader.GetString(12), reader.GetInt32(13), reader.GetString(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17));
        }
        return produce;
    }

    /// <summary>
    /// 按器具查询配方
    /// </summary>
    /// <returns></returns>
    public List<MakeRecipe> SelectProduceByTool(int _tool)
    {
        List<MakeRecipe> list = new List<MakeRecipe>();
        reader = dbService_game.ExecuteReader("SELECT * FROM MakeRecipe WHERE type=" + _tool + ";");
        while (reader.Read())
        {
            var By = new MakeRecipe(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetString(11), reader.GetString(12), reader.GetInt32(13), reader.GetString(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17));
            list.Add(By);
        }
        return list;
    }

    /// <summary>
    /// 按ID查询建筑组件模板
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public BuildingPart_Model SelectBuildingPartModel(int _id)
    {
        BuildingPart_Model buildingPart_Model = null;
        reader = dbService_game.ExecuteReader("SELECT model_size, prefab FROM Building_PartModel WHERE id=" + _id + ";");
        while (reader.Read())
        {
            buildingPart_Model = new BuildingPart_Model(_id, reader.GetString(0), reader.GetString(1));
        }
        return buildingPart_Model;
    }

    /// <summary>
    /// 按建筑组件id查询对应的室内模板集合
    /// </summary>
    /// <param name="_buildingPartId"></param>
    /// <returns></returns>
    public List<int> SelectIndoorModel(int _buildingPartId)
    {
        List<int> list = new List<int>();
        string str = "";
        reader = dbService_game.ExecuteReader("SELECT model_id FROM Building_Part WHERE id=" + _buildingPartId + ";");
        while (reader.Read())
        {
            str = reader.GetString(0);
        }
        var reg = str.Split(';');
        foreach (var s in reg)
        {
            list.Add(int.Parse(s));
        }
        return list;
    }


    /// <summary>
    /// 根据行当的ID查询对应行当的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Business_Trade SelectBusiness_Trade(int _id)
    {
        Business_Trade business_Trade = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Trade WHERE id=" + _id + ";");
        while (reader.Read())
        {
            business_Trade = new Business_Trade(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17));
        }
        return business_Trade;
    }

    /// <summary>
    /// 按类型查询行当
    /// </summary>
    /// <returns></returns>
    public List<Business_Trade> SelectBusiness_TradeByType(int _type)
    {
        List<Business_Trade> list = new List<Business_Trade>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Trade WHERE type=" + _type + ";");
        while (reader.Read())
        {
            var Business_TradeByType = new Business_Trade(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17));
            list.Add(Business_TradeByType);
        }
        return list;
    }

    /// <summary>
    /// 按身份查询行当
    /// </summary>
    /// <returns></returns>
    public List<Business_Trade> SelectBusiness_TradeByIden(int _Iden)
    {
        List<Business_Trade> list = new List<Business_Trade>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Trade WHERE identity=" + _Iden + ";");
        while (reader.Read())
        {
            var Business_TradeByIden = new Business_Trade(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17));
            list.Add(Business_TradeByIden);
        }
        return list;
    }

    /// <summary>
    /// 按照ID查询系统配置表内容
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public System_Config SelectSystem_Config(int _id)
    {
        System_Config system_Config = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM System_Config WHERE id=" + _id + ";");
        while (reader.Read())
        {
            system_Config = new System_Config(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
        }
        return system_Config;
    }

    /// <summary>
    /// 根据建筑模板的ID查询对应建筑模板的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Building_Model SelectBuilding_Model(int _id)
    {
        Building_Model building_Model = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Building_Model WHERE id=" + _id + ";");
        while (reader.Read())
        {
            building_Model = new Building_Model(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8), reader.GetString(9));
        }
        return building_Model;
    }

    /// <summary>
    /// 按建筑模板的类型查询所有建筑模板
    /// </summary>
    /// <returns></returns>
    public List<Building_Model> SelectBuildingModelByType(int _buildingType)
    {
        List<Building_Model> list = new List<Building_Model>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Building_Model WHERE buildingType=" + _buildingType + ";");
        while (reader.Read())
        {
            var By = new Building_Model(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8), reader.GetString(9));
            list.Add(By);
        }
        return list;
    }

        /// <summary>
        /// 根据模块的ID查询对应场景模块的信息
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Map_PartModel SelectMap_PartModel(int _id)
    {
        Map_PartModel go = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Map_PartModel WHERE id=" + _id + ";");
        while (reader.Read())
        {
            go = new Map_PartModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4),reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetString(11), reader.GetString(12));
        }
        return go;
    }

    /// <summary>
    /// 按是否特殊查询对应所有场景模块的信息
    /// </summary>
    /// <returns></returns>
    public List<Map_PartModel> SelectMap_PartModelByIsSpecial(int _isSpecial)
    {
        List<Map_PartModel> list = new List<Map_PartModel>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Map_PartModel WHERE isSpecial=" + _isSpecial + ";");
        while (reader.Read())
        {
            var by = new Map_PartModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetString(11), reader.GetString(12));
            list.Add(by);
        }
        return list;
    }

    /// <summary>
    /// 根据地形的ID查询对应地形的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Map_Terrain SelectMap_Terrain(int _id)
    {
        Map_Terrain go = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Map_Terrain WHERE id=" + _id + ";");
        while (reader.Read())
        {
            go = new Map_Terrain(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5));
        }
        return go;
    }

    /// <summary>
    /// 根据ID查询对应商业地区的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Business_Area SelectBusiness_Area(int _id)
    {
        Business_Area business_Area = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Area WHERE id=" + _id + ";");
        while (reader.Read())
        {
            business_Area = new Business_Area(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
        }
        return business_Area;
    }

    /// <summary>
    /// 按类型查询商品
    /// </summary>
    /// <returns></returns>
    public List<Business_Area> SelectBusiness_AreaByType(int _type)
    {
        List<Business_Area> list = new List<Business_Area>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Area WHERE type=" + _type + ";");
        while (reader.Read())
        {
            var Business_areaByType = new Business_Area(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
            list.Add(Business_areaByType);
        }
        return list;
    }

    /// <summary>
    /// 根据ID查询对应牙行模板的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Business_Broker SelectBusiness_Broker(int _id)
    {
        Business_Broker business_Broker = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Broker WHERE id=" + _id + ";");
        while (reader.Read())
        {
            business_Broker = new Business_Broker(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(15));
        }
        return business_Broker;
    }

    /// <summary>
    /// 根据ID查询对应商业环境的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Business_Environment SelectBusiness_Environment(int _id)
    {
        Business_Environment Selectbusiness_Environment = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Environment WHERE id=" + _id + ";");
        while (reader.Read())
        {
            Selectbusiness_Environment = new Business_Environment(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4));
        }
        return Selectbusiness_Environment;
    }

    /// <summary>
    /// 根据ID查询对应商品的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Business_Goods SelectBusiness_Goods(int _id)
    {
        Business_Goods Selectbusiness_Goods = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Goods WHERE id=" + _id + ";");
        while (reader.Read())
        {
            Selectbusiness_Goods = new Business_Goods(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetString(9), reader.GetInt32(10));
        }
        return Selectbusiness_Goods;
    }

    /// <summary>
    /// 按类型查询商品
    /// </summary>
    /// <returns></returns>
    public List<Business_Goods> SelectBusiness_GoodsByType(int _type)
    {
        List<Business_Goods> list = new List<Business_Goods>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Goods WHERE type=" + _type + ";");
        while (reader.Read())
        {
            var Business_GoodsByType = new Business_Goods(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetString(9), reader.GetInt32(10));
            list.Add(Business_GoodsByType);
        }
        return list;
    }

    /// <summary>
    /// 根据ID查询对应仓库的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Business_Warehouse SelectBusiness_Warehouse(int _id)
    {
        Business_Warehouse Selectbusiness_Warehouse = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Business_Warehouse WHERE id=" + _id + ";");
        while (reader.Read())
        {
            Selectbusiness_Warehouse = new Business_Warehouse(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetString(11), reader.GetInt32(12), reader.GetString(13), reader.GetInt32(14));
        }
        return Selectbusiness_Warehouse;
    }

    /// <summary>
    /// 根据ID查询对应场景的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Map_Scene SelectMap_Scene(int _id)
    {
        Map_Scene Selectmap_Scene = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Map_Scene WHERE id=" + _id + ";");
        while (reader.Read())
        {
            Selectmap_Scene = new Map_Scene(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5))
;
        }
        return Selectmap_Scene;
    }

    /// <summary>
    /// 根据ID查询对应场景模板的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Map_SceneModel SelectMap_SceneModel(int _id)
    {
        Map_SceneModel Selectmap_Scene = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Map_SceneModel WHERE id=" + _id + ";");
        while (reader.Read())
        {
            Selectmap_Scene = new Map_SceneModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6))
;
        }
        return Selectmap_Scene;
    }

    /// <summary>
    /// 查询全部场景模板
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, Map_SceneModel> SelectAllMap_SceneModel()
    {
        Dictionary<int, Map_SceneModel> all = new Dictionary<int, Map_SceneModel>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Map_SceneModel ;");
        while (reader.Read())
        {
            var go = new Map_SceneModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
            all.Add(reader.GetInt32(0), go);
        }
        return all;
    }

    /// <summary>
    /// 根据ID查询对应组织职位的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Organize_Title SelectOrganize_Title (int _id)
    {
        Organize_Title Selectorganize_Title = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Organize_Title WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            Selectorganize_Title = new Organize_Title (reader.GetInt32 (0), reader.GetString (1), reader.GetInt32 (2), reader.GetString (3), reader.GetInt32 (4), reader.GetInt32 (5));
        }
        return Selectorganize_Title;
    }

    /// <summary>
    /// 根据ID查询对应关系的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Relationship SelectRelationship (int _id)
    {
        Relationship Selectrelationship = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Relationship WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            Selectrelationship = new Relationship (reader.GetInt32 (0), reader.GetInt32 (1), reader.GetString (2), reader.GetInt32 (3), reader.GetString (4), reader.GetString (5));
        }
        return Selectrelationship;
    }

    /// <summary>
    /// 根据好感度查询对应关系的信息
    /// </summary>
    /// <param name="_exp">好感度</param>
    /// <returns></returns>
    public Relationship SelectRelationshipByExp (int _exp)
    {
        Relationship Selectrelationship = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Relationship;");
        while (reader.Read ())
        {
            string [] str = reader.GetString (4).Split (';');
            if (str.Length > 0)
            {
                if (_exp >= int.Parse (str [0]) && _exp <= int.Parse (str [1]))
                {
                    Selectrelationship = new Relationship (reader.GetInt32 (0), reader.GetInt32 (1), reader.GetString (2), reader.GetInt32 (3), reader.GetString (4), reader.GetString (5));
                    return Selectrelationship;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 根据ID查询对应职位的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Role_Job SelecRole_Job (int _id)
    {
        Role_Job Selectrole_Job = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_Job WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            Selectrole_Job = new Role_Job (reader.GetInt32 (0), reader.GetString (1), reader.GetInt32 (2), reader.GetString (3), reader.GetInt32 (4), reader.GetInt32 (5), reader.GetInt32 (6), reader.GetInt32 (7));
        }
        return Selectrole_Job;
    }

    /// <summary>
    /// 根据ID查询对应性格的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Role_Nature SelecRole_Nature (int _id)
    {
        Role_Nature Selectrole_Nature = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_Nature WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            Selectrole_Nature = new Role_Nature (reader.GetInt32 (0), reader.GetString (1), reader.GetString (2), reader.GetInt32 (3), reader.GetInt32 (4));
        }
        return Selectrole_Nature;
    }

    /// <summary>
    /// 根据身份ID查询角色生成模板
    /// </summary>
    /// <param name="_idenId"></param>
    /// <returns></returns>
    public Role_Generate SelectRole_GenerateByIden (int _idenId)
    {
        Role_Generate Role_Generate = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_Generate WHERE idenId=" + _idenId + ";");
        while (reader.Read ())
        {
            Role_Generate = new Role_Generate (reader.GetInt32 (0), reader.GetString (1), reader.GetString (2), reader.GetInt32 (3), reader.GetString (4), reader.GetInt32 (5), reader.GetString (6), reader.GetString (7), reader.GetString (8), reader.GetString (9), reader.GetInt32 (10), reader.GetString (11), reader.GetString (12), reader.GetString (13), reader.GetString (14), reader.GetString (15), reader.GetString (16), reader.GetString (17), reader.GetString (18));
        }
        return Role_Generate;
    }

    /// <summary>
    /// 根据ID查询角色生成模板
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Role_Generate SelectRole_Generate (int _id)
    {
        Role_Generate Role_Generate = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_Generate WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            Role_Generate = new Role_Generate (reader.GetInt32 (0), reader.GetString (1), reader.GetString (2), reader.GetInt32 (3), reader.GetString (4), reader.GetInt32 (5), reader.GetString (6), reader.GetString (7), reader.GetString (8), reader.GetString (9), reader.GetInt32 (10), reader.GetString (11), reader.GetString (12), reader.GetString (13), reader.GetString (14), reader.GetString (15), reader.GetString (16), reader.GetString (17), reader.GetString (18));
        }
        return Role_Generate;
    }

    /// <summary>
    /// 随机NPC——随机获取一个姓氏
    /// </summary>
    /// <returns></returns>
    public string SelectRoleRandomInfo_Lastname ()
    {
        string str = "";
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_RandomInfo_Lastname ORDER BY RANDOM() LIMIT 1");
        while (reader.Read ())
        {
            str = reader.GetString (0);
        }
        return str;
    }

    /// <summary>
    /// 随机NPC——随机获取一个男名
    /// </summary>
    /// <returns></returns>
    public string SelectRoleRandomInfo_Malename ()
    {
        string str = "";
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_RandomInfo_FirstnameMale ORDER BY RANDOM() LIMIT 2");
        while (reader.Read ())
        {
            str += reader.GetString (0);
            if (new System.Random ().Next (0, 10) <= 3)
                return str;
        }
        return str;
    }

    /// <summary>
    /// 随机NPC——随机获取一个女名
    /// </summary>
    /// <returns></returns>
    public string SelectRoleRandomInfo_Femalname ()
    {
        string str = "";
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_RandomInfo_FirstnameFemale ORDER BY RANDOM() LIMIT 1");
        while (reader.Read ())
        {
            str = reader.GetString (0);
        }
        return str;
    }
    /// <summary>
    /// 随机NPC——随机获取一个表字
    /// </summary>
    /// <returns></returns>
    public string SelectRoleRandomInfo_Wordname ()
    {
        string str = "";
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_RandomInfo_Wordname ORDER BY RANDOM() LIMIT 1");
        while (reader.Read ())
        {
            str = reader.GetString (0);
        }
        return str;
    }

    /// <summary>
    /// 随机NPC——随机获取一个号
    /// </summary>
    /// <returns></returns>
    public string SelectRoleRandomInfo_Titlename ()
    {
        string str = "";
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_RandomInfo_Titlename ORDER BY RANDOM() LIMIT 1");
        while (reader.Read ())
        {
            str = reader.GetString (0);
        }
        return str;
    }

    /// <summary>
    /// 随机NPC——随机获取一个出生府
    /// </summary>
    /// <returns></returns>
    public string SelectRoleRandomInfo_Birthfu ()
    {
        string str = "";
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_RandomInfo_BirthFu ORDER BY RANDOM() LIMIT 1");
        while (reader.Read ())
        {
            str = reader.GetString (0);
        }
        return str;
    }


    /// <summary>
    /// 按类型查询道具
    /// </summary>
    /// <returns></returns>
    public List<Item> SelectItemByType (int _type)
    {
        List<Item> list = new List<Item> ();
        reader = dbService_game.ExecuteReader ("SELECT * FROM Item WHERE type=" + _type + ";");
        while (reader.Read ())
        {
            var by = new Item (reader.GetInt32 (0), reader.GetInt32 (1), reader.GetString (2), reader.GetString (3), reader.GetString (4), reader.GetInt32 (5), reader.GetInt32 (6), reader.GetInt32 (7), reader.GetInt32 (8));
            list.Add (by);
        }
        return list;
    }


    /// <summary>
    /// 根据ID查询对应工作菜单的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public IndexMenu_Job SelectIndexMenu_Job (int _id)
    {
        IndexMenu_Job go = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM IndexMenu_Job WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            go = new IndexMenu_Job (reader.GetInt32 (0), reader.GetString (1), reader.GetInt32 (2), reader.GetInt32 (3), reader.GetInt32 (4), reader.GetInt32 (5));
        }
        return go;
    }

    /// <summary>
    /// 按类型查询工作菜单
    /// </summary>
    /// <returns></returns>
    public List<IndexMenu_Job> SelectIndexMenu_JobByJob (int _job)
    {
        List<IndexMenu_Job> list = new List<IndexMenu_Job> ();
        reader = dbService_game.ExecuteReader ("SELECT * FROM IndexMenu_Job WHERE job=" + _job + ";");
        while (reader.Read ())
        {
            var by = new IndexMenu_Job (reader.GetInt32 (0), reader.GetString (1), reader.GetInt32 (2), reader.GetInt32 (3), reader.GetInt32 (4), reader.GetInt32 (5));
            list.Add (by);
        }
        return list;

    }

    /// <summary>
    /// 根据ID查询组织任务
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public TaskOrg SelectTaskOrg (int _id)
    {
        TaskOrg go = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Task_Org WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            go = new TaskOrg (reader.GetInt32 (0), reader.GetInt32 (1), reader.GetInt32 (2), reader.GetString (3), reader.GetString (4), reader.GetInt32 (5), reader.GetString (6), reader.GetInt32 (7), reader.GetInt32 (8));
        }
        return go;
    }

    /// <summary>
    /// 随机获取一定数量的组织任务
    /// </summary>
    /// <param name="_num">要获取的条数；-1表示所有</param>
    /// <returns></returns>
    public List<TaskOrg> SelectRandomTaskOrg (int _num)
    {
        List<TaskOrg> list = new List<TaskOrg> ();
        if (_num == -1)
            reader = dbService_game.ExecuteReader ("SELECT * FROM Task_Org;");
        else
            reader = dbService_game.ExecuteReader ("SELECT * FROM Task_Org ORDER BY RANDOM() LIMIT " + _num + ";");
        while (reader.Read ())
        {
            TaskOrg go = new TaskOrg (reader.GetInt32 (0), reader.GetInt32 (1), reader.GetInt32 (2), reader.GetString (3), reader.GetString (4), reader.GetInt32 (5), reader.GetString (6), reader.GetInt32 (7), reader.GetInt32 (8));
            list.Add (go);
        }
        return list;
    }

    /// <summary>
    /// 随机获取一定数量的（每日/阶段）组织任务
    /// </summary>
    /// <param name="_num">要获取的条数</param>
    /// <param name="_daily">每日任务？阶段任务？</param>
    /// <returns></returns>
    public List<TaskOrg> SelectRandomTaskOrgByDaily (int _num, bool _daily)
    {
        List<TaskOrg> list = new List<TaskOrg> ();
        reader = dbService_game.ExecuteReader ("SELECT * FROM Task_Org WHERE isDaily=" + ((_daily == true) ? "1" : "0") + " ORDER BY RANDOM() LIMIT " + _num + ";");
        while (reader.Read ())
        {
            TaskOrg go = new TaskOrg (reader.GetInt32 (0), reader.GetInt32 (1), reader.GetInt32 (2), reader.GetString (3), reader.GetString (4), reader.GetInt32 (5), reader.GetString (6), reader.GetInt32 (7), reader.GetInt32 (8));
            list.Add (go);
        }
        return list;
    }

    /// <summary>
    /// 查询所有剧本
    /// </summary>
    /// <returns></returns>
    public List<Drama_Main> SelectAllDrama ()
    {
        List<Drama_Main> list = new List<Drama_Main> ();
        reader = dbService_game.ExecuteReader ("SELECT * FROM Drama_Main ;");
        while (reader.Read ())
        {
            var go = new Drama_Main (reader.GetInt32 (0), reader.GetString (1), reader.GetString (2), reader.GetInt32 (3),reader.GetInt32 (4), reader.GetInt32 (5), reader.GetString (6), reader.GetString (7));
            list.Add (go);
        }
        return list;
    }

    /// <summary>
    /// 根据ID查询剧本信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Drama_Main SelectDrama_Main (int _id)
    {
        Drama_Main go = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Drama_Main WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            go = new Drama_Main (reader.GetInt32 (0), reader.GetString (1), reader.GetString (2), reader.GetInt32 (3), reader.GetInt32 (4), reader.GetInt32 (5), reader.GetString (6), reader.GetString (7));
        }
        return go;
    }

    /// <summary>
    /// 根据ID查询剧本——地区内容
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Drama_Area SelectDrama_Area(int _id)
    {
        Drama_Area go = null;
        reader = dbService_game.ExecuteReader("SELECT * FROM Drama_Area WHERE id=" + _id + ";");
        while (reader.Read())
        {
            go = new Drama_Area(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetString(11), reader.GetInt32(12), reader.GetString(13), reader.GetString(14), reader.GetString(15), reader.GetString(16), reader.GetString(17), reader.GetString(18));
        }
        return go;
    }

    /// <summary>
    /// 按剧本ID查询剧本——地区内容
    /// </summary>
    /// <returns></returns>
    public List<Drama_Area> SelectDrama_Area_ByDramaID(int _drama)
    {
        List<Drama_Area> list = new List<Drama_Area>();
        reader = dbService_game.ExecuteReader("SELECT * FROM Drama_Area WHERE drama=" + _drama + ";");
        while (reader.Read())
        {
            var go = new Drama_Area(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetString(11), reader.GetInt32(12), reader.GetString(13), reader.GetString(14), reader.GetString(15), reader.GetString(16), reader.GetString(17), reader.GetString(18));
            list.Add(go);
        }
        return list;
    }

    /// <summary>
    /// 根据ID查询剧本——角色内容
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Drama_Role SelectDrama_Role (int _id)
    {
        Drama_Role go = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Drama_Role WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            go = new Drama_Role(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10));
        }
        return go;
    }

    /// <summary>
    /// 按剧本ID查询剧本——角色内容
    /// </summary>
    /// <returns></returns>
    public List<Drama_Role> SelectDrama_Role_ByDramaID(int _drama)
    {
        List<Drama_Role> list = new List<Drama_Role> ();
        reader = dbService_game.ExecuteReader ("SELECT * FROM Drama_Role WHERE drama=" + _drama + ";");
        while (reader.Read ())
        {
            var go = new Drama_Role(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10));
            list.Add (go);
        }
        return list;
    }


    /// <summary>
    /// 根据ID查询角色表的信息
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public Role_Main SelectRole (int _id)
    {
        Role_Main go = null;
        reader = dbService_game.ExecuteReader ("SELECT * FROM Role_Main WHERE id=" + _id + ";");
        while (reader.Read ())
        {
            go = new Role_Main(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(15), reader.GetInt32(15));
        }
        return go;
    }

    /// <summary>
    /// 查询所有NPC交互选项
    /// </summary>
    /// <returns></returns>
    public List<Interaction_Npc> SelectInteraction_Npc ()
    {
        List<Interaction_Npc> list = new List<Interaction_Npc> ();
        reader = dbService_game.ExecuteReader ("SELECT * FROM Interaction_Npc;");
        while (reader.Read ())
        {
            list.Add (new Interaction_Npc (reader.GetInt32 (0), reader.GetString (1), reader.GetString (2), reader.GetString (3), reader.GetString (4)));
        }
        return list;
    }



}