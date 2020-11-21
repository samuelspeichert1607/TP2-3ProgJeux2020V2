using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SceneControl : MonoBehaviour
{
    private GlobalControl globalControl;
    
    public bool IsVRActivated;

    // Start is called before the first frame update
    private void Awake()
    {
        GameObject globalGameController = GameObject.Find("GlobalGameController");

        if (globalGameController != null)
        {
            globalControl = globalGameController.GetComponent<GlobalControl>();
            IsVRActivated = globalControl.IsVRActivated;
        }

        GameObject player = GameObject.Find("VR Rig");

        if(player != null && globalControl != null)
        {
            if (IsVRActivated)
            {
                player.GetComponent<KeyboardMouseMovement>().enabled = false;
                player.GetComponent<CameraControl>().enabled = false;
                GameObject mainCamera = GameObject.Find("StandardCamera");
                mainCamera.SetActive(false);
            }
            else
            {
                player.GetComponent<ContinuousMovement>().enabled = false;
                GameObject vrCamera = GameObject.Find("CameraOffset");
                vrCamera.SetActive(false);
            }
        }


    }
}
