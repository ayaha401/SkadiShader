using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AyahaShader.Skadi
{
    public static class SkadiCircleFadeProvider
    {
        /// <summary>
        /// フェードするターゲット地点を設定する
        /// </summary>
        /// <param name="material">マテリアル</param>
        /// <param name="posWS">ターゲットの座標、ワールド座標</param>
        public static void SetFadeTargetPos(Material material, Vector2 posWS)
        {
            if(!material.HasProperty("_FadeTarget")) { SkadiErrorDisplay.NotFindProperty("_FadeTarget"); return; }

            Vector2 posVS = Camera.main.WorldToViewportPoint(posWS);
            posVS *= 2f;
            posVS = new Vector2(posVS.x - 1f, posVS.y - 1f);
            material.SetVector("_FadeTarget", new Vector4(posVS.x, posVS.y, 0f, 0f));
        }

        /// <summary>
        /// フェードの色を設定する
        /// </summary>
        /// <param name="material">マテリアル</param>
        /// <param name="col">フェードの色</param>
        public static void SetColor(Material material, Color col)
        {
            if(!material.HasProperty("_Color")) { SkadiErrorDisplay.NotFindProperty("_Color"); return; }

            material.SetColor("_Color", col);
        }

        /// <summary>
        /// フェードの割合を変える
        /// </summary>
        /// <param name="material">マテリアル</param>
        /// <param name="ratio">割合</param>
        public static void SetFadeRatio(Material material, float ratio)
        {
            if(!material.HasProperty("_Fadeout")) { SkadiErrorDisplay.NotFindProperty("_Fadeout"); return; }

            material.SetFloat("_Fadeout", ratio);
        }
    }
}

