using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AyahaShader.Skadi
{
    /// <summary>
    /// SkadiShaderのSprite関係のマテリアルを調整するためのスクリプト
    /// </summary>
    public static class SkadiSpriteShaderProvider
    {
        /// <summary>
        /// SpriteRendererのColorのアルファ値を変更します。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="alpha">アルファ値</param>
        /// <param name="blendMode">Multi : 乗算, Fill : 塗りつぶし</param>
        public static void SetColorAlpha(SpriteRenderer renderer, float alpha, ColorBlendMode blendMode = ColorBlendMode.Multi)
        {
            Color col = renderer.color;
            col.a = alpha;
            renderer.color = col;
        }
        
        /// <summary>
        /// マテリアルの色を変えます。SpriteRenderer.Colorとほぼ同義
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="col">合成したい色</param>
        /// <param name="blendMode">Multi : 乗算, Fill : 塗りつぶし
        /// </param>
        public static void SetColor(SpriteRenderer renderer, Color col, ColorBlendMode blendMode = ColorBlendMode.Multi)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_BlendMode")) { SkadiErrorDisplay.NotFindProperty("_BlendMode"); return; }

            renderer.color = col;
            mat.SetFloat("_BlendMode", (float)blendMode);
        }

        /// <summary>
        /// アウトラインのカラーを変更します。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="col">変更したい色</param>
        public static void SetOutlineColor(SpriteRenderer renderer, Color col)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_OutlineColor")) { SkadiErrorDisplay.NotFindProperty("_OutlineColor"); return; }
            
            mat.SetColor("_OutlineColor", col);
        }

        /// <summary>
        /// アウトラインを有効、無効化します。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="isEnable">アウトラインの有効、無効化</param>
        public static void EnableOutline(SpriteRenderer renderer, bool isEnable)
        {
            Material mat = renderer.material;

            if(!mat.HasProperty("_UseOutline")) { SkadiErrorDisplay.NotFindProperty("_UseOutline"); return; }

            mat.SetInt("_UseOutline", Convert.ToInt32(isEnable));
        }

        /// <summary>
        /// アウトラインを使用しているか調べます。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        public static bool GetUseOutlineProp(SpriteRenderer renderer)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UseOutline")) { SkadiErrorDisplay.NotFindProperty("_UseOutline"); return false; }

            return Convert.ToBoolean(mat.GetInt("_UseOutline"));
        }

        /// <summary>
        /// エミッションを有効、無効化します。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="isEnable">エミッションの有効、無効化</param>
        public static void EnableEmission(SpriteRenderer renderer, bool isEnable)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UseEmission")) { SkadiErrorDisplay.NotFindProperty("_UseEmission"); return; }

            mat.SetInt("_UseEmission", Convert.ToInt32(isEnable));
        }

        /// <summary>
        /// エミッションを使用しているか調べます。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        public static bool GetUseEmission(SpriteRenderer renderer)
        {
            Material mat = renderer.material;

            if(!mat.HasProperty("_UseEmission")) { SkadiErrorDisplay.NotFindProperty("_UseEmission"); return false; }

            return Convert.ToBoolean(mat.GetInt("_UseEmission"));
        }

        /// <summary>
        /// エミッション強度を設定します。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="power">エミッション強度</param>
        public static void SetEmissionPower(SpriteRenderer renderer, float power)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_EmissionPower")) { SkadiErrorDisplay.NotFindProperty("_EmissionPower"); return; }

            mat.SetFloat("_EmissionPower", power);
        }

        /// <summary>
        /// エミッションの点滅モードを切り替えます。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="mode">点滅モード</param>
        public static void SetFlickerMode(SpriteRenderer renderer, FlickerMode mode)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_Flicker")) { SkadiErrorDisplay.NotFindProperty("_Flicker"); return; }

            mat.SetInt("_Flicker", (int)mode);
        }

        /// <summary>
        /// 一秒間に点滅する回数を変えます。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="frequency">振動数</param>
        public static void SetFrequency(SpriteRenderer renderer, float frequency)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_Frequency")) { SkadiErrorDisplay.NotFindProperty("_Frequency"); return; }

            mat.SetFloat("_Frequency", frequency);
        }

        /// <summary>
        /// UVスクロールを有効、無効化します。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="isEnable">UVスクロールの有効、無効化</param>
        public static void EnableUVScroll(SpriteRenderer renderer, bool isEnable)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UseUVScroll")) { SkadiErrorDisplay.NotFindProperty("_UseUVScroll"); return; }

            mat.SetInt("_UseUVScroll", Convert.ToInt32(isEnable));
        }

        /// <summary>
        /// UVスクロールを使用しているか調べます。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        public static bool GetUseUVScroll(SpriteRenderer renderer)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UseUVScroll")) { SkadiErrorDisplay.NotFindProperty("_UseUVScroll"); return false; }

            return Convert.ToBoolean(mat.GetInt("_UseUVScroll"));
        }

        /// <summary>
        /// UVスクロールの速度を設定します。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="vector2">X、Y方向の速度</param>
        public static void SetUVScrollSpeed(SpriteRenderer renderer, Vector2 vector2)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UVScroll_X")) { SkadiErrorDisplay.NotFindProperty("_UVScroll_X"); return; }
            if (!mat.HasProperty("_UVScroll_Y")) { SkadiErrorDisplay.NotFindProperty("_UVScroll_Y"); return; }

            mat.SetFloat("_UVScroll_X", vector2.x);
            mat.SetFloat("_UVScroll_Y", vector2.y);
        }

        /// <summary>
        /// ポリゴンを回転させます。
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="angle">回転角度</param>
        /// <param name="pivot">中心点をずらす</param>
        public static void SetRotation(SpriteRenderer renderer, float angle, Vector2 pivot)
        {
            Material mat = renderer.material;

            if(!mat.HasProperty("_Angle")) { SkadiErrorDisplay.NotFindProperty("_Angle"); return; }
            if(!mat.HasProperty("_Pivot")) { SkadiErrorDisplay.NotFindProperty("_Pivot"); return; }

            mat.SetFloat("_Angle", angle);
            mat.SetVector("_Pivot", new Vector4(pivot.x, pivot.y, 0f, 0f));
        }
    }
}

