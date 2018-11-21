// ====================================================================================== 
// 文件名              ：    AddFileHeadComment.cs                                                         
// 版本号              ：    v1.0.0.0                                                  
// 作者                  ：    xic                                                          
// 创建日期           ：    2017-9-19                                           
// 最后修改日期     ：    2017-9-19                                                            
// ====================================================================================== 
// 功能描述           ：     批量制作所有子物体为预制体                                                                 
// ======================================================================================

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BatchPrefabAllChildren : MonoBehaviour 
{

    [MenuItem("Tools/BatchPrefab All Children")]
public static void BatchPrefab()
    {
    Transform tParent = ((GameObject)Selection.activeObject).transform;


    Object tempPrefab;
    foreach(Transform t in tParent)
        {
        tempPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefab/Prefab_Ground/Prefab_Indoor/" + t.gameObject.name + ".prefab");
        tempPrefab = PrefabUtility.ReplacePrefab(t.gameObject, tempPrefab);
    }
}
}
