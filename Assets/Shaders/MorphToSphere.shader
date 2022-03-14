Shader "Unlit/MorphToSphere"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Blend("Blend",Range(0,1)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

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
                float SIDE = 2;
                float _Blend;

                v2f vert(appdata v)
                {
                    v2f o;

                    float3 anotherShape;

                     // SPHERE
                     // Spheres are easier - just normalize to get a direction and scale it.
                     anotherShape = normalize(v.vertex.xyz);

                    // Perform our blend in object space, before projection.
                    float4 blended = float4(lerp(v.vertex.xyz, anotherShape, _Blend), 1.0f);

                    // Project our blended result to clip space. 
                    o.vertex = UnityObjectToClipPos(blended);

                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    return col;
                }
                ENDCG
            }
        }
}