using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AyahaShader.Skadi
{
    /// <summary>
    /// SkadiShader�̃t�F�[�h�A�E�g�̃V�F�[�_�[�𒲐����邽�߂̃X�N���v�g
    /// </summary>
    public static class SkadiScreenFadeShaderProvider
    {
        /// <summary>
        /// �t�F�[�h�A�E�g�̊�����ς��A�������ς������
        /// </summary>
        /// <param name="mat">�}�e���A��</param>
        /// <param name="fadeoutRatio">�t�F�[�h�A�E�g�̊���</param>
        /// <param name="dir">�t�F�[�h�A�E�g�̕���</param>
        public static void SetFadeRatioAndDir(Material mat, float fadeoutRatio, FadeDir dir = FadeDir.Right)
        {
            if(!mat.HasProperty("_Fadeout")) { SkadiErrorDisplay.NotFindProperty("_Fadeout"); return; }
            if(!mat.HasProperty("_FadeDir")) { SkadiErrorDisplay.NotFindProperty("_FadeDir"); return; }

            mat.SetInt("_FadeDir", (int)dir);
            mat.SetFloat("_Fadeout", fadeoutRatio);
        }

        /// <summary>
        /// �t�F�[�h�A�E�g�̊�����ς���
        /// </summary>
        /// <param name="mat">�}�e���A��</param>
        /// <param name="fadeoutRatio">�t�F�[�h�A�E�g�̊���</param>
        public static void SetFadeRatio(Material mat, float fadeoutRatio)
        {
            if (!mat.HasProperty("_Fadeout")) { SkadiErrorDisplay.NotFindProperty("_Fadeout"); return; }

            mat.SetFloat("_Fadeout", fadeoutRatio);
        }

        /// <summary>
        /// �t�F�[�h�A�E�g�̕�����ς���
        /// </summary>
        /// <param name="mat">�}�e���A��</param>
        /// <param name="dir">�t�F�[�h�A�E�g�̕���</param>
        public static void SetFadeDir(Material mat, FadeDir dir)
        {
            if (!mat.HasProperty("_FadeDir")) { SkadiErrorDisplay.NotFindProperty("_FadeDir"); return; }
           
            mat.SetInt("_FadeDir", (int)dir);
        }
    }
}

