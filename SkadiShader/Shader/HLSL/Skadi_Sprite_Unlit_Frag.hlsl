#ifndef SKADI_SPRITE_UNLIT_FRAG
#define SKADI_SPRITE_UNLIT_FRAG

float4 frag (v2f i) : SV_Target
{
    const float4 mainCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * i.color;
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

    float4 lastCol = mainCol;
    
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