using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SinusWave
{
    public Vector2 direction;
    public float waveStartHeight;
    public float waveMinHeight;
    public float waveCount;
    public float speedOscillation;
    public float faza;

    public float GetWaveHeight2(float x, float z)
    {
        float time = Time.time + 1;
        float k = waveCount;
        float w = speedOscillation;
        float time2 = Mathf.Min(time, waveMinHeight);
        var pos = new Vector2(x, z);
        var val = Vector2.Dot(pos, direction);
        var val2 = Vector2.Dot(pos, new Vector2(direction.y, -direction.x));
        float aplidute1 = waveStartHeight * Mathf.Sin(k * val - w * time + faza) / time2;
        float aplidute2 = waveStartHeight * Mathf.Sin(k * val2 - w * time + faza) / time2;
        return (aplidute1 + aplidute2) / 2;
    }
    public float DDX_GetWaveHeight2(float x, float z)
    {
        float time = Time.time + 1;
        float k = waveCount;
        float w = speedOscillation;
        float time2 = Mathf.Min(time, waveMinHeight);
        var pos = new Vector2(x, z);
        var val = Vector3.Dot(pos, direction);
        float aplidute1 = direction.x * k * waveStartHeight
            * Mathf.Cos(k * val - w * time + faza) / time2;
        return aplidute1;
    }
    public float DDZ_GetWaveHeight2(float x, float z)
    {
        float time = Time.time + 1;
        float k = waveCount;
        float w = speedOscillation;
        float time2 = Mathf.Min(time, waveMinHeight);
        var pos = new Vector2(x, z);
        var val = Vector3.Dot(pos, direction);
        float aplidute1 = direction.y * k * waveStartHeight
            * Mathf.Cos(k * val - w * time + faza) / time2;
        return aplidute1;
    }
    //private void SinusWavesMaterialInitialization()
    //{
    //    sinusWaves.direction = sinusWaves.direction.normalized;

    //    var materials = new[] { waterMaterial, foamTrailMaterial };
    //    foreach (var material in materials)
    //    {
    //        if (material != null)
    //        {
    //            material.SetVector(Shader.PropertyToID("Vector2_WaveDirection"),
    //                sinusWaves.direction);
    //            material.SetFloat(Shader.PropertyToID("Vector1_WaveStartHeight"),
    //                sinusWaves.waveStartHeight);
    //            material.SetFloat(Shader.PropertyToID("Vector1_WaveMinHeight"),
    //                sinusWaves.waveMinHeight);
    //            material.SetFloat(Shader.PropertyToID("Vector1_WaveCount"),
    //                sinusWaves.waveCount);
    //            material.SetFloat(Shader.PropertyToID("Vector1_WaveSpeedOscillation"),
    //                sinusWaves.speedOscillation);
    //            material.SetFloat(Shader.PropertyToID("Vector1_WaveStartFaza"),
    //                sinusWaves.faza);
    //        }
    //    }
    //}
}
