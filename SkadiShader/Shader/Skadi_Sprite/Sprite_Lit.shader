Shader "Skadi/Skadi_Sprite_Lit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightingMask ("Lighting Mask", 2D) = "white" {}
        _OTex ("OTexture",2D) = "black" {}
        [Toggle]_UseOutline ("Use Outline", int) = 0
        _OutlineColor ("OutlineColor",Color) = (1.0,1.0,1.0,1.0)
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
            Name "Base"
            Tags
            {
                "LightMode" = "Universal2D"
            }

            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "../Shader/HLSL/Skadi_Macro.hlsl"

            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_0
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_1
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_2
            #pragma multi_compile __ USE_SHAPE_LIGHT_TYPE_3
            #pragma multi_compile _ DEBUG_DISPLAY

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"

            TEXTURE2D(_MainTex);    float4 _MainTex_ST;
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_LightingMask);
            SAMPLER(sampler_LightingMask);

            #if USE_SHAPE_LIGHT_TYPE_0
            SHAPE_LIGHT(0)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_1
            SHAPE_LIGHT(1)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_2
            SHAPE_LIGHT(2)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_3
            SHAPE_LIGHT(3)
            #endif

            struct appdata
            {
                float3 positionOS : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float2 lightingUV : TEXCOORD1;      // 謎

                // 謎
                #if defined(DEBUG_DISPLAY)
                float3  positionWS  : TEXCOORD2;
                #endif
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert (appdata v)
            {
                v2f o = (v2f)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.positionCS = TransformObjectToHClip(v.positionOS);

                #if defined(DEBUG_DISPLAY)
                o.positionWS = TransformObjectToWorld(v.positionOS);
                #endif

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // 謎
                o.lightingUV = half2(ComputeScreenPos(o.positionCS / o.positionCS.w).xy);
                o.color = v.color;
                return o;
            }

            // 謎
            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

            float4 frag (v2f i) : SV_Target
            {
                const float4 mainCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * i.color;
                const float4 mask = SAMPLE_TEXTURE2D(_LightingMask, sampler_LightingMask, i.uv);
                SurfaceData2D surfaceData;
                InputData2D inputData;

                InitializeSurfaceData(mainCol.rgb, mainCol.a, mask, surfaceData);
                InitializeInputData(i.uv, i.lightingUV, inputData);

                float4 lastCol = CombinedShapeLightShared(surfaceData, inputData);
                return lastCol;
            }
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
