using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AyahaShader.Skadi
{
    /// <summary>
    /// SkadiShaderのフェードアウトのシェーダーを調整するためのスクリプト
    /// </summary>
    public static class SkadiScreenFadeShaderProvider
    {
        /// <summary>
        /// フェードアウトの割合を変え、方向も変える方向
        /// </summary>
        /// <param name="mat">マテリアル</param>
        /// <param name="fadeoutRatio">フェードアウトの割合</param>
        /// <param name="dir">フェードアウトの方向</param>
        public static void SetFadeRatioAndDir(Material mat, float fadeoutRatio, FadeDir dir = FadeDir.Right)
        {
            if(!mat.HasProperty("_Fadeout")) { SkadiErrorDisplay.NotFindProperty("_Fadeout"); return; }
            if(!mat.HasProperty("_FadeDir")) { SkadiErrorDisplay.NotFindProperty("_FadeDir"); return; }

            mat.SetInt("_FadeDir", (int)dir);
            mat.SetFloat("_Fadeout", fadeoutRatio);
        }

        /// <summary>
        /// フェードアウトの割合を変える
        /// </summary>
        /// <param name="mat">マテリアル</param>
        /// <param name="fadeoutRatio">フェードアウトの割合</param>
        public static void SetFadeRatio(Material mat, float fadeoutRatio)
        {
            if (!mat.HasProperty("_Fadeout")) { SkadiErrorDisplay.NotFindProperty("_Fadeout"); return; }

            mat.SetFloat("_Fadeout", fadeoutRatio);
        }

        /// <summary>
        /// フェードアウトの方向を変える
        /// </summary>
        /// <param name="mat">マテリアル</param>
        /// <param name="dir">フェードアウトの方向</param>
        public static void SetFadeDir(Material mat, FadeDir dir)
        {
            if (!mat.HasProperty("_FadeDir")) { SkadiErrorDisplay.NotFindProperty("_FadeDir"); return; }
           
            mat.SetInt("_FadeDir", (int)dir);
        }
    }
}

