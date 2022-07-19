using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AyahaShader.Skadi
{
    /// <summary>
    /// ダメージフレームのマテリアルを調整するためのスクリプト
    /// </summary>
    public static class SkadiDamageFrameShaderProvider
    {
        /// <summary>
        /// マテリアルの色を変えます
        /// </summary>
        /// <param name="mat">マテリアル</param>
        /// <param name="col">変えたい色</param>
        public static void SetColor(Material mat, Color col)
        {
            if (!mat.HasProperty("_Color")) { SkadiErrorDisplay.NotFindProperty("_Color"); return; }

            mat.SetColor("_Color", col);
        }

        /// <summary>
        /// フレームの大きさを変更する
        /// </summary>
        /// <param name="mat">マテリアル</param>
        /// <param name="size">枠の大きさ</param>
        public static void SetFrameSize(Material mat, Vector2 size)
        {
            if (!mat.HasProperty("_SizeX")) { SkadiErrorDisplay.NotFindProperty("_SizeX"); return; }
            if (!mat.HasProperty("_SizeY")) { SkadiErrorDisplay.NotFindProperty("_SizeY"); return; }

            mat.SetFloat("_SizeX", size.x);
            mat.SetFloat("_SizeY", size.y);
        }
    }
}