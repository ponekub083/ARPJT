using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facingcamera : MonoBehaviour
{
    public GameObject CameraAR;
    // Start is called before the first frame update
    void Start()
    {
        CameraAR = GameObject.Find("ARCamera");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(CameraAR.transform.position);
    }
}
