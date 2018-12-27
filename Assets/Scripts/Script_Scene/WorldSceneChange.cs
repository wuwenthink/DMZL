using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSceneChange : MonoBehaviour {
    public Transform World_Root;
    public Transform Map_World;
    public Transform Map_Building;

    public GameObject SelectedBox;
    public GameObject Part_Already;
    public GameObject PlaneGround;
    public SpriteRenderer GroundPic;
    public Camera worldCamera;

    void Start () {
        //CommonFunc.GetInstance.Map_Instantiate(Data_Static.Map_City_beijing, Map_World);
    }
	

	void Update () {
		
	}

    public void SceneChange(string _prefabPath, int sceneId)
    {
        MapScene newScene = CommonFunc.GetInstance.Map_Instantiate(_prefabPath, sceneId);
        //读取场景模块数据
        Dictionary<string, MapPart_Data> dic_partData = new Dictionary<string, MapPart_Data>();
        dic_partData = JsonReader.ReadJson<MapPart_Data>(Data_Static.Res_pathScene_Data + newScene.id);
        foreach (var item in dic_partData)
        {
            List<MapBox> list_grid = new List<MapBox>();
            Dictionary<string, MapBox> dic_grid = new Dictionary<string, MapBox>();
            list_grid = item.Value.Dic_MapBox;
            foreach (var grid in list_grid)
            {
                dic_grid.Add(grid.Pos, grid);
            }
            List<PartBuilding> list_build = new List<PartBuilding>();
            Dictionary<int, PartBuilding> dic_build = new Dictionary<int, PartBuilding>();
            list_build = item.Value.Dic_Building;
            foreach (var build in list_build)
            {
                dic_build.Add(build.id, build);
            }
            MapPart mp = new MapPart();
            mp.id = item.Value.id;
            mp.writeName = item.Value.writeName;
            mp.sizeX = item.Value.sizeX;
            mp.sizeY = item.Value.sizeY;
            mp.posXY = item.Value.posXY;
            mp.modelID = item.Value.modelID;
            mp.upScene = item.Value.upScene;
            mp.Dic_MapBox = dic_grid;
            mp.Dic_Building = dic_build;
            newScene.Dic_MapPart.Add(item.Value.id, mp);
        }
        float GridSize = 0.64f;
        //生成模块
        Dictionary<int, GameObject> alreadyParts = new Dictionary<int, GameObject>();
        foreach (var item in newScene.Dic_MapPart)
        {
            Map_PartModel mp = SelectDao.GetDao().SelectMap_PartModel(item.Value.modelID);
            int countX = item.Value.sizeX;
            int countY = item.Value.sizeY;
            SelectedBox.SetActive(true);
            SpriteRenderer srB = SelectedBox.GetComponent<SpriteRenderer>();
            srB.sprite = Resources.Load<Sprite>(Data_Static.MapPic_PartModel + mp.mapIcon);
            srB.drawMode = SpriteDrawMode.Tiled;
            srB.size = new Vector2(countX * GridSize, countY * GridSize);
            SelectedBox.GetComponent<BoxCollider2D>().size = new Vector2(countX * GridSize, countY * GridSize);
            //SelectedBox.transform.localScale = new Vector3(distanceX, distanceY, 1);
            float posX = float.Parse(item.Value.posXY.Split('_')[0]);
            float posY = float.Parse(item.Value.posXY.Split('_')[1]);
            SelectedBox.transform.localPosition = new Vector3(posX, posY, 0);
            //将矩形放入已完成目录下，并且生成碰撞体
            GameObject already = Instantiate(SelectedBox);
            already.transform.parent = Part_Already.transform;
            //already.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Data_Static.MapPic_PartModel + SelectDao.GetDao().SelectMap_PartModel(newPart.modelID).mapIcon);
            already.name = item.Value.id.ToString();
            already.layer = 9;
            alreadyParts.Add(item.Value.id, already);
        }

        Map_SceneModel ms = SelectDao.GetDao().SelectMap_SceneModel(newScene.modelID);
        float mapSizeX = (float)ms.maxSize_X * GridSize;
        float mapSizeY = (float)ms.maxSize_Y * GridSize;
        PlaneGround.transform.localScale = new Vector3(mapSizeX, mapSizeY, 0);
        PlaneGround.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(ms.maxSize_X, ms.maxSize_Y));
        GroundPic.sprite = Resources.Load<Sprite>(Data_Static.MapPic_SceneModel + ms.ground);
        worldCamera.transform.localPosition = new Vector3(0, 0, -1);

        //地图边界控制
        MapControl_Move mm = FindObjectOfType<MapControl_Move>();
        mm.limit_Bottom = -mapSizeX / 2f;
        mm.limit_Top = mapSizeX / 2f;
        mm.limit_Left = -mapSizeY / 2f;
        mm.limit_Right = mapSizeY / 2f;
        mm.isOpenMouse = false;
    }
}
