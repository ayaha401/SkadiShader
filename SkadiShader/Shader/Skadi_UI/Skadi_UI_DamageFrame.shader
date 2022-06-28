Shader "Skadi/UI/Skadi_UI_DamageFrame"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1.0,1.0,1.0,1.0)
        _SizeX("WideSize", Range(0.0, 1.0))= 0.3
        _SizeY("HeightSize",Range(0.0, 1.0))=0.3
        [Enum(LINE,0, SIN,1, SAW,2, TRIANGLE,3, SQUARE,4)] _Flicker ("Emission Flicker", int) = 0
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
            #pragma multi_compile_fog

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform float3 _Color; 
            uniform float _SizeX;
            uniform float _SizeY;
            uniform int _Flicker;

            float sdBox(float2 p, float2 s)
            {
                float2 d=abs(p)-s;
                return length(max(d,0.))+min(max(d.x,d.y),0.);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
				float2 uv=i.uv-.5;
                float3 col = _Color.rgb;
                float box = sdBox(uv, float2((1.-_SizeX)*.5, (1.-_SizeY)*.5));

                float4 lastCol=float4(col.rgb, max(box,0.)*FlickerWave(_Flicker, 1.));
                return lastCol;
            }
            ENDCG
        }
    }
    CustomEditor "AyahaShader.Skadi.SkadiDamageFrame_GUI"
}
