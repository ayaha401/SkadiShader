using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AyahaShader.Skadi
{
    public static class SkadiCircleFadeProvider
    {
        /// <summary>
        /// �t�F�[�h����^�[�Q�b�g�n�_��ݒ肷��
        /// </summary>
        /// <param name="material">�}�e���A��</param>
        /// <param name="posWS">�^�[�Q�b�g�̍��W�A���[���h���W</param>
        public static void SetFadeTargetPos(Material material, Vector2 posWS)
        {
            if(!material.HasProperty("_FadeTarget")) { SkadiErrorDisplay.NotFindProperty("_FadeTarget"); return; }

            Vector2 posVS = Camera.main.WorldToViewportPoint(posWS);
            posVS *= 2f;
            posVS = new Vector2(posVS.x - 1f, posVS.y - 1f);
            material.SetVector("_FadeTarget", new Vector4(posVS.x, posVS.y, 0f, 0f));
        }

        /// <summary>
        /// �t�F�[�h�̐F��ݒ肷��
        /// </summary>
        /// <param name="material">�}�e���A��</param>
        /// <param name="col">�t�F�[�h�̐F</param>
        public static void SetColor(Material material, Color col)
        {
            if(!material.HasProperty("_Color")) { SkadiErrorDisplay.NotFindProperty("_Color"); return; }

            material.SetColor("_Color", col);
        }

        /// <summary>
        /// �t�F�[�h�̊�����ς���
        /// </summary>
        /// <param name="material">�}�e���A��</param>
        /// <param name="ratio">����</param>
        public static void SetFadeRatio(Material material, float ratio)
        {
            if(!material.HasProperty("_Fadeout")) { SkadiErrorDisplay.NotFindProperty("_Fadeout"); return; }

            material.SetFloat("_Fadeout", ratio);
        }
    }
}

