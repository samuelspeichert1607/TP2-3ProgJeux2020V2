using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVRMode : MonoBehaviour
{
    Toggle toggle;
    GlobalControl globalControl;
    
    private void Start()
    {
        globalControl = GameObject.Find("GlobalGameController").GetComponent<GlobalControl>();
        toggle = GetComponent<Toggle>();
    }
    
    private void Update()
    {
        if (toggle.isOn)
        {
            globalControl.IsVRActivated = true;
        }
        else
        {
            globalControl.IsVRActivated = false;
        }
    }
}
