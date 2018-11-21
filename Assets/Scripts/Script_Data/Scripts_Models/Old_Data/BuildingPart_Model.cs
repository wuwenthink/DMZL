// ======================================================================================
// 文 件 名 称：BuildingPart_Model.cs
// 版 本 编 号：v1.1.1
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-10-09     13:09:44
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-10-23 18:47
// ======================================================================================
// 功 能 描 述：建筑组件模板
// ======================================================================================

using UnityEngine;

public class BuildingPart_Model
{
    public int Id { get; private set; }
    public Vector2 Size { get; private set; }
    public string Prefab { get; private set; }

    public BuildingPart_Model (int _Id, string _Size, string _Prefab)
    {
        Id = _Id;

        string [] reg = _Size.Split (',');
        Size = new Vector2 (int.Parse (reg [0]), int.Parse (reg [1]));

        Prefab = _Prefab;
    }
}