using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AyahaShader.Skadi
{
    /// <summary>
    /// SkadiShader�̃G���[��\������N���X
    /// </summary>
    public static class SkadiErrorDisplay
    {
        /// <summary>
        /// �v���p�e�B��������Ȃ������ꍇ�ɃG���[���o���܂��B
        /// </summary>
        public static void NotFindProperty(string propertyName)
        {
            Debug.Log("Not find " + propertyName);
        }
    }
}