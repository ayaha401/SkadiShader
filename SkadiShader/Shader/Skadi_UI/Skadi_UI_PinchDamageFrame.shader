Shader "Skadi/UI/Skadi_UI_PinchDamageFrame"
{
    Properties
    {
        _Color("Color", Color) = (1.0,1.0,1.0,1.0)
        _SizeX("WideSize", Range(0.0, 1.0))= 0.3
        _SizeY("HeightSize",Range(0.0, 1.0))=0.3
        [Enum(LINE,0, SIN,1, SAW,2, TRIANGLE,3, SQUARE,4)] _Flicker ("Emission Flicker", int) = 0
        _Frequency ("Frequency", float) = 1.0

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
            #pragma multi_compile_fog
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
                float2 patternUV : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 frameUV : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform float3 _Color; 
            uniform float _SizeX;
            uniform float _SizeY;
            uniform int _Flicker;
            uniform float _Frequency;

            // Advanced Settings
            uniform float _ImageSizeX;
            uniform float _ImageSizeY;

            float sdBox(float2 p, float2 s)
            {
                float2 d=abs(p)-s;
                return length(max(d,0.))+min(max(d.x,d.y),0.);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.frameUV = v.uv*2-1;

                float2 size = float2(1920.,1080.);
                v.uv *= size;
                v.uv = (v.uv*2.-size.xy)/min(size.x,size.y);
                o.patternUV = v.uv;
                
                return o;
            }

            float gridgeEffect(float2 uv, float s)
            {
                float2 i=floor(uv*s);
                float2 f=frac(uv*s);

                float2 minP = (float2)8.;

                for(int y=-1;y<=1;y++)
                for(int x=-1;x<=1;x++)
                {
                    float2 neighbor=float2((float)x, (float)y);
                    float2 p=f2rand2HalfOne((i+neighbor)*perlinNoise(uv.yy+_Time.y*10., 10.));
                
                    p=.5+.5*sin(_Time.y+SKADI_TAU*p);
                    float2 diff=neighbor+p-f;
                    float dist=polyDistance(diff, 3.);

                    if(dist<minP.x)
                    {
                        minP.y=minP.x;
                        minP.x = dist;
                    }
                    else if(dist<minP.y)
                    {
                        minP.y=dist;
                    }
                }

                float2 c = minP;
                return c.y-c.x;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 patternUV=i.patternUV;
                float2 frameUV = i.frameUV;

                float3 col = _Color.rgb;
                float frame = sdBox(frameUV, float2(1.-_SizeX, 1.-_SizeY));
                float pattern = smoothstep(.06,.08,gridgeEffect(patternUV,5.));

                float3 frameCol = col.rgb + 1-smoothstep(.1,.2,gridgeEffect(patternUV,5.)).xxx;
                float4 lastCol=float4(frameCol, max(frame,0.)*pattern*FlickerWave(_Flicker, _Frequency));
                return lastCol;
            }
            ENDCG
        }
    }
    CustomEditor "AyahaShader.Skadi.SkadiDamageFrame_GUI"
}
