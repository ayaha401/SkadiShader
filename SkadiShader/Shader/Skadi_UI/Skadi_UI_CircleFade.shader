Shader "Skadi/UI/Skadi_UI_CircleFade"
{
    Properties
    {
        // Fade
        _Fadeout ("Fadeout", Range(0,1)) = 0.5
        [Vec2] _FadeTarget ("fadeTarget", Vector) = (0,0,0,0)
        _Color("Color", Color) = (0.0, 0.0, 0.0, 1.0)

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
                float4 pos : TEXCOORD1;
            };

            // Fade
            uniform float _Fadeout;
            uniform float2 _FadeTarget;
            uniform float3 _Color;

            // Advanced Settings
            uniform float _ImageSizeX;
            uniform float _ImageSizeY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float4 p = UnityObjectToClipPos(float4(_FadeTarget,0.,0.));
                o.pos = ComputeScreenPos(p);

                v.uv+= _FadeTarget*-.5;
                float2 size = float2(_ImageSizeX,_ImageSizeY);
                v.uv*=size;
                v.uv=(v.uv*2.-size.xy)/min(size.x,size.y);
                o.uv = v.uv;
                return o;
            }

            float Circle(float2 p, float r)
            {
                return length(p)-r;
            }

            float4 frag (v2f i) : SV_Target
            {
                float maxDist;
                maxDist=distance(float2(_FadeTarget),float2(-1.,-1.));
                maxDist=max(maxDist,distance(float2(_FadeTarget),float2(1.,-1.)));
                maxDist=max(maxDist,distance(float2(_FadeTarget),float2(1.,1.)));
                maxDist=max(maxDist,distance(float2(_FadeTarget),float2(-1.,1.)));

                float aspect = _ImageSizeX/_ImageSizeY;
                float fade = step(.001 ,Circle(i.uv, (1.-_Fadeout)*aspect*maxDist));
                float4 col = float4(_Color,fade);
                return col;
            }
            ENDCG
        }
    }
    CustomEditor "AyahaShader.Skadi.SkadiCircleFade_GUI"
}
