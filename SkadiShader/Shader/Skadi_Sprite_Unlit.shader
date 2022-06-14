Shader "Skadi/Skadi_Sprite_Unlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OETex ("OETexture",2D) = "black" {}
        [Toggle]_UseOutline ("Use Outline", int) = 0
        _OutlineColor ("OutlineColor",Color) = (1.0,1.0,1.0,1.0)
        [Toggle]_UseEmission ("Use Emissin", int) = 0
        _EmissionPower ("Emission Power", float) = 200
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
            Name "Unlit_Base"
            Tags
            {
                "LightMode" = "Universal2D"
            }

            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _ DEBUG_DISPLAY

            #include "../Shader/HLSL/Skadi_Sprite_Unlit_Core.hlsl"

            ENDHLSL
        }

        Pass
        {
            Name "Unlit_Outline"
            Tags
            {
                "LightMode" = "SRPDefaultUnlit"
            }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ DEBUG_DISPLAY

            #include "../Shader/HLSL/Skadi_Unlit_Outline.hlsl"

            ENDHLSL
        }
    }

    Fallback "Sprites/Default"
    CustomEditor "Skadi_GUI"
}