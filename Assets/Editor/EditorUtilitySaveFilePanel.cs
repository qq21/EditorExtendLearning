using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class EditorUtilitySaveFilePanel : MonoBehaviour {
    [MenuItem("Examples/Save Texture to file")]
    static void Apply()
    {
        Texture2D texture = Selection.activeObject as Texture2D;
        
        if (texture == null)
        {
            EditorUtility.DisplayDialog(
                "Select Texture",
                "You Must Select a Texture first!",
                "Ok");
            return;
        }
        Debug.Log(AssetDatabase.GetAssetPath( Selection.activeObject.GetInstanceID()));

        var path = EditorUtility.SaveFilePanel(
            "Save texture as PNG",
            "D:",
            texture.name + ".png ",
            "png");

        if (path.Length != 0)
        {
            var pngData = texture.EncodeToPNG();
            if (pngData != null)
                File.WriteAllBytes(path, pngData);
        }

    }
}
