using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SinusShapedWave
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
        float aplidute1 = waveStartHeight * ShapedSinus(k * val - w * time + faza) / time2;
        float aplidute2 = waveStartHeight * ShapedSinus(k * val2 - w * time + faza) / time2;
        float height = (aplidute1 + aplidute2) / 2;
        return height;
    }
    public float ShapedSinus(float x)
    {
        return 1 - 2 * Mathf.Abs(Mathf.Sin(x));
    }
    public float ShapedCosinus(float x)
    {
        return 1 - 2 * Mathf.Abs(Mathf.Cos(x));

    }
    public float DDX_GetWaveHeight2(float x, float z)
    {
        float time = Time.time + 1;
        float k = waveCount;
        float w = speedOscillation;
        float time2 = Mathf.Min(time, waveMinHeight);
        var pos = new Vector2(x, z);
        var val = Vector3.Dot(pos, direction);
        var xparam = k * val - w * time + faza;
        float aplidute1 = direction.x * k * waveStartHeight / time2
            * Mathf.Sin(2 * x) / Mathf.Abs(Mathf.Cos(xparam));
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
        var xparam = k * val - w * time + faza;

        float aplidute1 = direction.y * k * waveStartHeight / time2
            * Mathf.Sin(2 * x) / Mathf.Abs(Mathf.Cos(xparam));
        return aplidute1;
    }
}