Shader "Skadi/UI/Skadi_UI_Scanline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortPower ("Distort Power", Range(0.0,1.0)) = 0.0
        _Brightness ("Brightness", Range(0.0,1.0)) = 0.8
        _LineAmount ("LineAmount", float)=200
        _LineMoveSpeed ("Line MoveSpeed", float) = 0.0
        [Toggle]_UseVignette ("Use Vignette", float) = 0.0
        [Toggle]_LineNonBlur ("Line NonBlur", float) = 0.0
        [Toggle]_UseGlitch ("Use Glitch", float) = 0.0
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
        CUll Off
        ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma enable_d3d11_debug_symbols

            #include "UnityCG.cginc"
            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Macro.hlsl"
            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Skadi_Function.hlsl"
            #include "Assets/AyahaShader/SkadiShader/Shader/HLSL/Noise.hlsl"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            uniform float _DistortPower;
            uniform float _Brightness;
            uniform uint _LineAmount;
            uniform float _UseVignette;
            uniform float _LineMoveSpeed;
            uniform float _LineNonBlur;
            uniform float _UseGlitch;

            // 画面を歪ませる
            float2 distort(float2 p)
            {
                p = p * 2. - 1.;
                float theta = atan2(p.y, p.x);
                float r = length(p);
                r = pow(r, 1.+_DistortPower);
                p.x = r * cos(theta);
                p.y = r * sin(theta);
                return .5 * (p + 1.);
            }

            // ビネット効果
            float3 vignette(float2 p,float3 col, float flag)
            {
                p=abs(p * 2. - 1.);
                p=pow(p,15);
                return lerp(col.rgb, (0.).xxx + (col.rgb * (1.- flag)), p.x + p.y);
            }

            v2f vert (appdata v)
            {
                v2f o = (v2f)0;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 p = distort(i.uv);

                float noise = perlinNoise(i.uv.yy+_Time.y * 10., 10.);
                noise = (noise * 2. - 1.) * .05;

                float strength = pow(perlinNoise((float2)_Time.y, 10.),4.);

                float3 mainTexCol = tex2D(_MainTex, p + (noise * strength * _UseGlitch));

                float lineCol = sin((i.uv.y*_LineAmount)+_Time.y*_LineMoveSpeed);
                if(_LineNonBlur > 0.) lineCol = step(.2, lineCol);
                lineCol = Remap(lineCol, float2(-1,1), float2(0, _Brightness));

                mainTexCol *= lineCol;
                mainTexCol=vignette(p,mainTexCol, _UseVignette);
                return float4((float3)0, lineCol*_Brightness);
            }
            ENDCG
        }
    }
}
