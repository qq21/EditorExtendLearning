using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
public class AutoSummerToolWindow : EditorWindow {

    // Use this for initialization

    string youName;
     [MenuItem("Author/AuthorInfo")]
    public static void ShowAuthor()
    {
        //创建一个 MyWindow的可停靠窗口
        //EditorWindow.GetWindow(typeof(AutoSummerToolWindow));
        //Unility 是否为 工具窗口 (不可停靠)
        //EditorWindow.GetWindow(typeof(AutoSummerToolWindow), false, "AuthorInfo Window");
        GetWindowWithRect(typeof(AutoSummerToolWindow), new Rect(0, 0, 200, 200));
    }

    private void OnGUI()
    {
        Debug.Log("OnGUI");
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);//粗体     
         youName = EditorGUILayout.TextField("作者:", youName);
        if (GUILayout.Button(new GUIContent("确定")))
        {
            //string tempPath = File.ReadAllText(Application.dataPath + "/Editor/Recent.txt");

            //string comtent = File.ReadAllText(tempPath);
 
            //Debug.Log(comtent);
            //if (!comtent.Contains(youName))
            //{
            //    if (!comtent.Contains(AutoSummerTools.annotationStr))
            //    {
            //        AutoSummerTools.youName = youName;
            //        AutoSummerTools.ChangeFileSummer("Done", tempPath,youName);
            //        Debug.Log(AutoSummerTools.youName);
            //    }
            //    else
            //    {
            //        Debug.Log("1");
            //        comtent = comtent.Replace(AutoSummerTools.youName, youName);
            //        File.WriteAllText(tempPath, comtent);
            //        Debug.Log(comtent);

            //    }


            //}


          
        
 
        }



        EndWindows();

    }

    public void Callback(object obj)
    {
        Debug.Log("Selected: " + obj);
    }
}
