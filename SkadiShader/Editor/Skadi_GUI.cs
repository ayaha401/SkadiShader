using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Skadi_GUI : ShaderGUI
{
    private string version = "0.1.9";

    // Texture
    private MaterialProperty MainTex;
    private MaterialProperty LightingMask;
    private MaterialProperty OETex;

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

    private bool uvScrollFoldout;
    private bool outlineFoldout;
    private bool emissionFoldout;
    private int selectFlicker;





    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] Prop)
    {
        var material = (Material)materialEditor.target;
        FindProperties(Prop);

        Information();

        GUIPartition();

        // èâä˙èÛë‘ÇÃGUIÇï\é¶Ç≥ÇπÇÈ
        //base.OnGUI(materialEditor, Prop);


        CustomUI.Title("Main");
        using (new EditorGUILayout.VerticalScope(GUI.skin.box))
        {
            materialEditor.TexturePropertySingleLine(new GUIContent("Main Texture"), MainTex);

            materialEditor.TexturePropertySingleLine(new GUIContent("OE Texture"), OETex);

            if(LightingMask != null)
            {
                materialEditor.TexturePropertySingleLine(new GUIContent("LightingMask Texture"), LightingMask);
            }

            materialEditor.ShaderProperty(BlendMode, new GUIContent("BlendMode"));

            if (material.GetInt("_UseUVScroll") == 1) uvScrollFoldout = true;
            uvScrollFoldout = EditorGUILayout.ToggleLeft("UseUVScroll", uvScrollFoldout);
            if(uvScrollFoldout)
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

        GUIPartition();

        // Outline
        if(UseOutline != null)
        {
            if (material.GetInt("_UseOutline") == 1) outlineFoldout = true;
            outlineFoldout = CustomUI.Foldout("UseOutline", outlineFoldout);
            if (outlineFoldout)
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

        GUIPartition();

        // Emission
        if (UseEmission != null)
        {
            if (material.GetInt("_UseEmission") == 1) emissionFoldout = true;
            emissionFoldout = CustomUI.Foldout("UseEmission", emissionFoldout);
            if (emissionFoldout)
            {
                material.SetInt("_UseEmission", 1);
                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    materialEditor.ShaderProperty(EmissionPower, new GUIContent("EmissionPower"));
                    
                    GUILayout.Label("Flicker");
                    selectFlicker = material.GetInt("_Flicker");
                    Texture[] textures = new Texture[5];
                    textures[0] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Line.png");
                    textures[1] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Sin.png");
                    textures[2] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Saw.png");
                    textures[3] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Triangle.png");
                    textures[4] = AssetDatabase.LoadAssetAtPath<Texture>("Assets/AyahaShader/SkadiShader/GUIImage/Square.png");
                    selectFlicker = GUILayout.Toolbar(selectFlicker, textures, GUILayout.Height(30));
                    material.SetInt("_Flicker", selectFlicker);
                    
                    
                    materialEditor.ShaderProperty(Frequency, new GUIContent("Frequency"));
                }
            }
            else
            {
                material.SetInt("_UseEmission", 0);
            }
        }
        

        GUIPartition();
        


    }

    

    private void FindProperties(MaterialProperty[] _Prop)
    {
        // Texture
        MainTex = FindProperty("_MainTex", _Prop, false);
        LightingMask = FindProperty("_LightingMask", _Prop, false);
        OETex = FindProperty("_OETex", _Prop, false);

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
    }

    private void GUIPartition()
    {
        GUI.color = Color.gray;
        GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
        GUI.color = Color.white;
    }

    private void Information()
    {
        using (new EditorGUILayout.VerticalScope())
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Version");
                EditorGUILayout.LabelField("Version " + version);
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

static class CustomUI
{
    public static bool Foldout(string label, bool value)
    {
        var style = new GUIStyle("ShurikenModuleTitle");
        style.font = new GUIStyle(EditorStyles.label).font;
        style.border = new RectOffset(15, 7, 4, 4);
        style.fixedHeight = 22;
        style.contentOffset = new Vector2(20f, -2f);

        var rect = GUILayoutUtility.GetRect(16f, 22f, style);
        GUI.Box(rect, label, style);

        var e = Event.current;

        var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
        if (e.type == EventType.Repaint)
        {
            EditorStyles.toggle.Draw(toggleRect, false, false, value, false);
        }

        if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
        {
            value = !value;
            e.Use();
        }

        return value;
    }

    public static void Title(string label)
    {
        var style = new GUIStyle("ShurikenModuleTitle");
        style.font = new GUIStyle(EditorStyles.label).font;
        style.border = new RectOffset(15, 7, 4, 4);
        style.fixedHeight = 22;
        style.contentOffset = new Vector2(20f, -2f);

        var rect = GUILayoutUtility.GetRect(16f, 22f, style);
        GUI.Box(rect, label, style);
    }
}
