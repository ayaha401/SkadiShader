Shader "Skadi/Skadi_Sprite_Lit"
{
    Properties
    {
        // Main
        _MainTex ("Texture", 2D) = "white" {}

        // UVScroll
        [Toggle]_UseUVScroll("Use UV Scroll", int) = 0
        _UVScroll_X("UV Scroll X",Range(-3.0,3.0)) = 0.0
        _UVScroll_Y("UV Scroll Y",Range(-3.0,3.0)) = 0.0

        // Lighting
        _LightingMask ("Lighting Mask", 2D) = "white" {}

        // OE
        _OETex ("OETexture",2D) = "black" {}

        // Emission
        [Toggle]_UseEmission ("Use Emissin", int) = 0
        _EmissionPower ("Emission Power", float) = 200
        [Enum(LINE,0, SIN,1, SAW,2, TRIANGLE,3, SQUARE,4)]_Flicker ("Emission Flicker", int) = 0
        _Frequency ("Frequency", float) = 1.0

        // BlendMode
        [Enum(Multi, 0, Fill, 1)]_BlendMode ("Blend Mode", int) = 0
        // [HideInInspector] _Color("Tint", Color) = (1,1,1,1)
        // [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        // [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        // [HideInInspector] _AlphaTex("External Alpha", 2D) = "white" {}
        // [HideInInspector] _EnableExternalAlpha("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags 
        {
            "Queue" = "Transparent" 
            "RenderType" = "Transparent" 
            "RenderPipeline" = "UniversalPipeline"
            "PreviewType" = "Plane"
        }

        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Name "Lit_Base"
            Tags
            {
                "LightMode" = "Universal2D"
            }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_0
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_1
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_2
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_3
            #pragma multi_compile _ DEBUG_DISPLAY

            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Lit_Core.hlsl"

            ENDHLSL
        }
    }

    Fallback "Sprites/Default"
    CustomEditor "AyahaShader.Skadi.SkadiSprite_GUI"
}
