#ifndef SKADI_SPRITE_UNLIT_CORE
#define SKADI_SPRITE_UNLIT_CORE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "../Shader/HLSL/Skadi_Macro.hlsl"
#if defined(DEBUG_DISPLAY)
    #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/InputData2D.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging2D.hlsl"
#endif

TEXTURE2D(_MainTex);   uniform float4 _MainTex_ST;
TEXTURE2D(_OETex);

SamplerState sampler_MainTex;

// Emission
uniform int _UseEmission;
uniform float _EmissionPower;

// Fill
uniform int _BlendMode;

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

    #if defined(DEBUG_DISPLAY)
        float3  positionWS  : TEXCOORD2;
    #endif
    UNITY_VERTEX_OUTPUT_STEREO
};

#include "../HLSL/Skadi_Sprite_Unlit_Vert.hlsl"
#include "../HLSL/Skadi_Sprite_Unlit_Frag.hlsl"

#endif