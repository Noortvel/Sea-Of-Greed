using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyArrow : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb = null;
    [SerializeField]
    private Transform target = null;

    // Update is called once per frame
    void Start()
    {
        transform.parent = null;
        
    }
    void Update()
    {
        var pos = target.position;
        pos.y = transform.position.y;
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }
}
