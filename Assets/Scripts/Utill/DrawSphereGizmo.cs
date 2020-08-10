#if UNITY_EDITOR
using UnityEngine;

public class DrawSphereGizmo : MonoBehaviour
{
    [SerializeField]
    private float radius = 0.5f;
    //[SerializeField]
    //private Color color = Color.green;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
#endif
