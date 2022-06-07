// Pcx - Point cloud importer & renderer for Unity
// https://github.com/keijiro/Pcx

Shader "Point Cloud/Point2"
{
    Properties
    {
        _Tint("Tint", Color) = (0.5, 0.5, 0.5, 1)
        _PointSize("Point Size", Float) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM

            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "UnityCG.cginc"

            struct Attributes
            {
                half3 position : POSITION;
            };

            struct Varyings
            {
                float4 position : SV_Position;
                float3 color : COLOR;
                float psize : PSIZE;
            };

            float4 _Tint;
            float _PointSize;

            Varyings Vertex(Attributes input)
            {
                float3 pos = input.position;
                Varyings o;
                o.position = UnityObjectToClipPos(pos);
                o.color = _Tint;
                o.psize = _PointSize;
                return o;
            }

            float4 Fragment(Varyings input) : SV_Target
            {
                float4 c = float4(input.color, _Tint.a);
                return c;
            }

            ENDCG
        }
    }
}
