///<summary>描述: 用于测试颜色特性
///作者:
///创建时间: 2019/02/17 22:17 
///版本:v1.0
///</summary>using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorBlender : System.Object
{
    public Color SourceColor = Color.white;
    public Color DestColor = Color.white;
    public Color BlendColor = Color.white;
    public Color DefaColor = Color.white;
}
public class ColorUser : MonoBehaviour {

    
    public ColorBlender colorBlender;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
