using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
public class CreateQuad : UnityEditor.ScriptableWizard
{
    public enum AnchorPoint
    {
        TopLeft,
        TopMiddle,
        TopRight,
        LeftMiddle,
        RightMiddle,
        BottomLeft,
        BottomMiddle,
        BottomRight,
        Custom,
        Center,
    }




    #region about Quad's property
    public float Width = 1.0f;
    public float Height = 1.0f;

    //pixels
    public float AnchorX = 1.0f;
    public float AnchorY = 1.0f;
    public AnchorPoint anchorPoint = AnchorPoint.TopMiddle;
    #endregion


    public string MeshName = "Quad";
    public string GameObjectName = "Plane_Object";
    public string AssetFolder = "Assets";





    [MenuItem("GameObject/Other/Custom Plane")]
    static void CreateWizard()
    {
        DisplayWizard<CreateQuad>("Create Plane");
    }
    public void OnInspectorUpdate()
    {
        // Get Position of Quad's Anchor 
        switch (anchorPoint)
        {
            case AnchorPoint.TopLeft:
                AnchorX = Width * 0;
                AnchorY = Height * 1;
                break;
            case AnchorPoint.TopMiddle:
                AnchorX = Width * 0.5f;
                AnchorY = Height * 1;
                break;
            case AnchorPoint.TopRight:
                AnchorX = Width * 1;
                AnchorY = Height * 1;
                break;
            case AnchorPoint.LeftMiddle:
                AnchorX = 0*Width;
                AnchorY = 0.5f * Height;
                break;
            case AnchorPoint.RightMiddle:
                AnchorX = 1 * Width;
                AnchorY = 0.5f * Height;
                break;
            case AnchorPoint.BottomLeft:
                AnchorX = 0 * Width;
                AnchorY = 0 * Height;
                break;
            case AnchorPoint.BottomMiddle:
                AnchorX = 0.5f * Width;
                AnchorY = 0 * Height;
                break;
            case AnchorPoint.BottomRight:
                AnchorX = 1 * Width;
                AnchorY = 0 * Height;
                break;
            case AnchorPoint.Center:
                AnchorX = 0.5f * Width;
                AnchorY = 0.5f * Height;
                break;
            default: 

                break;
        }
    }


    private void OnEnable()
    {
        OnSelectionChange();
    }


    private void OnWizardCreate()
    {
        Vector3[] vector3s = new Vector3[4];

        Vector2[] uvs = new Vector2[4];

        int[] Trangles = new int[6];

        //B L
        vector3s[0].y = -AnchorY;
        vector3s[0].x = -AnchorX;
        //B R
        vector3s[1].x = vector3s[0].x + Width;
        vector3s[1].y = vector3s[0].y;    
        //T L
        vector3s[2].y = vector3s[0].y + Height;
        vector3s[2].x = vector3s[0].x;
        //T R 
        vector3s[3].x = vector3s[0].x+Width;
        vector3s[3].y = vector3s[0].y + Height;

        //uv
        //B L
        uvs[0].x = 0;
        uvs[0].y = 0;
        //B R
        uvs[1].x = 1;
        uvs[1].y = 0;
        //T L
        uvs[2].x = 0;
        uvs[2].y = 1;
        //T R
        uvs[3].x = 1;
        uvs[3].y = 1;

        //三角面
        Trangles[0] = 3;
        Trangles[1] = 1;
        Trangles[2] = 2;
        Trangles[3] = 2;
        Trangles[4] = 1;
        Trangles[5] = 0;

        //Generate Mesh
        Mesh mesh = new Mesh();
        mesh.name = MeshName;
        mesh.vertices = vector3s;
        mesh.triangles = Trangles;
        mesh.RecalculateNormals();
        AssetDatabase.CreateAsset(mesh, AssetDatabase.GenerateUniqueAssetPath(AssetFolder + "/" + MeshName + ".asset"));
        AssetDatabase.SaveAssets();

        GameObject plane = new GameObject(GameObjectName);
        MeshFilter meshFilter = plane.AddComponent<MeshFilter>();
        plane.AddComponent<MeshRenderer>();

        meshFilter.sharedMesh = mesh;
        mesh.RecalculateBounds();

        plane.AddComponent<BoxCollider>();
    }
    private void OnSelectionChange()
    {

        if (Selection.objects!=null&&Selection.objects.Length==1)
        {
            AssetFolder = Path.GetDirectoryName(AssetDatabase.GetAssetPath(Selection.objects[0]));
        }
    }

}
