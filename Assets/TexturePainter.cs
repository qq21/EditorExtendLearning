using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 笔刷 功能
/// </summary>
public class TexturePainter : MonoBehaviour {

	// Use this for initialization
	
	public Texture2D BrushTexture=null;
	public int  SurfaceTextureWidth=512;
	public int  SurfaceTextureHeight=512;
	public Texture2D SurfaceTexture=null;

	public Material DestMat=null ;

      Renderer renderer;
	void Start () 
	{
        
       // renderer= GetComponent< MeshRenderer>();
		SurfaceTexture=new Texture2D(SurfaceTextureWidth,SurfaceTextureHeight,TextureFormat.RGBA32,false);	
		
		Color[] pix=SurfaceTexture.GetPixels();
		for (int i = 0; i < pix.Length; i++)
		{
			 pix[i]=new Color(0,0,0,0);
			 SurfaceTexture.SetPixels(pix);
			 SurfaceTexture.Apply();
		}
        //set  main texture
       //renderer.material.mainTexture = SurfaceTexture;

        if (DestMat)
        {
            DestMat.SetTexture("_BlendTex", SurfaceTexture);
        }
	     
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))  
        {
            RaycastHit hit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
            {
                return;
            }
            Renderer tempR = hit.collider.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;

            if (tempR == null|| tempR.sharedMaterial==null|| tempR.sharedMaterial.mainTexture==null||meshCollider==null)
            {
                return;

            }

            //获取点击 物体的UV 坐标
            Vector2 pixUv = hit.textureCoord;           
            pixUv.x *= tempR.material.mainTexture.width;
            pixUv.y *= tempR.material.mainTexture.height;

            //更新贴图的 中部UV 坐标  为点击指标 排列笔刷贴图
            pixUv.x -= BrushTexture.width / 2;
            pixUv.y -= BrushTexture.height / 2;

            //限制 像素值 在0 和width间 和高度
            pixUv.x = Mathf.Clamp(pixUv.x, 0, tempR.material.mainTexture.width);
            pixUv.y = Mathf.Clamp(pixUv.y, 0, tempR.material.mainTexture.height);


            PaintSourceToDestTexture(BrushTexture, tempR.material.mainTexture as Texture2D, (int)pixUv.x, (int)pixUv.y);

        }
	}
    public static void PaintSourceToDestTexture(Texture2D Source,Texture2D Dest,int Left,int Top)
    {
        //获取 颜色像素
        Color[] sourcePixels = Source.GetPixels();
        //获取 目标像素
        Color[] destPiels = Dest.GetPixels();
        for (int x = 0; x < Source.width; x++)
        {
            for (int y = 0; y <Source.height; y++)
            {
                //获取 原 像素的颜色
                Color Pixel = GetPixelFromArray(sourcePixels,x,y,Source.width);

                //获取目标偏移
                int DestOffsetX = Left + x;
                int DestOffsetY = Top + y;

                if (DestOffsetX < Dest.width && DestOffsetY < Dest.height)
                {
                    SetPixelInArray(destPiels, DestOffsetX, DestOffsetY, Dest.width, Pixel, true);
                }
            }
        }
        //更新 目标 贴图
        Dest.SetPixels(destPiels);
        Dest.Apply();
        
    }
 
    //从像素数组中读取 像素
    public static Color GetPixelFromArray(Color[] Pixels,int x,int Y,int Width )
    {
        return Pixels[x + Y * Width];
    }

    //从像素 数组中设置(混合) 颜色 
    public static void SetPixelInArray(Color[] Pixels,int x,int y ,int Width,Color newColor,bool Blending=false)
    {
        if (!Blending)
        {
            Pixels[x + y * Width] = newColor;
        }
        else
        {
            Color c = Pixels[x + y * Width] * (1.0f - newColor.a);
            Color Blend = newColor * newColor.a;
            Color Result = c + Blend;
            float Alpha = c.a + Blend.a;
            Pixels[x + y * Width] = new Color(Result.r, Result.g, Result.b, Alpha);
        }
    }
}
