Shader "Custom/Sparkle"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [MainTexture] _BaseMap("Layer 1 Stars", 2D) = "white" {}
        _Layer2Map("Layer 2 Stars", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _Speed("Sparkle Speed", Float) = 1.0
        _Range("Sparkle Range", Range(0, 1)) = 0.5
        _Layer1Parallax("Layer 1 Parallax", Float) = 0.02
        _Layer2Parallax("Layer 2 Parallax", Float) = 0.05
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" "Queue" = "Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                half4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                half4 color : COLOR;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            TEXTURE2D(_Layer2Map);
            SAMPLER(sampler_Layer2Map);
            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
                float4 _Layer2Map_ST;
                float4 _NoiseTex_ST;
                float _Speed;
                float _Range;
                float _Layer1Parallax;
                float _Layer2Parallax;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                OUT.color = IN.color;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 camOffset = _WorldSpaceCameraPos.xy;

                float2 uv1 = TRANSFORM_TEX(IN.uv, _BaseMap) + camOffset * _Layer1Parallax;
                float2 uv2 = TRANSFORM_TEX(IN.uv, _Layer2Map) + camOffset * _Layer2Parallax;

                half4 layer1 = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv1);
                half4 layer2 = SAMPLE_TEXTURE2D(_Layer2Map, sampler_Layer2Map, uv2);

                half4 stars = saturate(layer1 + layer2) * _BaseColor * IN.color;

                float2 noiseUV = TRANSFORM_TEX(IN.uv, _NoiseTex) + _Time.y * _Speed;
                half noise = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, noiseUV).r;
                half sparkle = lerp(1.0 - _Range, 1.0, noise);

                return half4(stars.rgb * sparkle, stars.a);
            }
            ENDHLSL
        }
    }
}
