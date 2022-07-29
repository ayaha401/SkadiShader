using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace AyahaShader.Skadi
{
    public class SkadiSprite_GUI : ShaderGUI
    {
        // Texture
        private MaterialProperty MainTex;
        private MaterialProperty LightingMask;
        private MaterialProperty OETex;

        private MaterialProperty UseOutlineDefault;
        private MaterialProperty OutlineDefault;
        private MaterialProperty UseEmissionDefault;
        private MaterialProperty EmissionDefault;
        private MaterialProperty UseStencilDefault;
        private MaterialProperty StencilDefault;

        // UVScroll
        private MaterialProperty UseUVScroll;
        private MaterialProperty UVScroll_X;
        private MaterialProperty UVScroll_Y;

        // Emission
        private MaterialProperty UseEmission;
        private MaterialProperty EmissionPower;
        private MaterialProperty Flicker;
        private MaterialProperty Frequency;

        // Fill
        private MaterialProperty BlendMode;

        // Outline
        private MaterialProperty UseOutline;
        private MaterialProperty OutlineColor;

        // Stencil
        private MaterialProperty StencilNum;
        private MaterialProperty StencilCompMode;
        private MaterialProperty StencilOp;
        private MaterialProperty UseMirrorImage;
        private MaterialProperty MirrorImageDist;
        private MaterialProperty MirrorImageTrans;


        private bool useTexDefault = false;
        private bool useOutlineDefault = false;
        private bool useEmissionDefault = false;
        private bool uvScrollToggleFoldout;
        private bool outlineToggleFoldout;
        private bool emissionToggleFoldout;
        private int selectFlicker;
        private bool advancedSettingsFoldout = false;
        //private bool mirrorImageToggleFoldout = false;





        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] Prop)
        {
            var material = (Material)materialEditor.target;
            FindProperties(Prop);

            // シェーダーのバージョンを表記
            SkadiCustomUI.Information();

            SkadiCustomUI.GUIPartition();

            // 初期状態のGUIを表示させる
            //base.OnGUI(materialEditor, Prop);


            SkadiCustomUI.Title("Main");
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                materialEditor.TexturePropertySingleLine(new GUIContent("Main Texture"), MainTex);

                materialEditor.TexturePropertySingleLine(new GUIContent("OE Texture"), OETex);

                EditorGUI.indentLevel++;
                useTexDefault = EditorGUILayout.Foldout(useTexDefault, "Use OETextureDefault", true);
                if(useTexDefault)
                {
                    EditorGUI.indentLevel++;
                    if (Convert.ToBoolean(UseOutlineDefault.intValue)) useOutlineDefault = true;
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        useOutlineDefault = EditorGUILayout.ToggleLeft("UseOutlineDefault", useOutlineDefault);
                        EditorGUI.BeginDisabledGroup(!useOutlineDefault);
                            materialEditor.ShaderProperty(OutlineDefault,string.Empty);
                        EditorGUI.EndDisabledGroup();
                    }

                    if (Convert.ToBoolean(UseEmissionDefault.intValue)) useEmissionDefault = true;
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        useEmissionDefault = EditorGUILayout.ToggleLeft("UseEmissionDefault", useEmissionDefault);
                        EditorGUI.BeginDisabledGroup(!useEmissionDefault);
                        materialEditor.ShaderProperty(EmissionDefault, string.Empty);
                        EditorGUI.EndDisabledGroup();
                    }

                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;

                if (LightingMask != null)
                {
                    materialEditor.TexturePropertySingleLine(new GUIContent("LightingMask Texture"), LightingMask);
                }

                materialEditor.ShaderProperty(BlendMode, new GUIContent("BlendMode"));

                if (material.GetInt("_UseUVScroll") == 1) uvScrollToggleFoldout = true;
                uvScrollToggleFoldout = EditorGUILayout.ToggleLeft("UseUVScroll", uvScrollToggleFoldout);
                if (uvScrollToggleFoldout)
                {
                    material.SetInt("_UseUVScroll", 1);
                    EditorGUI.indentLevel++;
                    materialEditor.ShaderProperty(UVScroll_X, new GUIContent("UV Scroll X"));
                    materialEditor.ShaderProperty(UVScroll_Y, new GUIContent("UV Scroll Y"));
                    EditorGUI.indentLevel--;
                }
                else
                {
                    material.SetInt("_UseUVScroll", 0);
                }
            }

            SkadiCustomUI.GUIPartition();

            // Outline
            if (UseOutline != null)
            {
                if (material.GetInt("_UseOutline") == 1) outlineToggleFoldout = true;
                outlineToggleFoldout = SkadiCustomUI.ToggleFoldout("UseOutline", outlineToggleFoldout);
                if (outlineToggleFoldout)
                {
                    material.SetInt("_UseOutline", 1);
                    EditorGUI.indentLevel++;
                    using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                    {
                        materialEditor.ShaderProperty(OutlineColor, new GUIContent("OutlineColor"));
                    }
                    EditorGUI.indentLevel--;
                }
                else
                {
                    material.SetInt("_UseOutline", 0);
                }
            }

            SkadiCustomUI.GUIPartition();

            // Emission
            if (UseEmission != null)
            {
                if (material.GetInt("_UseEmission") == 1) emissionToggleFoldout = true;
                emissionToggleFoldout = SkadiCustomUI.ToggleFoldout("UseEmission", emissionToggleFoldout);
                if (emissionToggleFoldout)
                {
                    material.SetInt("_UseEmission", 1);
                    using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                    {
                        materialEditor.ShaderProperty(EmissionPower, new GUIContent("EmissionPower"));

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
                else
                {
                    material.SetInt("_UseEmission", 0);
                }
            }

            SkadiCustomUI.GUIPartition();

            advancedSettingsFoldout = SkadiCustomUI.Foldout("Advanced Settings", advancedSettingsFoldout);
            if (advancedSettingsFoldout)
            {
                // Stencil
                SkadiCustomUI.Title("Stencil");
                //using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                //{
                //    materialEditor.ShaderProperty(StencilNum, new GUIContent("Stencil Number"));
                //    materialEditor.ShaderProperty(StencilCompMode, new GUIContent("Stencil CompMode"));
                //    materialEditor.ShaderProperty(StencilOp, new GUIContent("Stencil Operation"));

                //    if (material.GetInt("_UseMirrorImage") == 1) mirrorImageToggleFoldout = true;
                //    mirrorImageToggleFoldout = EditorGUILayout.ToggleLeft("Use MirrorImage", mirrorImageToggleFoldout);
                //    if(mirrorImageToggleFoldout)
                //    {
                //        material.SetInt("_UseMirrorImage", 1);
                //        EditorGUI.indentLevel++;
                //        materialEditor.ShaderProperty(MirrorImageDist, new GUIContent("MirrorImage Distance"));
                //        materialEditor.ShaderProperty(MirrorImageTrans, new GUIContent("MirrorImage Transparent"));
                //        EditorGUI.indentLevel--;
                //    }
                //    else
                //    {
                //        material.SetInt("_UseMirrorImage", 0);
                //    }
                    
                //    if(GUILayout.Button("Set Default"))
                //    {
                //        material.SetInt("_StencilNum", 0);
                //        material.SetInt("_StencilCompMode", 8);
                //        material.SetInt("_StencilOp", 2);
                //        material.SetInt("_UseMirrorImage", 0);
                //        mirrorImageToggleFoldout = false;
                //        material.SetFloat("_MirrorImageDist", 0.03f);
                //        material.SetFloat("_MirrorImageTrans", 0.5f);
                //    }
                //}
            }
        }

    

        private void FindProperties(MaterialProperty[] _Prop)
        {
            // Texture
            MainTex = FindProperty("_MainTex", _Prop, false);
            LightingMask = FindProperty("_LightingMask", _Prop, false);
            OETex = FindProperty("_OETex", _Prop, false);

            // OESDefault
            UseOutlineDefault = FindProperty("_UseOutlineDefault", _Prop, false);
            OutlineDefault = FindProperty("_OutlineDefault", _Prop, false);
            UseEmissionDefault = FindProperty("_UseEmissionDefault", _Prop, false);
            EmissionDefault = FindProperty("_EmissionDefault", _Prop, false);
            UseStencilDefault = FindProperty("_UseStencilDefault", _Prop, false);
            StencilDefault = FindProperty("_StencilDefault", _Prop, false);

            // UVScroll
            UseUVScroll = FindProperty("_UseUVScroll", _Prop, false);
            UVScroll_X = FindProperty("_UVScroll_X", _Prop, false);
            UVScroll_Y = FindProperty("_UVScroll_Y", _Prop, false);

            // Emission
            UseEmission = FindProperty("_UseEmission", _Prop, false);
            EmissionPower = FindProperty("_EmissionPower", _Prop, false);
            Flicker = FindProperty("_Flicker", _Prop, false);
            Frequency = FindProperty("_Frequency", _Prop, false);

            // Fill
            BlendMode = FindProperty("_BlendMode", _Prop, false);

            // Outline
            UseOutline = FindProperty("_UseOutline", _Prop, false);
            OutlineColor = FindProperty("_OutlineColor", _Prop, false);

            // Stencil
            StencilNum = FindProperty("_StencilNum", _Prop, false);
            StencilCompMode = FindProperty("_StencilCompMode", _Prop, false);
            StencilOp = FindProperty("_StencilOp", _Prop, false);
            UseMirrorImage = FindProperty("_UseMirrorImage", _Prop, false);
            MirrorImageDist = FindProperty("_MirrorImageDist", _Prop, false);
            MirrorImageTrans = FindProperty("_MirrorImageTrans", _Prop, false);
        }
    }
}

