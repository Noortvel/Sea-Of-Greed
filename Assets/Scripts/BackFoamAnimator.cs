using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFoamAnimator : MonoBehaviour
{
    [SerializeField]
    private BoatController boatController = null;
    [SerializeField]
    private BoatWaterPhysic boatWaterPhysic = null;
    [SerializeField]
    private AnimationCurve sizeIncreaceYZCurve = null;
    [SerializeField]
    private AnimationCurve sizeIncreaceXCurve = null;
    [SerializeField, Range(0,1)]
    private float inWaterFactorLerpParam = 0.5f;

    private float lastInWaterFactor = 0;
    void Update()
    {
        float inWaterFactor = boatWaterPhysic.isSignificantForcePointsIsUnderwater() ? 1 : 0;
        inWaterFactor = Mathf.Lerp(lastInWaterFactor, inWaterFactor, inWaterFactorLerpParam);
        lastInWaterFactor = inWaterFactor;
        float inputFactor = Mathf.Clamp01(boatController.ForceInputs.z);
        float ratio = inputFactor * inWaterFactor *
            boatController.ForwardSpeed / boatController.MaxForwardSpeed;
        
        float yz = sizeIncreaceYZCurve.Evaluate(ratio);
        float x = sizeIncreaceXCurve.Evaluate(ratio); ;
        var scale = new Vector3(x, yz, yz);
        transform.localScale = scale;
    }
}
