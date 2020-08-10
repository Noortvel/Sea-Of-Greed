using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatePositionFrom : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool isX, isY, isZ;
    
    void Update() { 
        var targetPosition = target.position;
        var position = transform.position;
        if (isX)
        {
            position.x = targetPosition.x;
        }
        if (isY)
        {
            position.y = targetPosition.y;
        }
        if (isZ)
        {
            position.z = targetPosition.z;
        }
        transform.position = position;
    }
}
