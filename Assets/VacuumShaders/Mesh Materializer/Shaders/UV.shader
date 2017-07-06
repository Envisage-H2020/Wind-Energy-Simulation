// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/VacuumShaders/Mesh Materializer/UV"
{
	SubShader 
	{
		Cull Off

        Pass 
		{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

			#pragma multi_compile V_MM_UVBAKER_COLORSPACE_NONE V_MM_UVBAKER_COLORSPACE_GAMMA V_MM_UVBAKER_COLORSPACE_LINEAR

            #include "UnityCG.cginc"

            struct vertOut 
			{ 
                float4 pos : SV_POSITION;
                
				fixed4 color : COLOR;
            };

            vertOut vert(appdata_full v)
			{
                vertOut o;
                o.pos = UnityObjectToClipPos(v.vertex);
                
				o.color = v.color;

				#ifdef V_MM_UVBAKER_COLORSPACE_GAMMA
					o.color.rgb = pow(o.color.rgb, 0.454545);
				#endif
				#ifdef V_MM_UVBAKER_COLORSPACE_LINEAR
					o.color.rgb = pow(o.color.rgb, 2.2); 
				#endif

                return o;
            }

            fixed4 frag(vertOut i) : SV_Target 
			{
                
                return i.color;
            }

            ENDCG
        }
    }
}