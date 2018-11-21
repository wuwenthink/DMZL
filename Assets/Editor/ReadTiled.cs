using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tiled2Unity;

[Tiled2Unity.CustomTiledImporter]
public class CustomImporter_StrategyTiles : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> customProperties)
    {
        if (customProperties.ContainsKey("bgType"))
        {
            // 根据自定类取得瓦片上面自定义的数据
            MapData_Fight tile = gameObject.AddComponent<MapData_Fight>();
            tile.bgType = customProperties["bgType"];
        }
        if (customProperties.ContainsKey("AreaID"))
        {
            // 根据自定类取得瓦片上面自定义的数据
            MapData_SpecialArea tile = gameObject.AddComponent<MapData_SpecialArea>();
            tile.AreaID = customProperties["AreaID"];
            tile.AreaType = customProperties["AreaType"];
        }
        if (customProperties.ContainsKey("BuildingID"))
        {
            // 根据自定类取得瓦片上面自定义的数据
            MapData_SpecialBuilding tile = gameObject.AddComponent<MapData_SpecialBuilding>();
            tile.BuildingID = customProperties["BuildingID"];
            tile.BuildingType = customProperties["BuildingType"];
        }
    }

    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing
    }
}



