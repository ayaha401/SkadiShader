using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AyahaShader.Skadi
{
    public static class SkadiSpriteShaderProvider
    {
        /// <summary>
        /// �}�e���A���̐F��ς��܂��BSpriteRenderer.Color�Ƃقړ��`
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        /// <param name="col">�����������F</param>
        /// <param name="blendMode">Multi : ��Z, Fill : �h��Ԃ�
        /// </param>
        public static void SetColor(SpriteRenderer renderer, Color col, ColorBlendMode blendMode = ColorBlendMode.Multi)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_BlendMode")) { NotFindProperty("_BlendMode"); return; }

            renderer.color = col;
            mat.SetFloat("_BlendMode", (float)blendMode);
        }

        /// <summary>
        /// �A�E�g���C���̃J���[��ύX���܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        /// <param name="col">�ύX�������F</param>
        public static void SetOutlineColor(SpriteRenderer renderer, Color col)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_OutlineColor")) { NotFindProperty("_OutlineColor"); return; }
            
            mat.SetColor("_OutlineColor", col);
        }

        /// <summary>
        /// �A�E�g���C����L���A���������܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        /// <param name="isEnable">�A�E�g���C���̗L���A������</param>
        public static void EnableOutline(SpriteRenderer renderer, bool isEnable)
        {
            Material mat = renderer.material;

            if(!mat.HasProperty("_UseOutline")) { NotFindProperty("_UseOutline"); return; }

            mat.SetInt("_UseOutline", Convert.ToInt32(isEnable));
        }

        /// <summary>
        /// �A�E�g���C�����g�p���Ă��邩���ׂ܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        public static bool GetUseOutlineProp(SpriteRenderer renderer)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UseOutline")) { NotFindProperty("_UseOutline"); return false; }

            return Convert.ToBoolean(mat.GetInt("_UseOutline"));
        }

        /// <summary>
        /// �G�~�b�V������L���A���������܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        /// <param name="isEnable">�G�~�b�V�����̗L���A������</param>
        public static void EnableEmission(SpriteRenderer renderer, bool isEnable)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UseEmission")) { NotFindProperty("_UseEmission"); return; }

            mat.SetInt("_UseEmission", Convert.ToInt32(isEnable));
        }

        /// <summary>
        /// �G�~�b�V�������g�p���Ă��邩���ׂ܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        public static bool GetUseEmission(SpriteRenderer renderer)
        {
            Material mat = renderer.material;

            if(!mat.HasProperty("_UseEmission")) { NotFindProperty("_UseEmission"); return false; }

            return Convert.ToBoolean(mat.GetInt("_UseEmission"));
        }

        /// <summary>
        /// �G�~�b�V�������x��ݒ肵�܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        /// <param name="power">�G�~�b�V�������x</param>
        public static void SetEmissionPower(SpriteRenderer renderer, float power)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_EmissionPower")) { NotFindProperty("_EmissionPower"); return; }

            mat.SetFloat("_EmissionPower", power);
        }

        /// <summary>
        /// �G�~�b�V�����̓_�Ń��[�h��؂�ւ��܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        /// <param name="mode">�_�Ń��[�h</param>
        public static void SetFlickerMode(SpriteRenderer renderer, FlickerMode mode)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_Flicker")) { NotFindProperty("_Flicker"); return; }

            mat.SetInt("_Flicker", (int)mode);
        }

        /// <summary>
        /// ��b�Ԃɓ_�ł���񐔂�ς��܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        /// <param name="frequency">�U����</param>
        public static void SetFrequency(SpriteRenderer renderer, float frequency)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_Frequency")) { NotFindProperty("_Frequency"); return; }

            mat.SetFloat("_Frequency", frequency);
        }

        /// <summary>
        /// UV�X�N���[����L���A���������܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        /// <param name="isEnable">UV�X�N���[���̗L���A������</param>
        public static void EnableUVScroll(SpriteRenderer renderer, bool isEnable)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UseUVScroll")) { NotFindProperty("_UseUVScroll"); return; }

            mat.SetInt("_UseUVScroll", Convert.ToInt32(isEnable));
        }

        /// <summary>
        /// UV�X�N���[�����g�p���Ă��邩���ׂ܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        public static bool GetUseUVScroll(SpriteRenderer renderer)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UseUVScroll")) { NotFindProperty("_UseUVScroll"); return false; }

            return Convert.ToBoolean(mat.GetInt("_UseUVScroll"));
        }

        /// <summary>
        /// UV�X�N���[���̑��x��ݒ肵�܂��B
        /// </summary>
        /// <param name="renderer">�����_���[</param>
        /// <param name="vector2">X�AY�����̑��x</param>
        public static void SetUVScrollSpeed(SpriteRenderer renderer, Vector2 vector2)
        {
            Material mat = renderer.material;

            if (!mat.HasProperty("_UVScroll_X")) { NotFindProperty("_UVScroll_X"); return; }
            if (!mat.HasProperty("_UVScroll_Y")) { NotFindProperty("_UVScroll_Y"); return; }

            mat.SetFloat("_UVScroll_X", vector2.x);
            mat.SetFloat("_UVScroll_Y", vector2.y);
        }


        /// <summary>
        /// �v���p�e�B��������Ȃ������ꍇ�ɃG���[���o���܂��B
        /// </summary>
        private static void NotFindProperty(string propertyName)
        {
            Debug.Log("Not find " + propertyName);
        }
    }
}

