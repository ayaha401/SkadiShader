#ifndef SKADI_SPRITE_UNLIT_CORE
#define SKADI_SPRITE_UNLIT_CORE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Macro.hlsl"
#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Function.hlsl"
#if defined(DEBUG_DISPLAY)
    #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/InputData2D.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging2D.hlsl"
#endif

TEXTURE2D(_MainTex);   uniform float4 _MainTex_ST;
TEXTURE2D(_OETex);

SamplerState sampler_MainTex;

// Rotation
uniform float _Angle;
uniform float2 _Pivot;

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

struct appdata
{
    float4 positionOS : POSITION;
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

#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Unlit_Vert.hlsl"
#include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Unlit_Frag.hlsl"

#endif