#ifndef SKADI_SPRITE_LIT_CORE
#define SKADI_SPRITE_LIT_CORE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"
#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Macro.hlsl"
#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Function.hlsl"

TEXTURE2D(_MainTex);   uniform float4 _MainTex_ST;
TEXTURE2D(_LightingMask);
TEXTURE2D(_OETex);

SamplerState sampler_MainTex;

// OESDefault
uniform int _UseOutlineDefault;
uniform int _OutlineDefault;
uniform int _UseEmissionDefault;
uniform int _EmissionDefault;
// uniform int _UseStencilDefault;
// uniform int _StencilDefault;

// UVScroll
uniform int _UseUVScroll;
uniform float _UVScroll_X;
uniform float _UVScroll_Y;

// Outline
uniform int _UseOutline;
uniform float4 _OutlineColor;

// Emission
uniform int _UseEmission;
uniform float _EmissionPower;
uniform int _Flicker;
uniform float _Frequency;

// Fill
uniform int _BlendMode;

#if USE_SHAPE_LIGHT_TYPE_0
SHAPE_LIGHT(0)
#endif

#if USE_SHAPE_LIGHT_TYPE_1
SHAPE_LIGHT(1)
#endif

#if USE_SHAPE_LIGHT_TYPE_2
SHAPE_LIGHT(2)
#endif

#if USE_SHAPE_LIGHT_TYPE_3
SHAPE_LIGHT(3)
#endif

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
    float2 lightingUV : TEXCOORD1;
    #if defined(DEBUG_DISPLAY)
        float3  positionWS  : TEXCOORD2;
    #endif
    float2 lightingMaskUV : TEXCOORD3;

    //==================//
    // x    : outline   //
    // y    : emission  //
    // z    : stencil   //
    // w    : not used  //
    //==================//
    float4 OESDefault : TEXCOORD4;
    
    UNITY_VERTEX_OUTPUT_STEREO
};

#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Lit_Vert.hlsl"
#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Lit_Frag.hlsl"

#endif