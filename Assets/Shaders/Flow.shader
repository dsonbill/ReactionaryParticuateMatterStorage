Shader "VectorFieldFlow"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex VSMain
            #pragma fragment PSMain

            static const float PI = 3.1415927;
            static const int   ARROW_V_STYLE = 1;
            static const int   ARROW_LINE_STYLE = 2;
            static const int   ARROW_STYLE = ARROW_LINE_STYLE;
            static const float ARROW_TILE_SIZE = 64.0;
            static const float ARROW_HEAD_ANGLE = 45.0 * PI / 180.0;
            static const float ARROW_HEAD_LENGTH = ARROW_TILE_SIZE / 6.0;
            static const float ARROW_SHAFT_THICKNESS = 3.0;

            float2 arrowTileCenterCoord(float2 pos)
            {
                return (floor(pos / ARROW_TILE_SIZE) + 0.5) * ARROW_TILE_SIZE;
            }

            float arrow(float2 p, float2 v)
            {
                p -= arrowTileCenterCoord(p);
                float mag_v = length(v), mag_p = length(p);
                if (mag_v > 0.0)
                {
                    float2 dir_p = p / mag_p, dir_v = v / mag_v;
                    mag_v = clamp(mag_v, 5.0, ARROW_TILE_SIZE / 2.0);
                    v = dir_v * mag_v;
                    float dist;
                    if (ARROW_STYLE == ARROW_LINE_STYLE)
                    {
                        dist = max(ARROW_SHAFT_THICKNESS / 4.0 - max(abs(dot(p, float2(dir_v.y, -dir_v.x))),
                            abs(dot(p, dir_v)) - mag_v + ARROW_HEAD_LENGTH / 2.0),
                            min(0.0, dot(v - p, dir_v) - cos(ARROW_HEAD_ANGLE / 2.0) * length(v - p)) * 2.0 +
                            min(0.0, dot(p, dir_v) + ARROW_HEAD_LENGTH - mag_v));
                    }
                    else
                    {
                        dist = min(0.0, mag_v - mag_p) * 2.0 +
                                min(0.0, dot(normalize(v - p), dir_v) - cos(ARROW_HEAD_ANGLE / 2.0)) * 2.0 * length(v - p) +
                                min(0.0, dot(p, dir_v) + 1.0) +
                                min(0.0, cos(ARROW_HEAD_ANGLE / 2.0) - dot(normalize(v * 0.33 - p), dir_v)) * mag_v * 0.8;
                    }
                    return clamp(1.0 + dist, 0.0, 1.0);
                }
                else
                {
                    return max(0.0, 1.2 - mag_p);
                }
            }

            float2 field(float2 pos)
            {
                return float2(cos(pos.x * 0.01 + pos.y * 0.01) + cos(pos.y * 0.005 + _Time.g), 2.0 * cos(pos.y * 0.01 + _Time.g * 0.3)) * 0.5;
            }

            void VSMain(inout float4 vertex:POSITION, inout float2 uv : TEXCOORD0)
            {
                vertex = UnityObjectToClipPos(vertex);
            }

            float4 PSMain(float4 vertex:POSITION, float2 uv : TEXCOORD0) : SV_Target
            {
                float2 fragCoord = uv * float2(1024,1024);
                return (1.0 - arrow(fragCoord.xy, field(arrowTileCenterCoord(fragCoord.xy)) * ARROW_TILE_SIZE * 0.4)) * float4(field(fragCoord.xy) * 0.5 + 0.5, 0.5, 1.0);
            }

            ENDCG
        }
    }
}