#ifndef SKADI_SPRITE_LIT_FRAG
#define SKADI_SPRITE_LIT_FRAG

#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

float4 frag (v2f i) : SV_Target
{
    const float4 mainCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
    const float4 lightingMask = SAMPLE_TEXTURE2D(_LightingMask, sampler_MainTex, i.lightingMaskUV);
    const float outline = SAMPLE_TEXTURE2D(_OETex, sampler_MainTex, i.uv).r;

    SurfaceData2D surfaceData;
    InputData2D inputData;

    // ColorBlend
    float3 blendCol = mainCol.rgb;
    if(_BlendMode == 0) blendCol = blendCol * i.color.rgb;
    if(_BlendMode == 1) blendCol = i.color.rgb;
    
    // outlineCol
    float3 outlineCol = (outline.xxx * _OutlineColor) * _UseOutline;
    float outlineAlpha = (outline * _OutlineColor.a) * _UseOutline;

    // 強制デフォルト値を使うのならば
    if(_UseOutlineDefault)
    {
        outlineCol = i.OESDefault.x;
        outlineAlpha = i.OESDefault.x;
    }

    InitializeSurfaceData(blendCol, mainCol.a * i.color.a + outlineAlpha, lightingMask, surfaceData);
    InitializeInputData(i.uv, i.lightingUV, inputData);

    float4 lastCol = CombinedShapeLightShared(surfaceData, inputData);
    
    // アウトラインカラーをライティング計算後に加算
    lastCol.rgb += outlineCol;
    
    // Emission
    float3 emissionCol = 0.;
    if(_UseEmission)
    {
        const float emissive = SAMPLE_TEXTURE2D(_OETex, sampler_MainTex, i.uv).g;
        emissionCol = mainCol * emissive * _EmissionPower;
        emissionCol *= FlickerWave(_Flicker, _Frequency);
    }

    // 強制デフォルト値を使うのならば
    if(_UseEmissionDefault)     emissionCol = i.OESDefault.yyy;
    
    // Add Emission
    lastCol.rgb += emissionCol;
    return lastCol;
}

#endif