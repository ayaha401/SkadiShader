using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AyahaShader.Skadi
{
    /// <summary>
    /// �_���[�W�t���[���̃}�e���A���𒲐����邽�߂̃X�N���v�g
    /// </summary>
    public static class SkadiDamageFrameShaderProvider
    {
        /// <summary>
        /// �}�e���A���̐F��ς��܂�
        /// </summary>
        /// <param name="mat">�}�e���A��</param>
        /// <param name="col">�ς������F</param>
        public static void SetColor(Material mat, Color col)
        {
            if (!mat.HasProperty("_Color")) { SkadiErrorDisplay.NotFindProperty("_Color"); return; }

            mat.SetColor("_Color", col);
        }

        /// <summary>
        /// �t���[���̑傫����ύX����
        /// </summary>
        /// <param name="mat">�}�e���A��</param>
        /// <param name="size">�g�̑傫��</param>
        public static void SetFrameSize(Material mat, Vector2 size)
        {
            if (!mat.HasProperty("_SizeX")) { SkadiErrorDisplay.NotFindProperty("_SizeX"); return; }
            if (!mat.HasProperty("_SizeY")) { SkadiErrorDisplay.NotFindProperty("_SizeY"); return; }

            mat.SetFloat("_SizeX", size.x);
            mat.SetFloat("_SizeY", size.y);
        }
    }
}