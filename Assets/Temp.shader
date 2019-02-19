Shader "Custom/Temp" 
{
	Properties
	{
		_Color("Main Color",Color)=(1,1,1,1)
		_MainTex("Base (RGB) Trans (A)",2D) = "White"{}
		_BlendTex("Blend (RGB)",2D)="White"
	}
	SubShader
	{
			Tags{"Queue"="Geometry-9" "IgnoreProject"="True" "RenderType"="Transparent"}
			Lighting Off
			LOD 200
			Blend  Srcalpha oneminussrcalpha
			CGPROGRAM 
				#pragma surface surf Lambert
				uniform fixed4 _Color
				uniform sampler2D _MainTex
				uniform sampler2D _BlendTex
				//放 UV
				struct Input
				{
					float2 _MainTexUV;
				};

				void surf(Input IN, SurfaceOutput o)
				{
					//取得 texture
					fixed4 c1=tex2D(_MainTex,IN._MainTexUV);
					fixed4 c2=tex2D(_BlendTex,IN._MainTexUV);

					//混合
					fixed4 main=c1.rgba*(1-c2.a);
					fixed4 blendedoutput=c2.rgba*c2.a;
					o.Albedo=(main.rgb+blendedoutput.rgb) * _Color;
					o.Alpha =main.a + blendedoutput.a ;				
				}
			ENDCG
	}
	Fallback "Transpaent / VertexLit"
}
