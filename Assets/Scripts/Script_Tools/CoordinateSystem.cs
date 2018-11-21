// ====================================================================================== 
// 文件名         ：    CoordinateSystem.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-7-22                                                                  
// 最后修改日期   ：    2017-7-22                                                            
// ====================================================================================== 
// 功能描述       ：    构建坐标系以及各种坐标转换，以左上角为原点                                                                  
// ====================================================================================== 


using UnityEngine;
/// <summary>
/// 坐标系的类
/// </summary>
public class CoordinateSystem
{
    /// <summary>
    /// 存放当前场景中横轴上区域总数
    /// </summary>
    private int scenePart_TotalNum_X = 2;
    /// <summary>
    /// 存放当前场景中纵轴上区域总数
    /// </summary>
    private int scenePart_TotalNum_Y = 2;

    /// <summary>
    /// 坐标系的初始化方法，每次加载一个场景之后new一个坐标系然后调用此方法
    /// </summary>
    /// <param name="scenePart_TotalNum_X">当前场景中横轴上区域总数</param>
    /// <param name="scenePart_TotalNum_Y">当前场景中纵轴上区域总数</param>
    public void CoordinateInit(int scenePart_TotalNum_X, int scenePart_TotalNum_Y)
    {
        this.scenePart_TotalNum_X = scenePart_TotalNum_X;
        this.scenePart_TotalNum_Y = scenePart_TotalNum_Y;
    }

    /// <summary>
    /// 将直接获取到的世界坐标转换为场景坐标
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public Vector2 GetPoint(Vector3 vector)
    {
        int height = 60 * (scenePart_TotalNum_Y - 1);
        return new Vector2(vector.x, height - vector.y);
    }

    /// <summary>
    /// 将世界坐标转换为区域中的小坐标
    /// </summary>
    /// <param name="vector">获取到的角色的世界坐标</param>
    /// <param name="scenePart_Id">场景子图的编号（从0开始，需从id转换）</param>
    /// <returns></returns>
    public Vector2 WorldToPartPoint(Vector3 vector, int scenePart_Id)
    {
        float x = vector.x - GetZeroPointInPart(scenePart_Id).x;
        float y = vector.y - GetZeroPointInPart(scenePart_Id).y;
        return new Vector2(x, y);
    }

    /// <summary>
    /// 计算每一个区域的零点的世界坐标
    /// </summary>
    /// <param name="scenePart_Id">场景子图的编号（从0开始，需从id转换）</param>
    /// <returns></returns>
    public Vector2 GetZeroPointInPart(int scenePart_Id)
    {
        float x = (scenePart_Id % scenePart_TotalNum_X) * 60;
        float y = (scenePart_Id % scenePart_TotalNum_Y) * 60;
        return new Vector2(x, y);
    }

    /// <summary>
    /// 计算角色当前所处的区域的编号
    /// </summary>
    /// <param name="vector">获取到的角色的世界坐标</param>
    /// <returns></returns>
    public int GetCurrScenePartId(Vector3 vector)
    {
        return (int)vector.x / 60 + scenePart_TotalNum_X * (scenePart_TotalNum_Y - (int)vector.y / 60 - 1);
    }
}
