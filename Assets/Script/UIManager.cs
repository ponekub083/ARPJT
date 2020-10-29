using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Animator AnimManager;
    public static UIManager Instance { get; set; }
    public Toggle toggleCrane;
    public Toggle toggleArea;
    public Button CalculateButton;
    public Button ClearButton;
    public InputField SizeTracking;
    public InputField DistanceRoot;
    public InputField Speed;
    public InputField Height;
    public InputField Angle;


    public CalculateDrawManager Calculate_Manager;
    // Start is called before the first frame update
    void Start()
    {
        AnimManager = GetComponent<Animator>();

        //SizeTracking.text = Draw_Manager. .ToString();
        //DistanceRoot.text = Draw_Manager.DistanceRoot.ToString();
        //Speed.text = Draw_Manager.Rpmf.ToString();
        //Height.text = Draw_Manager.RangeHeightPoint.ToString();
        //Angle.text = Draw_Manager.AnglePoint.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnToggleSettingPanal()
    {
        if (!AnimManager)
            return;

        bool nowState = AnimManager.GetBool("Setting");
        AnimManager.SetBool("Setting", !nowState);
    }

    public void OnSaveButton()
    {
        OnToggleSettingPanal();
    }

    public void OnSizeTracking(string str)
    {
        str = SizeTracking.text;
        int value;
        int.TryParse(str,out value);
        Draw_Manager.RealSizeOnWorld = value;
    }
}
