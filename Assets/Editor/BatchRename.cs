///<summary>描述: 批量修改名字
///作者:唐泽彬
///创建时间: 2019/02/17 20:16 
///版本:v1.0
///</summary>using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BatchRename :  ScriptableWizard
{
    public string BaseName = "MyObject_";

    public int StartNumber = 0;

    public int Increment = 1;

    [MenuItem("Edit/Batch Rennam")]
     static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Batch Rename", typeof(BatchRename), "Rename","Apply");
     //   DisplayWizard<BatchRename>("Button");

    }
    private void OnWizardOtherButton()
    {
        Debug.Log(Selection.objects);
    }
    private void OnWizardCreate()
    {
        if (Selection.objects==null)
        {
            return;
        }

        int i=0;
        foreach (var item in Selection.gameObjects)
        {
            item.name = BaseName + StartNumber+ (i++)*Increment;
        }
 
        //for (int i = 0; i < Selection.objects.Length; i++)
        //{
        //    Selection.objects[i].name = BaseName + (i * Increment + StartNumber);    
        //}

    }
    private void OnEnable()
    {
       
    }
    private void OnSelectionChange()
    {
        UpdateSelectionHelper();
        Debug.Log(Selection.objects.Length);
     
        Debug.Log("Selection activeContex"+Selection.activeContext);
        Debug.Log("Selection activeObj:+" + Selection.activeObject.name+"类型:"+Selection.activeObject.GetType());
        Debug.Log("Path:" + AssetDatabase.GetAssetPath(Selection.activeObject));
          //在选择的 物体的时候  调用
    }

    void UpdateSelectionHelper()
    {
        if (Selection.objects!=null)
        {
            helpString = Selection.gameObjects.Length.ToString();

        }
         
    }
}
