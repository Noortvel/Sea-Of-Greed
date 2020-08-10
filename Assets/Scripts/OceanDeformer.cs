using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OceanDeformer : MonoBehaviour {

    [SerializeField]
    private OceanMaster oceanMaster = null;
    private MeshFilter mesh;

    [SerializeField]
    private GameObject arrowNormalPrefab = null;
    [SerializeField]
    private GameObject arrowTangentPrefab = null;
    [SerializeField]
    private GameObject arrowBitangentPrefab = null;

    private GameObject[] arrowsNormal = null;
    private GameObject[] arrowsTangent = null;
    private GameObject[] arrowsBitangent = null;
    void Awake () {
        mesh = GetComponent<MeshFilter>();
        arrowsNormal = new GameObject[mesh.mesh.vertexCount];
        arrowsTangent = new GameObject[mesh.mesh.vertexCount];
        arrowsBitangent = new GameObject[mesh.mesh.vertexCount];
        for (int i = 0; i < arrowsNormal.Length; i++)
        {
            arrowsNormal[i] = Instantiate(arrowNormalPrefab);
            arrowsTangent[i] = Instantiate(arrowTangentPrefab);
            arrowsBitangent[i] = Instantiate(arrowBitangentPrefab);
        }
    }
    
    [SerializeField]
    private bool isCPUDeforming = false;
    [SerializeField]
    private bool isFlipBitangent = false;
    [SerializeField, Header("Debug Info")]
    private float _normalLen;
    [SerializeField]
    private float _tangentLen, _bitangentLen;
    void Update () {
        if (isCPUDeforming)
        {
            Vector3[] vert = mesh.mesh.vertices;
            Vector3[] normals = mesh.mesh.normals;
            Vector4[] tangents = mesh.mesh.tangents;
            for (int i = 0; i < mesh.mesh.vertexCount; i++)
            {
                float x = vert[i].x;
                float z = vert[i].z;
                vert[i].y = oceanMaster.GetWaveHeight2(x, z);
                var tangent = new Vector3(1, oceanMaster.DDX_GetWaveHeight2(x, z), 0);
                var bitangent = new Vector3(0, oceanMaster.DDZ_GetWaveHeight2(x, z), 1);
                var normal = Vector3.Cross(bitangent, tangent);
                tangent.Normalize();
                bitangent.Normalize();
                normal.Normalize();
                

                //normal.Normalize();
               

                _normalLen = normal.magnitude;
                _tangentLen = tangent.magnitude;
                _bitangentLen = bitangent.magnitude;

                int flipScale = isFlipBitangent ? -1 : 1;

                normals[i] = normal;
                tangents[i] = new Vector4(tangent.x, tangent.y, tangent.z, flipScale);

                arrowsNormal[i].transform.position = vert[i] + transform.position;
                arrowsNormal[i].transform.up = normal;

                arrowsTangent[i].transform.position = vert[i] + transform.position;
                arrowsTangent[i].transform.up = tangent;

                arrowsBitangent[i].transform.position = vert[i] + transform.position;
                arrowsBitangent[i].transform.up = bitangent;
            }
            mesh.mesh.vertices = vert;
            mesh.mesh.normals = normals;
            mesh.mesh.tangents = tangents;
        }
    }
}
