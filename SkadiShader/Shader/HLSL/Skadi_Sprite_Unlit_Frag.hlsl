#ifndef SKADI_SPRITE_UNLIT_FRAG
#define SKADI_SPRITE_UNLIT_FRAG

float4 frag (v2f i) : SV_Target
{
    const float4 mainCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
    #if defined(DEBUG_DISPLAY)
        SurfaceData2D surfaceData;
        InputData2D inputData;
        half4 debugColor = 0;

        InitializeSurfaceData(mainTex.rgb, mainTex.a, surfaceData);
        InitializeInputData(i.uv, inputData);
        SETUP_DEBUG_DATA_2D(inputData, i.positionWS);

        if(CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
        {
            return debugColor;
        }
    #endif

    // ColorBlend
    float3 blendCol = mainCol.rgb;
    if(_BlendMode == 0) blendCol = blendCol * i.color.rgb;
    if(_BlendMode == 1) blendCol = i.color.rgb;

    float4 lastCol = float4(blendCol, mainCol.a);
    
    // Emission
    float3 emissionCol = 0.;
    if(_UseEmission)
    {
        const float emissive = SAMPLE_TEXTURE2D(_OETex, sampler_MainTex, i.uv).g;
        emissionCol = mainCol * emissive * _EmissionPower;
        emissionCol *= FlickerWave(_Flicker, _Frequency);
    }

    // Add Emission
    lastCol.rgb += emissionCol;
    return lastCol;
}

#endif