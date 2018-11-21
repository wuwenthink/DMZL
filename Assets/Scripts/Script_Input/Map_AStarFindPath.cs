using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map_AStarFindPath : MonoBehaviour {
	private Map_Grid grid;

	// Use this for initialization
	void Start () {
		grid = GetComponent<Map_Grid> ();
	}
	
	// Update is called once per frame
	void Update () {
		FindingPath (grid.player.position, grid.destPos.position);
	}

    /// <summary>
    ///  A*寻路
    /// </summary>
    /// <param name="s">起始点</param>
    /// <param name="e">结束点</param>
    void FindingPath(Vector3 s, Vector3 e) {
		Map_Grid.NodeItem startNode = grid.getItem (s);
		Map_Grid.NodeItem endNode = grid.getItem (e);
        //建立开放 open list（开放目标点列表） 和 close list（排除目标点列表）。
        List<Map_Grid.NodeItem> openSet = new List<Map_Grid.NodeItem> ();
		HashSet<Map_Grid.NodeItem> closeSet = new HashSet<Map_Grid.NodeItem> ();
        //添加起始点到开放目标点列表
        openSet.Add (startNode);

		while (openSet.Count > 0) {
            //获得当前点（起始点）
			Map_Grid.NodeItem curNode = openSet [0];
            //遍历开放 open list ，查找 F 值最小的节点，把它作为当前要处理的节点。
            for (int i = 0, max = openSet.Count; i < max; i++) {
				if (openSet [i].fCost <= curNode.fCost &&
				    openSet [i].hCost < curNode.hCost) {
					curNode = openSet [i];
				}
			}
            //把这个节点移到 close list 。
            openSet.Remove (curNode);
			closeSet.Add (curNode);

			// 找到目标节点，生成路径，并结束循环，否则继续找
			if (curNode == endNode) {
				generatePath (startNode, endNode);
				return;
			}

			// 判断周围节点，选择一个最优的节点
			foreach (var item in grid.getNeibourhood(curNode)) {
				// 如果是墙或者已经在关闭列表中，跳过
				if (item.isWall || closeSet.Contains (item))
					continue;
				// 计算当前相邻节点与开始节点距离
				int newCost = curNode.gCost + getDistanceNodes (curNode, item);
				// 如果距离更小，或者原来不在开始列表中
				if (newCost < item.gCost || !openSet.Contains (item)) {
					// 更新与开始节点的距离
					item.gCost = newCost;
					// 更新与终点的距离
					item.hCost = getDistanceNodes (item, endNode);
					// 更新父节点为当前选定的节点
					item.parent = curNode;
					// 如果节点是新加入的，将它加入打开列表中
					if (!openSet.Contains (item)) {
						openSet.Add (item);
					}
				}
			}
		}

		generatePath (startNode, null);
	}

	/// <summary>
    /// 生成路径
    /// </summary>
    /// <param name="startNode">起始点</param>
    /// <param name="endNode">结束点</param>
	void generatePath(Map_Grid.NodeItem startNode, Map_Grid.NodeItem endNode) {
		List<Map_Grid.NodeItem> path = new List<Map_Grid.NodeItem>();
		if (endNode != null) {
			Map_Grid.NodeItem temp = endNode;
			while (temp != startNode) {
				path.Add (temp);
				temp = temp.parent;
			}
			// 反转路径
			path.Reverse ();
		}
		// 更新路径
		grid.updatePath(path);
	}

    /// <summary>
    ///  获取两个节点之间的距离（曼哈顿估价法：4方向）
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    int getDistanceNodes(Map_Grid.NodeItem a, Map_Grid.NodeItem b)
    {
        return Mathf.Abs(a.x - b.x) * 10 + Mathf.Abs(a.y - b.y) * 10;
    }

    /// <summary>
    /// 获取两个节点之间的距离（对角线估价法：8方向）
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    int getDistanceNodes_angles(Map_Grid.NodeItem a, Map_Grid.NodeItem b)
    {
        int cntX = Mathf.Abs(a.x - b.x);
        int cntY = Mathf.Abs(a.y - b.y);
        // 判断到底是那个轴相差的距离更远
        if (cntX > cntY)
        {
            return 14 * cntY + 10 * (cntX - cntY);
        }
        else
        {
            return 14 * cntX + 10 * (cntY - cntX);
        }
    }


    ////对角线估价法
    //private function diagonal(node:Node):Number
    //{
    //    var dx:Number=Math.abs(node.x - _endNode.x);
    //    var dy:Number=Math.abs(node.y - _endNode.y);
    //    var diag:Number=Math.min(dx, dy);
    //    var straight:Number=dx + dy;
    //    return _diagCost* diag + _straightCost* (straight - 2 * diag);
    //}

    ////曼哈顿估价法
    //    private function manhattan(node:Node):Number
    //{
    //    return Math.abs(node.x - _endNode.x) * _straightCost + Math.abs(node.y + _endNode.y) * _straightCost;
    //}

    ////几何估价法
    //private function euclidian(node:Node):Number
    //{
    //    var dx:Number=node.x - _endNode.x;
    //    var dy:Number=node.y - _endNode.y;
    //    return Math.sqrt(dx* dx + dy* dy) * _straightCost;
    //}



}
