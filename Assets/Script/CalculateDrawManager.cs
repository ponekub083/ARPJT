using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDrawManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject rootObject;

     Vector3 RootLocation;

    public float SizeTracking = 5.0f;
    public float f_min = 0;
    public float f_max = 0;
    public float radius = 0;
    public float H_big = 0;
    public float h_small = 0;

     float Hh = 0;
     float g = 9.81f;

     float W_max = 0;
     float U_max = 0;
     float T_max = 0;
     float D_max = 0;

     float W_min = 0;
     float U_min = 0;
     float T_min = 0;
     float D_min = 0;


     float AnglePoint= 18;
     List<GameObject> ListObjDebug = new List<GameObject>();
     GameObject prefab;
    public CyliderGenerate generater_normal;
    public CyliderGenerate generater_min;
    public CyliderGenerate generater_max;
    void Start()
    {
        //generate();
    }

    public void Calculate()
    {
        RootLocation = rootObject.transform.localPosition;

        float centimeter = 1 / SizeTracking;
        Hh = (H_big - h_small)*centimeter;
        radius = radius * centimeter;

        W_max = (2 * Mathf.PI * f_max) / 60;
        U_max = W_max * radius;
        T_max = Mathf.Sqrt((2 * Hh) / g);
        D_max = U_max * T_max;

        W_min = (2 * Mathf.PI * f_min) / 60;
        U_min = W_min * radius;
        T_min = Mathf.Sqrt((2 * Hh) / g);
        D_min = U_min * T_min;


        float radius_min = Mathf.Abs(D_min);
        float radius_max = Mathf.Abs(D_max);


        generater_normal.radius = radius ;
        generater_normal.iter = 20;
        generater_normal.leng = (Hh + 2.0f);

        generater_min.radius = radius_min;
        generater_min.iter = 20;
        generater_min.leng = (Hh + 1.0f);

        generater_max.radius = radius_max;
        generater_max.iter = 20;
        generater_max.leng = (Hh);
    }

    public void generate()
    {
        Calculate();
        generater_normal.GenerateStart();
        generater_min.GenerateStart();
        generater_max.GenerateStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
