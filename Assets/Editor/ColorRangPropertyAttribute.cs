///<summary>描述:颜色特性
///作者:唐泽彬
///创建时间: 2019/02/17 22:47 
///版本:v1.0
///</summary>using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRangPropertyAttribute : PropertyAttribute
{

    public Color min;
    public Color max;
    public ColorRangPropertyAttribute(float r,float g,float b, float a,float r1,float g1,float b1,float a1)
    {
        this.min = new Color(r, g, b, a);
        this.max = new Color(r1, g1, b1, a1);
    }
    
}
