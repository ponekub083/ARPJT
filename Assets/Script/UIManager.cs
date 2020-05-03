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

    DefaultTrackableEventHandler RootTop_Obj;
    DefaultTrackableEventHandler Base_Obj;

    public DrawManager Draw_Manager;
    // Start is called before the first frame update
    void Start()
    {
        AnimManager = GetComponent<Animator>();
        RootTop_Obj = GameObject.Find("Rok").GetComponent<DefaultTrackableEventHandler>();
        Base_Obj = GameObject.Find("Base").GetComponent<DefaultTrackableEventHandler>();

        SizeTracking.text = Draw_Manager.RealSizeOnWorld.ToString();
        DistanceRoot.text = Draw_Manager.DistanceRoot.ToString();
        Speed.text = Draw_Manager.Rpmf.ToString();
        Height.text = Draw_Manager.RangeHeightPoint.ToString();
        Angle.text = Draw_Manager.AnglePoint.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        toggleCrane.isOn = RootTop_Obj.bActiveFound;
        toggleArea.isOn = Base_Obj.bActiveFound;
        CalculateButton.interactable = toggleCrane.isOn && toggleArea.isOn && !Draw_Manager.ActiveDraw;
        ClearButton.interactable = Draw_Manager.ActiveDraw;
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
        float value;
        float.TryParse(str,out value);
        Draw_Manager.RealSizeOnWorld = value;
    }
    public void OnDistanceRoot(string str)
    {
        float value;
        float.TryParse(str, out value);
        Draw_Manager.DistanceRoot = value;
    }
    public void OnSpeed(string str)
    {
        float value;
        float.TryParse(str, out value);
        Draw_Manager.Rpmf = value;
    }
    public void OnVelocity(string str)
    {
        //Draw_Manager.V = float.Parse(str);
    }
    public void OnRangeHeightPoint(string str)
    {
        float value;
        float.TryParse(str, out value);
        Draw_Manager.RangeHeightPoint = value;
    }
    public void OnAngle(string str)
    {
        float value;
        float.TryParse(str, out value);
        Draw_Manager.AnglePoint = value;
    }
    public void OnCalculateButton()
    {
        Draw_Manager.StartGenerated();
    }

    public void OnClearAllButton()
    {
        Draw_Manager.ClearAll();
    }
}
