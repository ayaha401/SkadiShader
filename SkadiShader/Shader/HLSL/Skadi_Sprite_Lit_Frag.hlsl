#ifndef SKADI_SPRITE_LIT_FRAG
#define SKADI_SPRITE_LIT_FRAG

#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

float4 frag (v2f i) : SV_Target
{
    const float4 mainCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * i.color;
    const float4 mask = SAMPLE_TEXTURE2D(_LightingMask, sampler_MainTex, i.uv);
    SurfaceData2D surfaceData;
    InputData2D inputData;

    InitializeSurfaceData(mainCol.rgb, mainCol.a, mask, surfaceData);
    InitializeInputData(i.uv, i.lightingUV, inputData);

    float4 lastCol = CombinedShapeLightShared(surfaceData, inputData);

    // Emission
    float3 emissionCol = 0.;
    if(_UseEmission)
    {
        const float emissive = SAMPLE_TEXTURE2D(_OETex, sampler_MainTex, i.uv).g;
        emissionCol = mainCol * emissive * _EmissionPower;
    }
    
    // Add Emission
    lastCol.rgb += emissionCol;
    return lastCol;
}

#endif