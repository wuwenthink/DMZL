// ======================================================================================
// 文 件 名 称：BuildingPart.cs
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-12 11:05:03
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-12 11:05:03
// ======================================================================================
// 功 能 描 述：运行时数据：建筑组件（跳转室内信息）
// ======================================================================================

using UnityEngine;

public class BuildingPart
{
    /// <summary>
    /// 建筑组件的坐标
    /// </summary>
    public Vector2 position { get; private set; }
    /// <summary>
    /// 所在场景ID
    /// </summary>
    public int sceneId { get; private set; }
    /// <summary>
    /// 建筑组件模板ID
    /// </summary>
    public int bpModelId { get; private set; }
    /// <summary>
    /// 跳转后的房间ID
    /// </summary>
    public int roomId { get; set; }
    /// <summary>
    /// 房间模板的Prefab名
    /// </summary>
    public string rModelPrefab { get; set; }
    /// <summary>
    /// 房间模板ID
    /// </summary>
    public int rModelId { get; set; }

    public BuildingPart (float _positionX, float _positionY, int _sceneId, int _bpModelId, int _roomId, string _rModelPrefab, int _rModelId)
    {
        position = new Vector2 (_positionX, _positionY);
        sceneId = _sceneId;
        bpModelId = _bpModelId;
        roomId = _roomId;
        rModelPrefab = _rModelPrefab;
        rModelId = _rModelId;
    }

    public BuildingPart(Vector2 _position, int _sceneId, int _bpModelId)
    {
        position = _position;
        sceneId = _sceneId;
        bpModelId = _bpModelId;
        roomId = -1;
        rModelId = -1;
    }
}