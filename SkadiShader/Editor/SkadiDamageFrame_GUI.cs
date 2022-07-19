using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace AyahaShader.Skadi
{
    public class SkadiDamageFrame_GUI : ShaderGUI
    {
        // Color
        private MaterialProperty FrameColor;

        // Size
        private MaterialProperty SizeX;
        private MaterialProperty SizeY;

        // Flicker
        private MaterialProperty Flicker;
        private MaterialProperty Frequency;

        private int selectFlicker;

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] Prop)
        {
            var material = (Material)materialEditor.target;
            FindProperties(Prop);

            Information();

            GUIPartition();

            // èâä˙èÛë‘ÇÃGUIÇï\é¶Ç≥ÇπÇÈ
            //base.OnGUI(materialEditor, Prop);

            SkadiCustomUI.Title("Main");
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                materialEditor.ShaderProperty(FrameColor, new GUIContent("Frame Color"));
            }

            GUIPartition();

            SkadiCustomUI.Title("FrameSize");
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                materialEditor.ShaderProperty(SizeX, new GUIContent("SizeX"));
                materialEditor.ShaderProperty(SizeY, new GUIContent("SizeY"));
            }

            GUIPartition();

            SkadiCustomUI.Title("Flicker");
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                GUILayout.Label("Flicker");
                selectFlicker = material.GetInt("_Flicker");
                Texture[] textures = new Texture[5];
                textures[(int)FlickerMode.Line] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Line.png");
                textures[(int)FlickerMode.Sin] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Sin.png");
                textures[(int)FlickerMode.Saw] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Saw.png");
                textures[(int)FlickerMode.Triangle] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Triangle.png");
                textures[(int)FlickerMode.Square] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Square.png");
                selectFlicker = GUILayout.Toolbar(selectFlicker, textures, GUILayout.Height(30));
                material.SetInt("_Flicker", selectFlicker);

                if (selectFlicker != (int)FlickerMode.Line)
                {
                    materialEditor.ShaderProperty(Frequency, new GUIContent("Frequency"));
                }
            }
        }

        private void FindProperties(MaterialProperty[] _Prop)
        {
            // Main
            FrameColor = FindProperty("_Color", _Prop, false);

            // Size
            SizeX = FindProperty("_SizeX", _Prop, false);
            SizeY = FindProperty("_SizeY", _Prop, false);

            // Flicker
            Flicker = FindProperty("_Flicker", _Prop, false);
            Frequency = FindProperty("_Frequency", _Prop, false);
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

