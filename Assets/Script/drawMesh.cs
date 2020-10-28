using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class drawMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;

        foreach (Vector3 item in mesh.vertices)
        {
            Debug.Log(item);
        }
       

        //// vertices
        //Vector3[] vertices = new Vector3[]
        //{
        //    // font face
            //    new Vector3(-1,1,1), //0
            //    new Vector3(1,1,1), //1
            //    new Vector3(-1,-1,1), // 2
            //    new Vector3(1,-1,1),
        //};

        //// triangles
        //int[] triangles = new int[]
        //{
        //    // font face
        //    0,2,3,
        //    3,1,0
        //};
        //// UV
        //Vector2[] uv = new Vector2[]
        //{
        //    // font face
        //    new Vector2(0,1), //0
        //    new Vector2(0,0), //1
        //    new Vector2(1,1), // 2
        //    new Vector2(1,0),
        //};

        //mesh.Clear();
        //mesh.vertices = vertices;
        //mesh.triangles = triangles;
        //mesh.uv = uv;
        //mesh.Optimize();
        //mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
