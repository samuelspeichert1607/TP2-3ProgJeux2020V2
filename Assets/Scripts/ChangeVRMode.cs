using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVRMode : MonoBehaviour
{
    Toggle toggle;
    GlobalControl globalControl;

    // Start is called before the first frame update
    private void Start()
    {
        globalControl = GameObject.Find("GlobalGameController").GetComponent<GlobalControl>();
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
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
