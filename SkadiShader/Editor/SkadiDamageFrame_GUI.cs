using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace AyahaShader.Skadi
{
    public class SkadiDamageFrame_GUI : ShaderGUI
    {
        // Texture 
        private MaterialProperty MainTex;

        // Color
        private MaterialProperty FrameColor;

        // Size
        private MaterialProperty SizeX;
        private MaterialProperty SizeY;

        // Flicker
        private MaterialProperty Flicker;

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] Prop)
        {
            var material = (Material)materialEditor.target;
            FindProperties(Prop);

            Information();

            GUIPartition();

            // èâä˙èÛë‘ÇÃGUIÇï\é¶Ç≥ÇπÇÈ
            base.OnGUI(materialEditor, Prop);



        }

        private void FindProperties(MaterialProperty[] _Prop)
        {
            // Texture
            MainTex = FindProperty("_MainTex", _Prop, false);
            FrameColor = FindProperty("_Color", _Prop, false);
            SizeX = FindProperty("_SizeX", _Prop, false);
            SizeY = FindProperty("_SizeY", _Prop, false);
            Flicker = FindProperty("_Flicker", _Prop, false);
        }

        private void GUIPartition()
        {
            GUI.color = Color.gray;
            GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            GUI.color = Color.white;
        }

        private void Information()
        {
            SkadiCustomUI.Title("Info");
            using (new EditorGUILayout.VerticalScope())
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("Version");
                    EditorGUILayout.LabelField("Version " + Skadi_Version.GetSkadiVersion());
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("How to use (Japanese)");
                    if (GUILayout.Button("How to use (Japanese)"))
                    {
                        System.Diagnostics.Process.Start("https://github.com/ayaha401/SkadiShader");
                    }
                }
            }
        }
    }
}

