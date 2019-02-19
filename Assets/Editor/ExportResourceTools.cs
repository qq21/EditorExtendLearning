///<summary>描述:  打包  面板 可以 选择 资源 打包
///作者: 唐泽彬
///创建时间: 2019/02/18 17:08 
///版本:v1.0
///</summary>using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
[System.Serializable]
public class ExportResourceTools : ScriptableWizard {


    Dictionary<string, GameObject> items;

    [Tooltip("存放ab文件的位置")]
    public string AssetBundlePath;

    [Tooltip("在此目录下打包资源")]
    public string AssetPath;


    [System.Serializable]
    public struct Item
    {
        public string path;
        public GameObject item;
    }

    [Tooltip("选中的要打包的物品")]
    public Item[] item;
    [Tooltip("选中目录下的资源")]
    public Item[] itemAb;

    [MenuItem("Tools/Asstetbundo Build")]
    static void CreateWizard()
    {
      
        DisplayWizard<ExportResourceTools>("打包工具", "应用", "选择要存放的位置");
    }
     
    protected override bool DrawWizardGUI()
    {
        if (GUI.Button(new Rect(120, 15, 30, 20), "选择"))
        {
           AssetBundlePath = EditorUtility.SaveFolderPanel("选择AB包存放的地址", "D:", "");
        }

        if (GUI.Button(new Rect(120, 40, 30, 20), "选择"))
        {
            AssetPath = EditorUtility.SaveFolderPanel("选择要打包的资源目录",Application.dataPath, "");

            Debug.Log("Assets"+AssetPath.Replace(Application.dataPath, ""));
            Object[] objects = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetPath);      //LoadAllAssetsAtPath(AssetPath);
           
            Debug.Log(objects.Length);

            //AssetDatabase.LoadAssetAtPath();
            //TODO: 获取目录下所有文件  取得 路径, 加载出来
            string[] files= Directory.GetFiles(AssetPath);

   
            foreach (var item in files)
            {
                if (!item.EndsWith(".meta"))
                {
                 
                   
                }
     
            }
            itemAb = new Item[objects.Length];
            AddGameObjToItem(itemAb, objects);

        }

        return base.DrawWizardGUI();

    }


    void AddGameObjToItem(Item[] temp,Object[] o)
    {
        int i = 0;
        foreach (var item in  o)
        {
            if (item.GetType() == typeof(GameObject))
            {
                temp[i].item = item as GameObject;
                temp[i].path = AssetDatabase.GetAssetPath(item.GetInstanceID());
                i++;
            }
        }
    }
    private void OnSelectionChange()
    {
        Debug.Log(Selection.objects.Length);

        item = new Item[Selection.objects.Length];
         
        helpString = "一共选中" + Selection.objects.Length+"个物体";
        AddGameObjToItem(item, Selection.objects);
     
        
    }

    private void OnWizardOtherButton()
    {
        
    }
    private void OnWizardCreate()
    {
        
    }
    
}
