Shader "Skadi/Sprite/Unlit_Stencil_Read"
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

        // OE
        _OETex ("OETexture",2D) = "black" {}

        // Outline
        [Toggle]_UseOutline ("Use Outline", int) = 0
        _OutlineColor ("OutlineColor",Color) = (1.0,1.0,1.0,1.0)

        // Emission
        [Toggle]_UseEmission ("Use Emissin", int) = 0
        _EmissionPower ("Emission Power", float) = 200
        [Enum(LINE,0, SIN,1, SAW,2, TRIANGLE,3, SQUARE,4)]_Flicker ("Emission Flicker", int) = 0
        _Frequency ("Frequency", float) = 1.0

        // BlendMode
        [Enum(Multi,0, Fill,1)]_BlendMode ("Blend Mode", int) = 0
    
        // Stencil
        _StencilNum ("Stencil Number", int) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)]_StencilCompMode ("Stencil CompMode", int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilOp ("Stencil Operation", int) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "PreviewType" = "Plane"
        }

        Pass
        {
            Name "Stencil_Pass"
            Tags
            {
                "LightMode" = "SRPDefaultUnlit"
                "Queue" = "Transparent"
                "RenderType" = "Transparent"
            }

            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Always
            ZWrite Off

            Stencil
            {
                Ref [_StencilNum]
                Comp [_StencilCompMode]
                Pass [_StencilOp]
            }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ DEBUG_DISPLAY

            #define STENCIL_READ
            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Unlit_Stencil.hlsl"
            ENDHLSL
        }

        Pass
        {
            Name "Unlit_Base"
            Tags
            {
                "LightMode" = "Universal2D"
                "Queue" = "Transparent"
                "RenderType" = "Transparent" 
            }

            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            Stencil
            {
                Ref 0
                Comp [_StencilCompMode]
                Pass [_StencilOp]
            }

            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ DEBUG_DISPLAY
            #pragma enable_d3d11_debug_symbols

            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Unlit_Core.hlsl"

            ENDHLSL
        }
    }

    Fallback "Sprites/Default"
    CustomEditor "AyahaShader.Skadi.SkadiSprite_GUI"
}
