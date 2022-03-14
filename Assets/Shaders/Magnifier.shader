    Shader "Unlit/ZoomShader"
    {
        Properties
        {
            _MaskColor("Mask Color", Color) = (1,1,1,1)
            _Mask("Mask", 2D) = "white" {}
            _Magnification("Magnification", Float) = 1
            _UVCenterOffset("UVCenterOffset", Vector) = (0,0,0,1)
        }
     
            SubShader
        {
            Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100
     
            GrabPass{ "_GrabTexture" }
     
            Pass
                {
                    //ZTest On
                    //ZWrite Off
                    //Blend One Zero
                    //Lighting Off
                    //Fog{ Mode Off }
     
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
     
                #include "UnityCG.cginc"
     
                struct appdata
                {
                    float4 vertex : POSITION;
                    float4 uv : TEXCOORD0;
                    float4 uv2 : TEXCOORD1;
                };
     
                struct v2f
                {
                    //our vertex position after projection
                    float4 vertex : SV_POSITION;
     
                    //our UV coordinate on the GrabTexture
                    float4 uv : TEXCOORD0;
                    float2 uv2 : TEXCOORD1;
                };
     
                float4 _Mask_ST;
                sampler2D _GrabTexture;
                float4 _MaskColor;
                sampler2D _Mask;
                half _Magnification;
                float4 _UVCenterOffset;
     
                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
     
                    //the UV coordinate of our object's center on the GrabTexture
                    float4 uv_center = ComputeGrabScreenPos(UnityObjectToClipPos(float4(0, 0, 0, 1)));
     
                    uv_center += _UVCenterOffset;
                    //the vector from uv_center to our UV coordinate on the GrabTexture
                    float4 uv_diff = ComputeGrabScreenPos(o.vertex) - uv_center;
                    //apply magnification
                    uv_diff /= _Magnification;
                    //save result
                    o.uv = uv_center + uv_diff;
                    o.uv2 = TRANSFORM_TEX(v.uv2, _Mask);
                    return o;
                }
     
                fixed4 frag(v2f i) : COLOR
                {
                    fixed4 albedo = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uv));
                    fixed4 mask = tex2D(_Mask, UNITY_PROJ_COORD(i.uv2));
                    bool isMask = mask != _MaskColor;
                    albedo = (1 - isMask)*albedo + (isMask * mask);
                    return albedo;
                }
                ENDCG
            }
        }
    }
