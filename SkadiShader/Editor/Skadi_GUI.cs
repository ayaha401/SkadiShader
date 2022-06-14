using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Skadi_GUI : ShaderGUI
{
    private string version = "0.1.8";


    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] Prop)
    {
        var material = (Material)materialEditor.target;

        Information();

        GUIPartition();

        // èâä˙èÛë‘ÇÃGUIÇï\é¶Ç≥ÇπÇÈ
        base.OnGUI(materialEditor, Prop);


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
