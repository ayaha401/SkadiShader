#ifndef SKADI_SPRITE_UNLIT_VERT
#define SKADI_SPRITE_UNLIT_VERT

v2f vert (appdata v)
{
    v2f o = (v2f)0;
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

    // ポリゴンを回転させる
    v.positionOS.xy += _Pivot;
    v.positionOS.xy = mul(v.positionOS.xy, SKADI_ROT_Z(SKADI_DEG2RAD(_Angle)));

    o.positionCS = TransformObjectToHClip(v.positionOS);

    #if defined(DEBUG_DISPLAY)
        o.positionWS = TransformObjectToWorld(v.positionOS);
    #endif

    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    // UVScroll
    if(_UseUVScroll)
    {
        o.uv.xy += float2(_UVScroll_X, _UVScroll_Y) * _Time.y;
    }

    o.color = v.color;

    return o;
}

#endif