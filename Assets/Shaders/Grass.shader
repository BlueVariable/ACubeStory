Shader "BLUE/Grass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GrassColor ("Grass Color", color) = (0, 1, 0, 1)
        _WindDirection("Wind Direction", Vector) = (0, 0, 0)
    	_WindSpeed("Wind speed", Range(0, 0.1)) = 0.05
    	_GrassHeight ("Grass Height", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
		LOD 200
		CULL off 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct VertexInput
            {
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;   
                float4 vertex : POSITION;
            };

            struct VertexOutput
            {
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 position : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _GrassColor;
            float3 _WindDirection;
            half _GrassHeight;
			half _WindSpeed;
            
            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
				o.normal = v.normal;
				o.uv = v.uv;

            	float grassHeight = (_GrassHeight * 0.25) + 0.5;

            	float3 animatedVertex = v.vertex;

            	if (v.uv.y >= 0.5)
				{
					float3 v0 = animatedVertex;
					
					v0 += cos(_Time.y) * _WindDirection.xyz * (_WindSpeed * (step(0.01, _GrassHeight)));
					
					animatedVertex = v0;
				}

            	if (v.uv.y >= 0.5)
            	{
					animatedVertex.z = grassHeight;
            	}

            	o.position = UnityObjectToClipPos(animatedVertex);
            	
				return o;
            }
            

            fixed4 frag (VertexOutput i) : SV_Target
            {
                float4 c = tex2D(_MainTex, i.uv);
				
				clip(c.a - 0.05);
            	
				return _GrassColor;
            }
            
            ENDCG
        }
    }
}
