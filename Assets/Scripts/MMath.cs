using System;
using UnityEngine;


public static class MMath
{
    public static Vector3 DeltaRotation(Vector3 from, Vector3 to)
    {
        var res = Vector3.zero;
        res.x = DeltaComponentRotation(from.x, to.x);
        res.y = DeltaComponentRotation(from.y, to.y);
        res.z = DeltaComponentRotation(from.z, to.z);
        return res;
    }
    public static float DeltaComponentRotation(float from, float to)
    {
        float val = to - from;
        float valabs = Mathf.Abs(val);
        if (valabs > 180)
        {
            float sgn = Mathf.Sign(val);
            val = -(360 - valabs) * sgn;
        }
        return val;
    }
    public static Vector3 Euler180Norma(Vector3 rotation)
    {
        rotation.x = AngleTo180Norm(rotation.x);
        rotation.y = AngleTo180Norm(rotation.y);
        rotation.z = AngleTo180Norm(rotation.z);
        return rotation;
    }
    public static float AngleTo180Norm(float val)
    {
        if (Math.Abs(val) > 180)
        {
            return val - 360 * Math.Sign(val);
        }
        return val;
    }
    public static float AngleTo360Norm(float val)
    {
        if (val < 0)
        {
            return val + 360;
        }
        return val;
    }
    public static Vector3 To360Euler(Vector3 rotation)
    {
        rotation.x = AngleTo360Norm(rotation.x);
        rotation.y = AngleTo360Norm(rotation.y);
        rotation.z = AngleTo360Norm(rotation.z);
        return rotation;
    }
}

