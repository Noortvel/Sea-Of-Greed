using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OceanDeformer : MonoBehaviour {

    [SerializeField]
    private OceanMaster oceanMaster = null;
    private MeshFilter mesh;

    [SerializeField]
    private bool isShowBasisArrows = false;
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

        savedVerts = mesh.mesh.vertices;
        for (int i = 0; i < arrowsNormal.Length; i++)
        {
            arrowsNormal[i] = Instantiate(arrowNormalPrefab);
            arrowsTangent[i] = Instantiate(arrowTangentPrefab);
            arrowsBitangent[i] = Instantiate(arrowBitangentPrefab);
            arrowsNormal[i].SetActive(false);
            arrowsTangent[i].SetActive(false);
            arrowsBitangent[i].SetActive(false);
        }
    }
    
    [SerializeField]
    private bool isCPUDeforming = false;
    [SerializeField]
    private bool isFlipBitangent = false;
    [SerializeField]
    private float perlinNoiseDevider = 8f;
    [SerializeField, Header("Debug Info")]
    private float _normalLen;
    [SerializeField]
    private float _tangentLen, _bitangentLen;

    private Vector3[] savedVerts;

    void Update () {
        if (isCPUDeforming)
        {
            Vector3[] vert = mesh.mesh.vertices;
            Vector3[] normals = mesh.mesh.normals;
            Vector4[] tangents = mesh.mesh.tangents;
            for (int i = 0; i < mesh.mesh.vertexCount; i++)
            {
                vert[i] = oceanMaster.GetPosition(savedVerts[i]);

                var tangent = oceanMaster.GetTanget(savedVerts[i]);
                var normal = oceanMaster.GetNormal(savedVerts[i]);
                tangent.Normalize();
                normal.Normalize();

                int flipScale = isFlipBitangent ? -1 : 1;
                normals[i] = normal;
                tangents[i] = new Vector4(tangent.x, tangent.y, tangent.z, flipScale);

                //_normalLen = normal.magnitude;
                //_tangentLen = tangent.magnitude;
                //_bitangentLen = bitangent.magnitude;
                if (isShowBasisArrows)
                {
                    arrowsNormal[i].transform.position = vert[i] + transform.position;
                    arrowsNormal[i].transform.up = normal;

                    arrowsTangent[i].transform.position = vert[i] + transform.position;
                    arrowsTangent[i].transform.up = tangent;

                    //arrowsBitangent[i].transform.position = vert[i] + transform.position;
                    //arrowsBitangent[i].transform.up = bitangent;

                    arrowsNormal[i].SetActive(true);
                    arrowsTangent[i].SetActive(true);
                    //arrowsBitangent[i].SetActive(true);
                }
            }
            mesh.mesh.vertices = vert;
            mesh.mesh.normals = normals;
            mesh.mesh.tangents = tangents;
        }
    }
}
