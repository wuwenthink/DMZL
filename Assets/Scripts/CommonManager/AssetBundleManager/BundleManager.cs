using UnityEngine;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// AssetBundle管理器
    /// </summary>
    public class BundleManager :Singleton<BundleManager>
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        string FilePath
        {
            get
            {
                string path;
#if UNITY_EDITOR || UNITY_STANDALONE
                path = "file://" + Application.dataPath + "/StreamingAssets/" + BundleKey.FileName + "/";
#elif UNITY_ANDROID
                path = "file://" + Application.dataPath + "/StreamingAssets/" +  BundleKey.FileName+"/";
#elif UNITY_IPHONE
                path ="file://"+ Application.dataPath + "/Raw/"+ BundleKey.FileName+"/";
#endif
                return path;
            }
        }
        //key 资源名称   value:所在bundle名称
        Dictionary<string,string> map;
        /// <summary>资源字典 </summary>
        Dictionary<string,Dictionary<string,Object>> assetDic;

        /// <summary>Bundle依赖</summary>
        AssetBundleManifest manifest;
        /// <summary>依赖bundle字典</summary>
        Dictionary<string,AssetBundle> dependBundleDic;

        protected override void Initialize ()
        {
            //清空所有未注销bundle
            AssetBundle.UnloadAllAssetBundles(true);

            //实例map
            map = new Dictionary<string,string>();   
            //读取配置文件
            ConfigurationManager.LoadConfig(BundleKey.ConfigName,BuildMap);
            //实例资源字典
            assetDic = new Dictionary<string,Dictionary<string,Object>>();

            //获取manifest
            manifest = ReturnBundle(BundleKey.FileName).LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            //实例依赖bundle字典
            dependBundleDic = new Dictionary<string,AssetBundle>();

        }
        void BuildMap ( string line )
        {
            line = line.Trim();//去掉前后空格
            if ( line == "" ) return;//去除空格
            string[] keyValue = line.Split('=');
            map.Add(keyValue[0],keyValue[1]);
        }
        public void Refresh ()
        {
            Initialize();
        }
        /// <summary>
        /// 加载AB包
        /// </summary>
        /// <param name="BundleName">AB包名</param>
        public void LoadBundle ( string BundleName )
        {
            //加载bundle的依赖
            LoadDependBundle(BundleName);
            //获取bundle
            AssetBundle bundle = ReturnBundle(BundleName);
            //获取bundle中所有资源的名字
            Object[] assetObjects = bundle.LoadAllAssets();
            //实例字典
            assetDic[BundleName] = new Dictionary<string,Object>();
            //遍历所有资源放入字典中
            foreach ( Object asset in assetObjects )
            {
                assetDic[BundleName][asset.name] = asset;
            }
            //卸载bundle
            bundle.Unload(false);
            //卸载依赖资源
            ClearDependBundle();
            return;
        }
        /// <summary>
        /// 返回Bundle
        /// </summary>
        /// <param name="BundleName"></param>
        /// <returns></returns>
        AssetBundle ReturnBundle ( string BundleName )
        {
            using ( WWW www = new WWW(FilePath + BundleName) )
            {
                while ( true )
                {
                    if ( www.isDone )
                    {
                        return www.assetBundle;
                    }
                }
            }
        }
        /// <summary>
        /// 加载依赖Bundle
        /// </summary>
        /// <param name="BundleName">Bundle名字</param>
        void LoadDependBundle ( string BundleName )
        {
            //获取依赖文件名
            string[] dependsFile = manifest.GetAllDependencies(BundleName);

            if ( dependsFile.Length > 0 )
            {
                foreach ( string dependFile in dependsFile )
                {
                    //获取依赖bundle
                    AssetBundle dependBundle = ReturnBundle(dependFile);
                    //加载依赖Bundle
                    LoadDependBundle(dependFile);
                    //放入依赖字典中
                    dependBundleDic[dependBundle.name] = dependBundle;
                }
            }
        }
        /// <summary>
        /// 清空依赖Bundle
        /// </summary>
        void ClearDependBundle ()
        {
            foreach ( AssetBundle bundle in dependBundleDic.Values )
            {
                bundle.Unload(false);
            }
            dependBundleDic.Clear();
        }
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T">加载数据类型</typeparam>
        /// <param name="resName"></param>
        /// <returns></returns>
        public T Load<T> ( string resName ) where T : Object
        {
            if ( !map.ContainsKey(resName.ToLower()) )
            {
                Debug.LogError("没有" + resName + "文件");
                return null;
            }

            string bundleName = map[resName.ToLower()];

            //如果资源字典中没有加载bundle，则加载bundle
            if ( !assetDic.ContainsKey(bundleName) )
            {
                LoadBundle(bundleName);
            }
            return assetDic[bundleName][resName] as T;
        }
        /// <summary>
        /// 卸载bundle
        /// </summary>
        public void UnLoadBundle ( string bundleName )
        {
            assetDic[bundleName].Clear();
            //释放内存
            System.GC.Collect();
        }
        /// <summary>
        /// 卸载所有bundle
        /// </summary>
        public void UnLoadAllBundle ()
        {
            foreach ( string bundleName in assetDic.Keys)
            {
                UnLoadBundle(bundleName);
            }
            assetDic.Clear();
        }
    }
}