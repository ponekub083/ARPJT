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
    public InputField f_min;
    public InputField f_max;
    public InputField radius;
    public InputField H_big;
    public InputField h_small;

    public Renderer red;
    public Renderer yellow;
    public Renderer green;

    public CalculateDrawManager Calculate_Manager;
    // Start is called before the first frame update
    void Start()
    {
        AnimManager = GetComponent<Animator>();

        SizeTracking.text = Calculate_Manager.SizeTracking.ToString();
        f_min.text = Calculate_Manager.f_min.ToString();
        f_max.text = Calculate_Manager.f_max.ToString();
        radius.text = Calculate_Manager.radius.ToString();
        H_big.text = Calculate_Manager.H_big.ToString();
        h_small.text = Calculate_Manager.h_small.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        CalculateButton.interactable = red.enabled && yellow.enabled && green.enabled;
    }
    public void OnCalculate()
    {
        Calculate_Manager.generate();
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
        Calculate_Manager.SizeTracking = value;
    }

    public void OnH_big(string str)
    {
        str = H_big.text;
        int value;
        int.TryParse(str, out value);
        Calculate_Manager.H_big = value;
    }

    public void Onh_small(string str)
    {
        str = h_small.text;
        int value;
        int.TryParse(str, out value);
        Calculate_Manager.h_small = value;
    }

    public void Onf_min(string str)
    {
        str = f_min.text;
        int value;
        int.TryParse(str, out value);
        Calculate_Manager.f_min = value;
    }

    public void Onf_max(string str)
    {
        str = f_max.text;
        int value;
        int.TryParse(str, out value);
        Calculate_Manager.f_max = value;
    }

    public void OnRadius(string str)
    {
        str = radius.text;
        int value;
        int.TryParse(str, out value);
        Calculate_Manager.radius = value;
    }

}
