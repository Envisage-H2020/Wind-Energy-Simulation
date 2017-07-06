// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// VacuumShaders 2015
// https://www.facebook.com/VacuumShaders

Shader "Custom/Unlit (Shadows)"
{ 
	Properties 
	{
		_Color("Color", color) = (1, 1, 1, 1)
		_MainTex("Texture", 2D) = ""{}
	}

    SubShader 
    {
		Tags { "RenderType"="Opaque"  }

		Pass
	    {			
			Name "FORWARD"
			Tags { "LightMode" = "ForwardBase" }  
		 
            CGPROGRAM 
		    #pragma vertex vert
	    	#pragma fragment frag
	    	#pragma fragmentoption ARB_precision_hint_fastest		 
			 
			#pragma multi_compile_fwdbase
			#define UNITY_PASS_FORWARDBASE
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
						

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;


			struct vInput
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct vOutput
			{ 
				float4 pos : SV_POSITION;	
				float2 texcoord : TEXCOORD0;
				
				LIGHTING_COORDS(3,4)		
			};

			vOutput vert(vInput v)
			{
				vOutput o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;


				TRANSFER_VERTEX_TO_FRAGMENT(o);

				return o;
			}

			fixed4 frag(vOutput i) : SV_Target 
			{		
				return tex2D(_MainTex, i.texcoord) * _Color * LIGHT_ATTENUATION(i);
			}


			ENDCG 

    	} //Pass		
		
		
		
		// ---- shadow caster pass:
		Pass 
		{
			Name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }
			Fog {Mode Off}
			ZWrite On ZTest LEqual Cull Off
			Offset 1, 1

			CGPROGRAM
			#pragma vertex vert_surf
			#pragma fragment frag_surf
			#pragma multi_compile_shadowcaster
			#include "HLSLSupport.cginc"
			#include "UnityShaderVariables.cginc"
			#define UNITY_PASS_SHADOWCASTER
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

	
			struct v2f_surf 
			{
			  V2F_SHADOW_CASTER;
			};

			v2f_surf vert_surf (appdata_full v) 
			{
			    v2f_surf o;
			    TRANSFER_SHADOW_CASTER(o)
			    
				return o;
			}
	
			fixed4 frag_surf (v2f_surf IN) : SV_Target 
			{
				SHADOW_CASTER_FRAGMENT(IN)
			}	

			ENDCG
		}


		// ---- shadow screenspace collector pass:
		Pass 
		{
			Name "ShadowCollector"
			Tags { "LightMode" = "ShadowCollector" }
			Fog {Mode Off}
			ZWrite On ZTest LEqual

			CGPROGRAM
			#pragma vertex vert_surf
			#pragma fragment frag_surf
			#pragma multi_compile_shadowcollector
			#include "HLSLSupport.cginc"
			#include "UnityShaderVariables.cginc"
			#define UNITY_PASS_SHADOWCOLLECTOR
			#define SHADOW_COLLECTOR_PASS
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct v2f_surf 
			{
				V2F_SHADOW_COLLECTOR;
			};

			v2f_surf vert_surf (appdata_full v) 
			{
				v2f_surf o;
				TRANSFER_SHADOW_COLLECTOR(o)
    
				return o;
			}

			fixed4 frag_surf (v2f_surf IN) : SV_Target 
			{
				SHADOW_COLLECTOR_FRAGMENT(IN)
			}

			ENDCG
		}	

        
    } //SubShader

	Fallback "VertexLit"

} //Shader
