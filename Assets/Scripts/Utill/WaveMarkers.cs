using UnityEngine;

public class WaveMarkers : MonoBehaviour
{
    [SerializeField]
    private GameObject markerPrefab = null;
    [SerializeField]
    private OceanMaster ocean = null;
    [SerializeField]
    private Transform[] targetsList = null;

    private Transform[] markers = null;
    void Start()
    {
        markers = new Transform[targetsList.Length];
        for (int i = 0; i < markers.Length; i++)
        {
            markers[i] = Instantiate(markerPrefab).transform;
        }
    }
    [SerializeField]
    private float waveHeight = 0;
    void Update()
    {
        for (int i = 0; i < targetsList.Length; i++)
        {
            var targetPos = targetsList[i].position;
            waveHeight = ocean.GetWaveHeight2(targetPos.x, targetPos.z);
            targetPos.y = waveHeight;
            markers[i].position = targetPos;
        }
    }
}
