// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/VacuumShaders/Mesh Thickness Baker" 
{
    SubShader 
	{
		Tags{ "DisableBatching" = "True" }

		Cull Off
		ZWrite On
		Fog {Mode Off}
		
        Pass 
		{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			          
            float4 vert(float4 v:POSITION) : SV_POSITION
		    {
                return UnityObjectToClipPos (v);
            }

            fixed4 frag() : SV_TARGET 
			{
                return 0;
            }

            ENDCG
        }
    }
}
