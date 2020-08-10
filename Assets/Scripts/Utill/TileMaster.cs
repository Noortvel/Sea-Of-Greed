using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

#if UNITY_EDITOR
[CustomEditor(typeof(TileMaster))]
public class TileMasterEditor : Editor
{
    private TileMaster tileMaster;
    private void OnEnable()
    {
        tileMaster = target as TileMaster;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Replace tiles"))
        {
            tileMaster.ReplaceTiles();
        }
        if(GUILayout.Button("Clear tiles"))
        {
            tileMaster.ClearTiles();
        }
        if (GUILayout.Button("Print tile size"))
        {
            tileMaster.PrintTileSize();
        }
    }

}

[ExecuteInEditMode]
public class TileMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject tile = null;
    [SerializeField]
    private int xCount = 1, yCount = 1;
    [SerializeField]
    private Vector2 tileSize = Vector2.one;

    public void PrintTileSize()
    {
        var mrend = tile.GetComponent<MeshRenderer>();
        if(mrend == null)
        {
            mrend = tile.GetComponentInChildren<MeshRenderer>();
        }
        if(mrend != null)
        {
            Debug.Log(mrend.bounds.size);
        }
        else
        {
            Debug.Log("Render not finded");
        }
    }
    public void ClearTiles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    }
    public void ReplaceTiles()
    {
        ClearTiles();
        float yHalf = yCount / 2.0f;
        int yUp = Mathf.CeilToInt(yHalf);
        int yDown = -Mathf.FloorToInt(yHalf);

        float xHalf = xCount / 2.0f;
        int xLeft = -Mathf.FloorToInt(xHalf);
        int xRight = Mathf.CeilToInt(xHalf);

        for (int i = yDown; i < yUp; i++)
        {
            for(int j = xLeft; j < xRight; j++)
            {
                var position = new Vector3(j * tileSize.x, 0, i * tileSize.y);
                var tileInstance = Instantiate(tile, position, Quaternion.identity, transform);
            }
        }
    }
}
#endif