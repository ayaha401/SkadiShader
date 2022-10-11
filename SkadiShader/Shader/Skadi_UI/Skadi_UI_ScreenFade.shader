Shader "Skadi/UI/Skadi_UI_ScreenFade"
{
    Properties
    {
        // Fade
        _Fadeout ("Fadeout", Range(0,1)) = 0.5
        [Enum(Up,0, Down,1, Left,2, Right,3)] _FadeDir("Fade Direction", int) = 2

        // Advanced Settings
        _ImageSizeX ("ImageSizeX", float) = 1920
        _ImageSizeY ("ImageSizeY", float) = 1080
    }
    SubShader
    {
        Tags
        { 
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma enable_d3d11_debug_symbols

            #include "UnityCG.cginc"
            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Macro.hlsl"
            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Function.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Fade
            uniform float _Fadeout;
            uniform int _FadeDir;

            // Advanced Settings
            uniform float _ImageSizeX;
            uniform float _ImageSizeY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float fade = _Fadeout*2-1;              // -1~1
                if(_FadeDir == 0)   fade += 1-i.uv.y;
                if(_FadeDir == 1)   fade += i.uv.y;
                if(_FadeDir == 2)   fade += i.uv.x;
                if(_FadeDir == 3)   fade += 1-i.uv.x;
                float4 lastCol = float4(0,0,0, fade);

                return lastCol;
            }
            ENDCG
        }
    }
    CustomEditor "AyahaShader.Skadi.SkadiScreenFade_GUI"
}
