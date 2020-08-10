using System.Collections.Generic;
using UnityEngine;


public class BoatWaterPhysic : MonoBehaviour
{
    [SerializeField]
    private Transform forcePoints = null;
    [SerializeField]
    private float dencity = 1;
    [SerializeField]
    private float waterDrag = 1;
    [SerializeField]
    private float waterAngularDrag = 1;
    [SerializeField]
    private float volumeHeight = 1;
    [SerializeField]
    private float volumeHeightScale = 1;

    private OceanMaster ocean;
    private Rigidbody rb;

    private List<Transform> significantForFrowardMoveForcePoints = new List<Transform>();
    private List<bool> significantPointsIsUnderWater = new List<bool>();
    void Awake()
    {
        var root = gameObject.scene.GetRootGameObjects()[0];
        ocean = root.GetComponentInChildren<OceanMaster>();
        if(ocean == null)
        {
            Debug.LogError("Ocean Master not founded");
        }
        rb = GetComponent<Rigidbody>();
        int pointsCount = forcePoints.childCount;
        for (int i = 0; i < pointsCount; i++)
        {
            var point = forcePoints.GetChild(i);
            if(point.position.z < 0)
            {
                significantForFrowardMoveForcePoints.Add(point);
                significantPointsIsUnderWater.Add(false);
            }
        }
    }
    public IList<bool> CalcSignificantForcePointsIsUnderwater()
    {

        for (int i = 0; i < significantForFrowardMoveForcePoints.Count; i++)
        {
            var position = significantForFrowardMoveForcePoints[i].position;
            float waterLevel = ocean.GetWaveHeight2(position.x, position.z);
            float waterDeltaLevel = waterLevel - position.y;
            significantPointsIsUnderWater[i] = waterDeltaLevel >= 0;
        }
        
        return significantPointsIsUnderWater;
    }
    public bool isSignificantForcePointsIsUnderwater()
    {
        bool ret = true;
        CalcSignificantForcePointsIsUnderwater();
        foreach (bool x in significantPointsIsUnderWater)
        {
            ret = ret && x;
        }
        return ret;
    }


    void FixedUpdate()
    {
        int pointsCount = forcePoints.childCount;
        for (int i = 0; i < pointsCount; i++)
        {
            var position = forcePoints.GetChild(i).position;
            float waterLevel = ocean.GetWaveHeight2(position.x, position.z);
            float waterDeltaLevel = waterLevel - position.y;

            float gydroStaticFactor = waterDeltaLevel > 0 ? waterDeltaLevel : 0;
            gydroStaticFactor = Mathf.Clamp(gydroStaticFactor, 0, volumeHeight) * volumeHeightScale;
            float gydroStaticForce = gydroStaticFactor * dencity * -Physics.gravity.y;

            var force = new Vector3(0, gydroStaticForce * Time.deltaTime, 0);
            rb.AddForceAtPosition(force / pointsCount, position);

            float waterDragForce = gydroStaticFactor * -rb.velocity.y * waterDrag * Time.deltaTime;
            rb.AddForce(new Vector3(0, waterDragForce / 4, 0), ForceMode.VelocityChange);

            var angularForce = -rb.angularVelocity * waterAngularDrag
                * gydroStaticFactor * Time.deltaTime;//Need dt?
            rb.AddTorque(angularForce / pointsCount, ForceMode.VelocityChange);
        }
    }
}
