Shader "NCat/PostProcessingExample"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_CenterOffset ("Center Offset", Vector) = (0,0,0,0)
		_AberrationSampleCount ("Aberration Sample Count", Range(0, 24)) = 0
		_AberrationStrength ("Aberration Strength", Range(0, 1)) = 0
	}
	SubShader
	{
		tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"IgnoreProjector" = "True"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float2 _CenterOffset;
			float _AberrationStrength;
			int _AberrationSampleCount;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv;
				fixed4 col = tex2D(_MainTex, uv);
				float dir = pow(length(uv + _CenterOffset - 0.5), 2);
				for (int j = 0; j < _AberrationSampleCount; j++)
				{
					float strength = j / (float)_AberrationSampleCount * _AberrationStrength * 0.1;
					strength *= dir;
					col.r += tex2D(_MainTex, uv + float2(strength, 0)).r;
					col.g += tex2D(_MainTex, uv).g;
					col.b += tex2D(_MainTex, uv + float2(-strength, 0)).b;
				}
				col.rgb /= _AberrationSampleCount + 1;
				return col;
			}
			ENDCG
		}
	}
}