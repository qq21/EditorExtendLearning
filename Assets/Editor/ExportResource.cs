///<summary>描述: AB包的打包工具  可以当个打包，多个打包 也可以 全部打包
///作者:唐泽彬
///创建时间: 2019/02/18 15:07 
///版本:v1.0
///</summary>using System.Collections;
///


using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ExportResource {
    [MenuItem("Assets/Build All AssetBundle")]
    static void ExoportResource()
    {
        //调出   保存面板
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0)
        {
            //打包 选中的 资源 文件
            Object[] selection = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }
    }
    [MenuItem("Assets/Build AssetBundle Using BuldMap")]
    static void BuildMapABs() 
    {
        AssetBundleBuild[] buildMap = new AssetBundleBuild[2];

        buildMap[0].assetBundleName = "enemyBundle";
        string[] enemyAssets = new string[2];
        enemyAssets[0] = "xxx/xx/xxx";
        enemyAssets[1] = "xx/xx/xxx";
        buildMap[0].assetNames = enemyAssets;
        buildMap[1].assetBundleName = "herobundle";

        string[] heroAssets = new string[1];
        heroAssets[0] = "char_hero_beanMan";
        buildMap[1].assetNames = heroAssets;

        BuildPipeline.BuildAssetBundles("Assets/ABs", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
     
}
