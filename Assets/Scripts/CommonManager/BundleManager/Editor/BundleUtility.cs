using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// BundleManager编辑器扩展
    /// </summary>
    public class BundleUtility :MonoBehaviour
    {
        const string bundlePath = "Assets/StreamingAssets/" + BundleKey.FileName;

        /// <summary>
        /// 创建bundle文件
        /// </summary>
        [MenuItem("Tools/AssetBundle/Build Bundle")]
        public static void BuildBundle ()
        {
            //如果已经创建过bundle文件则删除之前的文件
            if ( Directory.Exists(bundlePath) )
            {
                DirectoryHelper.DeleteFolder(bundlePath);
            }
           //创建目标路径
            Directory.CreateDirectory(bundlePath);
            //添加Assetbundle
            BuildPipeline.BuildAssetBundles(bundlePath,BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows64);
            //刷新资源面板
            AssetDatabase.Refresh();
            //生成配置文件
            GenerateBundleConfigFile();
            Debug.Log("Bundle创建成功");
        }

        /// <summary>
        /// 生成配置文件
        /// </summary>
       // [MenuItem("Tool/AssetBundle/Generate Bundle Config File")]
        public static void GenerateBundleConfigFile ()
        {
            //清空所有未注销bundle
            AssetBundle.UnloadAllAssetBundles(true);
            //配置文件字典
            Dictionary<string,string> configFile = new Dictionary<string,string>();
            //获取导航Bundle
            AssetBundle mainBundle = AssetBundle.LoadFromFile(bundlePath + "/" + BundleKey.FileName);
            //获取manifest
            AssetBundleManifest manifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            //卸载该Bundle
            mainBundle.Unload(false);
            //获取所有bundle名称
            string[] bundlesName = manifest.GetAllAssetBundles();

            //遍历所有bundle
            foreach ( string bundleName in bundlesName )
            {
                //读取bundle
                AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath+ "/"+ bundleName);
                //获取所有资源地址
                string[] assetPaths = bundle.GetAllAssetNames();
                //遍历所有资源地址
                foreach ( string assetPath in assetPaths )
                {
                    //将资源地址转化为名称
                    string assetName = Path.GetFileNameWithoutExtension(assetPath);
                    //查找是否有重复名字
                    if ( configFile.ContainsKey(assetName) )
                    {
                        Debug.LogError(assetName + "资源名称重复");
                        return;
                    }
                    //将资源名称和bundle名字添加至字典
                    configFile.Add(assetName,bundleName);
                }
            }
            //写入配置文件
            ConfigurationManager.WriteMapToFile(BundleKey.ConfigName,configFile);
              //刷新资源面板
            AssetDatabase.Refresh();
            Debug.Log("生成Asset Bundle配置文件");

        }
    }
}
