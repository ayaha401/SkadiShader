#ifndef SKADI_UNLIT_OUTLINE
#define SKADI_UNLIT_OUTLINE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "../Shader/HLSL/Skadi_Macro.hlsl"

TEXTURE2D(_OTex);    float4 _OTex_ST;
SAMPLER(sampler_OTex);

uniform float _UseOutline;
uniform float3 _OutlineColor;

struct appdata
{
    float3 positionOS : POSITION;
    float4 color : COLOR;
    float2 uv : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
    float4 positionCS : SV_POSITION;
    float3 color : COLOR;
    float2 uv : TEXCOORD0;

    #if defined(DEBUG_DISPLAY)
    float3  positionWS  : TEXCOORD2;
    #endif
    UNITY_VERTEX_OUTPUT_STEREO
};

v2f vert (appdata v)
{
    v2f o = (v2f)0;
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

    if(!_UseOutline)    v.positionOS *= SKADI_EPS;

    o.positionCS = TransformObjectToHClip(v.positionOS);

    #if defined(DEBUG_DISPLAY)
    o.positionWS = TransformObjectToWorld(v.positionOS);
    #endif

    
    o.uv = TRANSFORM_TEX(v.uv, _OTex);

    o.color = _OutlineColor;
    return o;
}

float4 frag (v2f i) : SV_Target
{
    if(!_UseOutline) discard;

    const float outline = SAMPLE_TEXTURE2D(_OTex, sampler_OTex, i.uv).r;
    float4 outlineCol = float4(outline.xxx * i.color, outline);
    clip(outlineCol.a-SKADI_EPS);

    return outlineCol;
}

#endif