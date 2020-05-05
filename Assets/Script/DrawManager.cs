﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public static DrawManager Instance { get; set; } // static singleton
    public bool Top_Obj_ActiveFound = false;
    public bool Floor_Obj_ActiveFound = false;
    public bool ActiveDraw = false;
    public float RealSizeOnWorld = 50.0f; // cm
    public float ScaleSizeInAppCm = 0.0f; // cm
    public float ScaleSizeInAppM = 0.0f; // m
    public float SizeTracking = 50.0f;
    public DefaultTrackableEventHandler RootTop_Obj;
    public DefaultTrackableEventHandler Base_Obj;
    public GameObject Top_Obj;
    public GameObject Floor_Obj;
    public GameObject prefab;
    [SerializeField]
    [Header("----Variable----")]
    // ตั้งเอง
    public float Rpmf = 10.0f; // Rpm f(ความมถี่)ความเร็วในการหมนุของเครน รอบ/นาที
    public float DistanceRoot = 2.5f; // ระยะห่างจากรอกถึงจุดหมนุ r
    public float Garvity = 9.81f;
    public float AnglePoint = 1.0f;
    public float RangeHeightPoint = 100.0f;
    // โค้ดหา
    public float Height = 0.0f; // คำนวน จากพื้นถึง กรอก
    public float TempHeight = 0.0f; // Temp height เก็บไว้
    public float I_rpm = 0.0f;  //  I_rpm = (2 * Mathf.PI * Rpmf) / 60;
    public float V_Speed = 0.0f; // V_Speed = I_rpm * DistanceRoot
    public float T_time = 0.0f; // Mathf.Sqrt((2 * TempHeight) / Garvity);
    public List<Vector3> Vector_Radius = new List<Vector3>(); //
    public List<GameObject> ListObjPoint = new List<GameObject>();
    //float times = 0.0f;
    float y = 0.0f;
    float x = 0.0f;
    float z = 0.0f;
    float radius = 0;
    // Start is called before the first frame update
    void Start()
    {
        ScaleSizeInAppCm = 1 / RealSizeOnWorld;
        ScaleSizeInAppM = ScaleSizeInAppCm * 100;
        RootTop_Obj = GameObject.Find("Rok").GetComponent<DefaultTrackableEventHandler>();
        Base_Obj = GameObject.Find("Base").GetComponent<DefaultTrackableEventHandler>();
        Top_Obj = GameObject.Find("Top");
        Floor_Obj = GameObject.Find("Floor");
        
        
    }


    // Update is called once per frame
    void Update()
    {
        Top_Obj_ActiveFound = Top_Obj.GetComponent<MeshRenderer>().enabled;
        Floor_Obj_ActiveFound = Floor_Obj.GetComponent<MeshRenderer>().enabled;
    }

    public void StartGenerated()
    {
        if (ActiveDraw || !Floor_Obj_ActiveFound || !Top_Obj_ActiveFound)
            return;

        ActiveDraw = true;
        Top_Obj.transform.localPosition = new Vector3(0, 0, (DistanceRoot* ScaleSizeInAppM));
        Vector3 pos = Floor_Obj.transform.position;
        pos.z = (DistanceRoot*ScaleSizeInAppM);
        Floor_Obj.transform.position = pos;
        Height = Vector3.Distance(Top_Obj.transform.position, Floor_Obj.transform.position)*RealSizeOnWorld;
        //TempHeight = Height;

        I_rpm = (2 * Mathf.PI * Rpmf) / 60;
        V_Speed = I_rpm * (DistanceRoot*ScaleSizeInAppM);

        Vector2[] uv = new Vector2[0];
        int[] trianglesArr = new int[0];

        Mesh mesh = new Mesh();

        GeneratedPoint();

        mesh.vertices = Vector_Radius.ToArray();
        mesh.uv = uv;
        mesh.triangles = trianglesArr;

        GameObject gameObj = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        gameObj.transform.localScale = new Vector3(1, 1, 1);

        gameObj.GetComponent<MeshFilter>().mesh = mesh;
    }

    public void DrawDebug(Vector3 point)
    {
        // Debug.DrawLine(point, point, Color.red);
        //Debug.DrawLine(point, point + new Vector3(0,0.5f, 0), Color.reds);
        GameObject item = Instantiate(prefab, point, new Quaternion());
        item.name = "[" + point.x + "]" + "[" + point.y + "]" + "[" + point.z + "]";
        ListObjPoint.Add(item);
    }

    public void GeneratedPoint()
    {
        for (TempHeight = 0; TempHeight < Top_Obj.transform.position.y + 1; TempHeight += (RangeHeightPoint*ScaleSizeInAppCm))
        {

            T_time = Mathf.Sqrt((2 * TempHeight) / Garvity);

            x = V_Speed * T_time;
            y = TempHeight;

            for (float angle = 0; angle < 360; angle += AnglePoint)
            {
                if (angle != 0)
                {
                    z = radius * Mathf.Sin(angle);
                    x = radius * Mathf.Cos(angle);

                }
                else
                {
                    radius = Mathf.Abs(x);
                    continue;
                }
                Vector3 vector = new Vector3(x, y, z);
                vector = (Top_Obj.transform.position) - vector;
                Vector_Radius.Add(vector);
                DrawDebug(vector);
                if (TempHeight == 0)
                    angle = 360;

            }
        }

    }

    public void ClearAll()
    {
        foreach (GameObject item in ListObjPoint)
        {
            Destroy(item);
        }

        ListObjPoint.Clear();
    }

}
