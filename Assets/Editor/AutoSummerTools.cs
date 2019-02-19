using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class AutoSummerTools : UnityEditor.AssetModificationProcessor {

    

    public static string annotationStr =
     "///<summary>" +
     "描述:\r\n" +
     "///" + "作者:"+"\r\n" +
     "///" + "创建时间: #CreateTime# \r\n" +
     "///" + "版本:v1.0\r\n" +
     "///</summary>";
    
    public static void ChangeFileSummer(object o,string path)
    {

        Debug.Log(path);
        if (File.Exists(path))
        {
            if (path.EndsWith(".cs"))
            {
                annotationStr = annotationStr.Replace("#CreateTime#", System.DateTime.Now.ToString("yyy/MM/dd HH:ss"));

               
                annotationStr += File.ReadAllText(path);
                
                File.WriteAllText(path, annotationStr);
                Debug.Log(o);
            }
        }
    }
 


    public static void OnWillCreateAsset(string name)
    {
     

        string path = name.Replace(".meta", "");
        Debug.Log(path);
        GenericMenu genericMenu = new GenericMenu();

        Debug.Log(Application.dataPath);
        if (File.Exists(Application.dataPath + "/Editor/Recent"))
        {
            File.WriteAllText(Application.dataPath + "/Editor/Recent.txt", path);
        }
        else
        {
            File.Create(Application.dataPath + "/Editor/Recent");
            File.WriteAllText(Application.dataPath + "/Editor/Recent.txt", path);
        }

        genericMenu.AddItem(new GUIContent("确定"), false,
        (object o) 
        =>
            {
                ChangeFileSummer(o,path);
            } ,"Done");

        genericMenu.AddSeparator("");
        genericMenu.AddItem(new GUIContent("取消"), false, () => { return; });
        genericMenu.AddItem(new GUIContent("修改", "呼出作者菜单"), false, () => {
          
        Debug.Log(path);
        EditorWindow.GetWindow(typeof(AutoSummerToolWindow));
           
        });
        genericMenu.ShowAsContext();
    }



    public static void OnGUI()
    {
        EditorGUILayout.TextField("name");
    }
    public void Callback(object obj)
    {
        Debug.Log("Selected: " + obj);
    }
}
