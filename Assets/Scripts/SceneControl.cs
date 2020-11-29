using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SceneControl : MonoBehaviour
{
    private GlobalControl globalControl;
    
    public bool IsVRActivated { get; set; }

    private DifficultyMode difficultyMode;

    [SerializeField]
    private GameObject UICanvas;

    [SerializeField]
    private GameObject UICanvasVR;
    
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
                player.GetComponent<CharacterController>().radius = 0.0f;
                player.GetComponent<CameraControl>().enabled = false;
                GameObject mainCamera = GameObject.Find("StandardCamera");
                mainCamera.SetActive(false);
                UICanvasVR.SetActive(true);
            }
            else
            {
                player.GetComponent<ContinuousMovement>().enabled = false;
                player.GetComponent<CharacterController>().radius = 0.5f;
                GameObject vrCamera = GameObject.Find("CameraOffset");
                vrCamera.SetActive(false);
                UICanvas.SetActive(true);
            }
        }


    }
}
