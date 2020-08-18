using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateRotationFrom : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private bool isX = false, isY = false, isZ = false;

    void Update()
    {
        var targetRotation = target.rotation.eulerAngles;
        var rotation = transform.rotation.eulerAngles;
        if (isX)
        {
            rotation.x = targetRotation.x;
        }
        if (isY)
        {
            rotation.y = targetRotation.y;
        }
        if (isZ)
        {
            rotation.z = targetRotation.z;
        }
        transform.rotation = Quaternion.Euler(rotation);
    }
}
