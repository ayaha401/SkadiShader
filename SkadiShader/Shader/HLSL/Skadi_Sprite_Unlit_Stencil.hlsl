#ifndef SKADI_SPRITE_UNLIT_STENCIL
#define SKADI_SPRITE_UNLIT_STENCIL

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Macro.hlsl"
#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Function.hlsl"

TEXTURE2D(_MainTex);   uniform float4 _MainTex_ST;
TEXTURE2D(_OETex);

SamplerState sampler_MainTex;

// Outline
uniform int _UseOutline;

uniform float _UseMirrorImage;
uniform float _MirrorImageDist;
uniform float _MirrorImageTrans;

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
    float4 color : COLOR;
    float2 uv : TEXCOORD0;

    UNITY_VERTEX_OUTPUT_STEREO
};

v2f vert(appdata v) 
{
    v2f o  = (v2f)0;
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

    o.positionCS = TransformObjectToHClip(v.positionOS);
    o.positionCS.x += _MirrorImageDist * _UseMirrorImage;

    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    o.color = v.color;
    return o;
}

float4 frag(v2f i) : SV_Target 
{
    const float4 mainCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
    
    // outline
    float outline = SAMPLE_TEXTURE2D(_OETex, sampler_MainTex, i.uv).r;
    outline *= _UseOutline;

    // stencilMask
    float stencilMask = SAMPLE_TEXTURE2D(_OETex, sampler_MainTex, i.uv).b;

    #ifdef STENCIL_WRITE
        clip((mainCol.a + outline) * stencilMask - 0.01);
        // clip((mainCol.a + outline) - 0.01); // いったんアルファだけ抜く
        return mainCol; // いったんMainCol
    #endif

    #ifdef STENCIL_READ
        clip(mainCol.a - 0.01); // いったんアルファだけ抜く

        return float4(float3(0,0,0), .5);
    #endif
}

#endif