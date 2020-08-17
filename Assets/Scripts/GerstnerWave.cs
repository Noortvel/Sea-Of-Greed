using System;
using UnityEngine;

[Serializable]
public class GerstnerWave
{
    public Vector2 direction;
    public float lenght;
    public float amplitude;
    public float stepness;
    public float speed;

    private static int WavesCount = 3;


    private const float G = 9.8f;

    public float _QD = 0;
    public Vector3 GetPosition(Vector3 position)
    {
        float time = Time.time;
        float w = Mathf.Sqrt((G * 2 * Mathf.PI) / lenght);
        float q = stepness / (w * amplitude * WavesCount);
        _QD = q;
        direction.Normalize();

        float xArg = w * Vector3.Dot(direction, new Vector3(position.x, position.z)) + speed * time;

        float xVal = q * amplitude * direction.x * Mathf.Cos(xArg);
        float zVal = q * amplitude * direction.y * Mathf.Cos(xArg);
        float yVal = amplitude * Mathf.Sin(xArg);
        return new Vector3(xVal, yVal, zVal);
    }
    public Vector3 GetTangent(Vector3 position)
    {
        float w = Mathf.Sqrt((G * 2 * Mathf.PI) / lenght);
        float wa = w * amplitude;

        float q = stepness / (wa * WavesCount);

        float xArg = w * Vector3.Dot(direction, position) + speed * Time.time;

        float x = -q * direction.x * direction.x * wa * Mathf.Sin(xArg);
        float z = -q * direction.x * direction.y * wa * Mathf.Sin(xArg); 
        float y = direction.x * wa * Mathf.Cos(xArg);
        return new Vector3(x, y, z);
    }
    public Vector3 GetNormal(Vector3 position)
    {
        float w = Mathf.Sqrt((G * 2 * Mathf.PI) / lenght);
        float wa = w * amplitude;

        float q = stepness / (wa * WavesCount);

        float xArg = w * Vector3.Dot(direction, position) + speed * Time.time;

        float x = -direction.x * wa * Mathf.Cos(xArg);
        float z = -direction.y * wa * Mathf.Cos(xArg);
        float y = -q * wa * Mathf.Sin(xArg);
        return new Vector3(x, y, z);
    }
}

