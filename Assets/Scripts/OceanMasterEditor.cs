
#if UNITY_EDITOR
using System.Runtime.InteropServices;
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
        //EditorGUILayout.FloatField(0);
        if (GUILayout.Button("Apply Materials Properties"))
        {
            oceanMaster.ApplyMaterialsProperties();
        }
        if (GUILayout.Button("Fix props to reccomended(len,speed)"))
        {
            oceanMaster.FitParamToReccomendByWaveLen();
        }
    }
}
#endif
