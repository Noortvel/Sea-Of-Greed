using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesBumpTest : MonoBehaviour
{
    [SerializeField, Header("Keycode.B for bumpgenerate")]
    private OceanMaster ocean = null;
    [SerializeField]
    private Vector2Int textureSize;
    [SerializeField]
    private float divideImageFitter = 1;

    [SerializeField]
    private float heightMaxHeight = 0;
    [SerializeField]
    private float heightNegativeFixOffset = 0;

    private Texture2D texture;
    private RawImage rawImage;

    
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        texture = new Texture2D(textureSize.x, textureSize.y);
        rawImage.texture = texture;
    }

    private void Generate(Func<float, float, Vector3> genrator)
    {
        for (int y = 0; y < textureSize.y; y++)
        {
            for (int x = 0; x < textureSize.x; x++)
            {
                var generated = genrator(x / divideImageFitter, y / divideImageFitter);
                var color = new Color(generated.x, generated.y, generated.z);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }
    private Vector3 NormalBumpGenerate(float x, float z)
    {
        var normal = ocean.GetNormal(new Vector3(x, 0, z));
        return normal;
    }
    private float _heigtCalcedMaxVal = float.MinValue;
    private float _heigtCalcedMinVal = float.MaxValue;

    private Vector3 HeightBumpGenerate(float x, float z)
    {
        Vector3 height = ocean.GetPosition(new Vector3(x, 0, z));
        if (height.y > _heigtCalcedMaxVal) _heigtCalcedMaxVal = height.y;
        if (height.y < _heigtCalcedMinVal) _heigtCalcedMinVal = height.y;
        return (Vector3.one * ((height.y + heightNegativeFixOffset) / heightMaxHeight));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Generate(NormalBumpGenerate);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Generate(HeightBumpGenerate);
            Debug.Log($"[{_heigtCalcedMinVal},{_heigtCalcedMaxVal}]");
        }
    }
}
