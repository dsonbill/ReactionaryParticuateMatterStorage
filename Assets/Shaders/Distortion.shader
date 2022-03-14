Shader "Distortion" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _DistortionLevel("Distortion Level", Color) = (0,0,0,0)
        _DistortionModifier ("Modifier", Float) = 1
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200

            Pass {
                CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag
                    #include "UnityCG.cginc"

                    struct v2f {
                        float4 pos : SV_POSITION;
                        float2 uv_MainTex : TEXCOORD0;
                    };

                    float4 _MainTex_ST;
                    float4 _DistortionLevel;
                    float _DistortionModifier;

                    v2f vert(appdata_base v) {
                        v2f o;
                        float4 pos = mul(UNITY_MATRIX_MV, v.vertex);

                        float y = pos.y;
                        float x = pos.x;
                        float z = pos.z;

                        pos.y = y * sin(_DistortionLevel.y) - x * cos(_DistortionLevel.y);
                        pos.x = x * sin(_DistortionLevel.x) - y * cos(_DistortionLevel.x);
                        pos.z = z * sin(_DistortionLevel.z) + z * cos(_DistortionLevel.z);

                        o.pos = mul(UNITY_MATRIX_P, pos);

                        o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
                        return o;
                    }

                    sampler2D _MainTex;

                    float4 frag(v2f IN) : COLOR {
                        half4 c = tex2D(_MainTex, IN.uv_MainTex);
                        return c;
                    }
                ENDCG
            }
    }
}