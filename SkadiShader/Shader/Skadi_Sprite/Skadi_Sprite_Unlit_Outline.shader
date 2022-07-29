Shader "Skadi/Sprite/Skadi_Sprite_Unlit_Outline"
{
    Properties
    {
        // Main
        _MainTex ("Texture", 2D) = "white" {}

        // UVScroll
        [Toggle]_UseUVScroll("Use UV Scroll", int) = 0
        _UVScroll_X("UV Scroll X",Range(-3.0,3.0)) = 0.0
        _UVScroll_Y("UV Scroll Y",Range(-3.0,3.0)) = 0.0

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
        [Enum(Multi,0, Fill,1)]_BlendMode ("Blend Mode", int) = 0
    
        // Stencil
        // _StencilNum ("Stencil Number", int) = 4
        // [Enum(Disabled,0, Never,1, Equal,3, LEqual,4, Greater,5, NotEqual,6, Always,8)] 
        // _StencilCompMode ("Stencil CompMode", int) = 0
        // [Enum(Keep,0, Zero,1, Replace,2, IncrSat,3, DecrSat,4)]
        // _StencilOp ("Stencil Operation", int) = 0
        // [Toggle] _UseMirrorImage ("Use MirrorImage", int) = 0
        // _MirrorImageDist ("MirrorImage Distance", float) = 0.03
        // _MirrorImageTrans ("MirrorImage Transparent", Range(0.0,1.0)) = 0.5
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

            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ DEBUG_DISPLAY

            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Sprite_Unlit_Core.hlsl"

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
