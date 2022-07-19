using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AyahaShader.Skadi
{
    /// <summary>
    /// SkadiShaderのエラーを表示するクラス
    /// </summary>
    public static class SkadiErrorDisplay
    {
        /// <summary>
        /// プロパティが見つからなかった場合にエラーを出します。
        /// </summary>
        public static void NotFindProperty(string propertyName)
        {
            Debug.Log("Not find " + propertyName);
        }
    }
}