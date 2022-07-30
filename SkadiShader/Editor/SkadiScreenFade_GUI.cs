using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace AyahaShader.Skadi
{
    public class SkadiScreenFade_GUI : ShaderGUI
    {
        // Fade
        private MaterialProperty Fadeout;
        private MaterialProperty FadeDir;

        // Advanced Settings
        private MaterialProperty ImageSizeX;
        private MaterialProperty ImageSizeY;

        private bool advancedSettingsFoldout = false;

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] Prop)
        {
            var material = (Material)materialEditor.target;
            FindProperties(Prop);

            SkadiCustomUI.Information();

            SkadiCustomUI.GUIPartition();

            // èâä˙èÛë‘ÇÃGUIÇï\é¶Ç≥ÇπÇÈ
            //base.OnGUI(materialEditor, Prop);

            SkadiCustomUI.Title("Fade");
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                materialEditor.ShaderProperty(Fadeout, new GUIContent("Fadeout"));
                materialEditor.ShaderProperty(FadeDir, new GUIContent("FadeDir"));
            }

            advancedSettingsFoldout = SkadiCustomUI.Foldout("Advanced Settings", advancedSettingsFoldout);
            if (advancedSettingsFoldout)
            {
                Vector2 imageSize = new Vector2(ImageSizeX.floatValue, ImageSizeY.floatValue);
                EditorGUILayout.Vector2Field("ImageSize", imageSize);
            }
        }

        private void FindProperties(MaterialProperty[] _Prop)
        {
            // Fade
            Fadeout = FindProperty("_Fadeout", _Prop, false);
            FadeDir = FindProperty("_FadeDir", _Prop, false);


            // ImageSize
            ImageSizeX = FindProperty("_ImageSizeX", _Prop, false);
            ImageSizeY = FindProperty("_ImageSizeY", _Prop, false);
        }
    }
}

