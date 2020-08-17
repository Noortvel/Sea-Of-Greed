using System;
using UnityEngine;

public class OceanMaster : MonoBehaviour
{
    [SerializeField]
    private Material waterMaterial = null;
    [SerializeField]
    private Material foamTrailMaterial = null;

    [SerializeField]
    private GerstnerWave[] gerstnerWaves;
    

    void Awake()
    {
        //ApplyMaterialsProperties();
    }

    /// <summary>
    /// Passed wave params to material, direction need normalized
    /// </summary>
    public void ApplyMaterialsProperties()
    {
        var materials = new[]{waterMaterial, foamTrailMaterial};
        foreach (var material in materials)
        {
            if (material != null)
            {
                for (int i = 0; i < gerstnerWaves.Length; i++)
                {
                    var wave = gerstnerWaves[i];
                    var dirProp = Shader.PropertyToID($"Vector2_Wave{i + 1}Direction");
                    material.SetVector(dirProp, wave.direction.normalized);
                    var parmasId = Shader.PropertyToID($"Vector4_Wave{i + 1}LenAmplStepSpeed");
                    var waveParams = new Vector4(wave.lenght, wave.amplitude, wave.stepness, wave.speed);
                    material.SetVector(parmasId, waveParams);
                }
            }
            else
            {
                Debug.LogWarning($"material '{material.name}' not setted, apply do not work");
            }
        }
    }

    public Vector3 GetPosition(Vector3 vertexPosition)
    {
        var position = vertexPosition;
        position.y = transform.position.y;
        foreach (var wave in gerstnerWaves)
        {
            position += wave.GetPosition(vertexPosition);
        }
        return position;
    }
    public Vector3 GetTanget(Vector3 vertexPosition)
    {
        var tangent = new Vector3(1, 0, 0);
        foreach (var wave in gerstnerWaves)
        {
            tangent += wave.GetTangent(vertexPosition);
        }
        return tangent;
    }
    public Vector3 GetNormal(Vector3 vertexPosition)
    {
        var normal = new Vector3(0, 1, 0);
        foreach (var wave in gerstnerWaves)
        {
            normal += wave.GetNormal(vertexPosition);
        }
        return normal;
    }
}