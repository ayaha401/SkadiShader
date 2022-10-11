Shader "Skadi/Sprite/Skadi_Sprite_Lit_Outline"
{
    Properties
    {
        // Main
        _MainTex ("Texture", 2D) = "white" {}

        // Rotation
        _Angle ("Angle", float) = 0
        [Vec2]_Pivot ("Pivot", Vector) = (0,0,0,0)

        // UVScroll
        [Toggle]_UseUVScroll("Use UV Scroll", int) = 0
        _UVScroll_X("UV Scroll X",Range(-3.0,3.0)) = 0.0
        _UVScroll_Y("UV Scroll Y",Range(-3.0,3.0)) = 0.0

        // Lighting
        _LightingMask ("Lighting Mask", 2D) = "white" {}

        // OE
        _OETex ("OETexture",2D) = "black" {}
        [Toggle] _UseOutlineDefault ("Use OutlineDefault", int) = 0
        [Enum(Black,0, White,1)] _OutlineDefault ("OutlineDefault", int) = 0
        [Toggle] _UseEmissionDefault ("Use EmissionDefault", int) = 0
        [Enum(Black,0, White,1)] _EmissionDefault ("EmissionDefault", int) = 0
        // [Toggle] _UseStencilDefault ("Use StencilDefault", int) = 0
        // [Enum(Black,0, White,1)] _StencilDefault ("StencilDefault", int) = 1

        // Outline
        [Toggle]_UseOutline ("Use Outline", int) = 0
        _OutlineColor ("OutlineColor",Color) = (1.0,1.0,1.0,1.0)

        // Emission
        [Toggle]_UseEmission ("Use Emissin", int) = 0
        _EmissionPower ("Emission Power", float) = 200
        [Enum(LINE,0, SIN,1, SAW,2, TRIANGLE,3, SQUARE,4)]_Flicker ("Emission Flicker", int) = 0
        _Frequency ("Frequency", float) = 1.0

        // BlendMode
        [Enum(Multi, 0, Fill, 1)]_BlendMode ("Blend Mode", int) = 0
    }

    SubShader
    {
        Tags 
        {
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
                "Queue" = "Transparent" 
                "RenderType" = "Transparent" 
            }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_0
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_1
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_2
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_3
            #pragma multi_compile _ DEBUG_DISPLAY
            #pragma enable_d3d11_debug_symbols

            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Lit_Core.hlsl"
            ENDHLSL
        }

        // Pass
        // {
        //     Name "Stencil_Pass"
        //     Tags
        //     {
        //         "LightMode" = "SRPDefaultUnlit"
        //         "Queue" = "Transparent"
        //         "RenderType" = "Transparent"
        //     }

        //     // Blend SrcAlpha OneMinusSrcAlpha
        //     // ZTest Always
        //     // ZWrite Off

        //     Stencil
        //     {
        //         Ref [_StencilNum]
        //         Comp [_StencilCompMode]
        //         Pass [_StencilOp]
        //     }

        //     HLSLPROGRAM

        //     #pragma vertex vert
        //     #pragma fragment frag
        //     #pragma multi_compile _ DEBUG_DISPLAY

        //     #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Unlit_Stencil.hlsl"
        //     ENDHLSL
        // }
    }
    
    Fallback "Sprites/Default"
    CustomEditor "AyahaShader.Skadi.SkadiSprite_GUI"
}
