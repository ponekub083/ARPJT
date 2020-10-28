﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDrawManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject rootObject;

    public Vector3 RootLocation;

    public List<Vector3> positionTop = new List<Vector3>();
    public List<Vector3> positionBottom = new List<Vector3>();

    public float f_min = 0;
    public float f_max = 0;
    public float radius = 0;
    public float H_big = 0;
    public float h_small = 0;

    public float Hh = 0;
    public float g = 9.81f;

    public float W_max = 0;
    public float U_max = 0;
    public float T_max = 0;
    public float D_max = 0;

    public float W_min = 0;
    public float U_min = 0;
    public float T_min = 0;
    public float D_min = 0;


    public float AnglePoint= 18;
    public List<GameObject> ListObjDebug = new List<GameObject>();
    public GameObject prefab;
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uv;
    public Mesh mesh;
    void Start()
    {
        RootLocation = rootObject.transform.localPosition;
        Hh = H_big - h_small;

        W_max = (2 * Mathf.PI * f_max) / 60;
        U_max = W_max * radius;
        T_max = Mathf.Sqrt((2 * Hh )/ g);
        D_max = U_max * T_max;

        W_min = (2 * Mathf.PI * f_min) / 60;
        U_min = W_min * radius;
        T_min = Mathf.Sqrt((2 * Hh)/ g);
        D_min = U_min * T_min;

        GeneratedPoint();
    }

// Update is called once per frame
    void Update()
    {
        
    }

    public void DrawDebug(float angle, Vector3 point)
    {
        // Debug.DrawLine(point, point, Color.red);
        //Debug.DrawLine(point, point + new Vector3(0,0.5f, 0), Color.reds);
        GameObject item = Instantiate(prefab, point, new Quaternion());
        item.transform.GetChild(1).GetComponent<TextMesh>().text = angle.ToString();
        item.name = "[" + angle + "]" + "[" + point.x + "]" + "[" + point.y + "]" + "[" + point.z + "]";
        float size = 0.2f;
        item.transform.localScale = new Vector3(size, size, size);
        ListObjDebug.Add(item);
    }

    public void DrawMesh()
    {
        vertices = mesh.vertices;
        triangles = mesh.triangles;
        uv = mesh.uv;

        int count = 0;

        vertices[0] = positionTop[0];
        vertices[1] = positionTop[1];
        vertices[2] = positionBottom[0];
        vertices[3] = positionBottom[1];
        for (int i = 2; i < vertices.Length; i+=2)
        {

            DrawDebug(i, vertices[i]);
            vertices[i+2] = positionTop[i-1];
            vertices[i+3] = positionBottom[i-1];
            count++;
        }
        //    new Vector3(-1,1,1), //0
        //    new Vector3(1,1,1), //1
        //    new Vector3(-1,-1,1), // 2
        ////    new Vector3(1,-1,1), 
        //vertices[0] = positionTop[0];
        //vertices[1] = positionTop[1];
        //vertices[2] = positionBottom[0];
        //vertices[3] = positionBottom[1];

        //vertices[4] = positionTop[1];
        //vertices[5] = positionTop[2];
        //vertices[6] = positionBottom[1];
        //vertices[7] = positionBottom[2];


        //vertices[8] = positionTop[2];
        //vertices[9] = positionTop[3];
        //vertices[10] = positionBottom[2];
        //vertices[11] = positionBottom[3];


        //vertices[12] = positionTop[3];
        //vertices[13] = positionTop[4];
        //vertices[14] = positionBottom[3];
        //vertices[15] = positionBottom[4];



        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.Optimize();
        mesh.RecalculateNormals();

        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;
    }
    public void GeneratedPoint()
    {
        // button
        for (float angle = 0; angle < 360; angle += AnglePoint)
        {
            Vector3 centerTop = RootLocation;

            float cornerAngle = 2f * Mathf.PI / (float)360 * (angle );
            float x = centerTop.x + (Mathf.Cos(cornerAngle) * radius);
            float z = centerTop.z + (Mathf.Sin(cornerAngle) * radius);

            Vector3 vector = new Vector3(x, RootLocation.y, z);
            //DrawDebug(positionBottom.Count, vector);
            positionBottom.Add(vector);
            

        }

        // top
        for (float angle = 0; angle < 360; angle += AnglePoint)
        {
            Vector3 centerTop = RootLocation;
            float cornerAngle = 2f * Mathf.PI / (float)360 * angle;
            float x = centerTop.x + (Mathf.Cos(cornerAngle) * radius);
            float z = centerTop.z + (Mathf.Sin(cornerAngle) * radius);

            Vector3 vector = new Vector3(x, RootLocation.y + Hh, z);

            positionTop.Add(vector);
            //aDrawDebug(positionTop.Count, vector);
        }

        DrawMesh();
    }
    

}