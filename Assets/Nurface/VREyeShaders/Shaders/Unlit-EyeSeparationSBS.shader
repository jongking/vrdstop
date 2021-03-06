// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "EyeSeparation/Unlit_TextureVR_SBS" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "black" {}
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				UNITY_VERTEX_OUTPUT_STEREO
			};

			struct Input
			{
				float3 worldPos;
				float3 worldNormal;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _EyeTransformVector;
			int _ShowType;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				if (_ShowType == 1) {
					o.texcoord = (o.texcoord * _EyeTransformVector.wz + _EyeTransformVector.yx);
				}
				else if (_ShowType == 2) {
					o.texcoord = (o.texcoord * _EyeTransformVector.wz * float2(2, 1) + _EyeTransformVector.yx);
				}
				else if (_ShowType == 3) {
					o.texcoord = (o.texcoord * _EyeTransformVector.zw + _EyeTransformVector.xy);
				}
				else if (_ShowType == 4) {
					o.texcoord = (o.texcoord * _EyeTransformVector.zw * float2(2, 1) + _EyeTransformVector.xy);
				}
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{				
					fixed4 col = tex2D(_MainTex, i.texcoord);
					UNITY_APPLY_FOG(i.fogCoord, col);
					UNITY_OPAQUE_ALPHA(col.a);
					return col;
			}
		ENDCG
	}
}

}
