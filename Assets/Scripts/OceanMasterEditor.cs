using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OceanMaster))]
public class OceanMasterEditor : Editor
{
    private OceanMaster oceanMaster;
    private void OnEnable()
    {
        oceanMaster = target as OceanMaster;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Apply Materials Properties"))
        {
            oceanMaster.ApplyMaterialsProperties();
        }
    }
}
