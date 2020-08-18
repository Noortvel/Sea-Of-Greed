using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PerlinNoiseWavesGenerationTest : MonoBehaviour
{
    [Header("Sinus Waves Params")]
    public Vector2 direction;
    public float waveHeight;
    public float waveCount;
    public float speedOscillation;
    public float faza;


    public float GetWaveHeight2(float x, float z)
    {
        float time = Time.time;
        float k = waveCount;
        float w = speedOscillation;
        float time2 = waveHeight;
        var pos = new Vector2(x, z);
        var val = Vector2.Dot(pos, direction);
        var val2 = Vector2.Dot(pos, new Vector2(direction.y, -direction.x));
        float aplidute1 = waveHeight * ShapedSinus(k * val - w + faza);
        float aplidute2 = waveHeight * ShapedSinus(k * val2 - w + faza);
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
    [Header("Philips Spectrum Params")]
    public Vector2 windDirection;
    public float windSpeed;

    private float gravity = Mathf.Abs(Physics.gravity.y);

    public float PhilipsSpectrum(Vector2 k)
    {
        float klen = k.magnitude;
        float k2 = klen * klen;
        float L = windSpeed * windSpeed / gravity;

        float pscl = Vector3.Dot(k, windDirection);
        float p0k = pscl * pscl * Mathf.Exp(-1 / (k2 * L * L)) / (k2 * k2);
        return p0k;
    }


    [SerializeField]
    private Vector2Int textureSize = Vector2Int.one;

    private Texture2D texture;
    private RawImage rawImage;

    [SerializeField, Header("Perlin Generate Keycode.P")]
    private Vector2 perlinParmsScale = Vector2.one;
    [SerializeField, Header("Sinus Generate Keycode.S")]
    private Vector2 sinWavesParmsScale = Vector2.one;
    [SerializeField, Header("Pliphs Generate Keycode.O")]
    private Vector2 philipsParmsScale = Vector2.one;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        texture = new Texture2D(textureSize.x, textureSize.y);
        rawImage.texture = texture;
    }

    private void Generate(Func<float, float, float> genrator)
    {
        for (int y = 0; y < textureSize.y; y++)
        {
            for (int x = 0; x < textureSize.x; x++)
            {
                float generated = genrator(x, y);
                var color = new Color(generated, generated, generated);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }
    private float PerlinGenerator(float x, float y)
    {
        float xP = x / textureSize.x * perlinParmsScale.x + Time.time;
        float yP = y / textureSize.y * perlinParmsScale.y + Time.time;
        float perlin = Mathf.PerlinNoise(xP, yP);
        return perlin;
    }
    private float SinusGenerator(float x, float y)
    {
        float height = GetWaveHeight2(x / (float)textureSize.x * sinWavesParmsScale.x,
                    y / (float)textureSize.x * sinWavesParmsScale.y);
        height = (height / waveHeight + 1) / 2;
        return height;
    }
    private float RandomSinusGenerator(float x, float y)
    {

        float rX = UnityEngine.Random.Range(0, 2 * Mathf.PI);
        float rY = UnityEngine.Random.Range(0, 2 * Mathf.PI);
        float pX = PerlinGenerator(x, y);
        float pY = PerlinGenerator(x, y);

        float height = GetWaveHeight2(pX * 2 * Mathf.PI, pY * 2 * Mathf.PI);
        height = (height / waveHeight + 1) / 2;
        return height;

    }
    private float PerlinNoiseSineGenerator(float x, float y)
    {
        float perlin = PerlinGenerator(x, y);
        float height = SinusGenerator(perlin, perlin);
        return height;
    }
    private Vector2 _philipsTempVec;

    [SerializeField]
    private float philipsHeightScale = 1;
    private float PhilipsGenerator(float x, float y)
    {
        float L = windSpeed * windSpeed / gravity;
        
        _philipsTempVec.x =  2 * Mathf.PI * (x + 1) / textureSize.x * philipsParmsScale.x / L;
        _philipsTempVec.y = 2 * Mathf.PI * (y + 1) / textureSize.y * philipsParmsScale.y / L;
        float height = PhilipsSpectrum(_philipsTempVec) * philipsHeightScale;
        return height;
    }
    private void Render1()
    {
        Generate(RandomSinusGenerator);
    }
    [SerializeField]
    private bool isRender = false;
    void Update()
    {
        if(isRender)
            Render1();
        if (Input.GetKeyDown(KeyCode.P))
        {
            Generate(PerlinGenerator);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Generate(SinusGenerator);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Generate(PhilipsGenerator);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Generate(PerlinNoiseSineGenerator);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Generate(RandomSinusGenerator);
        }
    }
}
