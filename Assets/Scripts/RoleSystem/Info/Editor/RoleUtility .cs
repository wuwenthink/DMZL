using UnityEditor;
using UnityEngine;

namespace RoleSpace
{   
    public class RoleUtility
    {
        [MenuItem("Utility/Role/Create RoleInfo")]
        static void CreateInfo ()
        {
            // 生成SkillInfo实例
            RoleSystemInfo info = ScriptableObject.CreateInstance<RoleSystemInfo>();
            // 创建文件
            AssetDatabase.CreateAsset(info,RoleSystemInfo.RoleInfoPath);
            // 自动选中文件
            Selection.activeObject = info;
        }
    }
}
