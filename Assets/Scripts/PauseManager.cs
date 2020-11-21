using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PauseManager : MonoBehaviour
{
    private GameObject[] pausableObjects;

    [SerializeField]
    private GameObject pauseCanvas;

    [SerializeField]
    private GameObject pauseCanvasVR;

    [SerializeField]
    private GameObject vrRigMenu;

    private GameObject usedPauseCanvas;

    private GameObject sceneManager;

    [SerializeField]
    private XRNode inputSource;

    public void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");

        if (sceneManager.GetComponent<SceneControl>().IsVRActivated)
        {
            usedPauseCanvas = pauseCanvasVR;
        }
        else
        {
            usedPauseCanvas = pauseCanvas;
        }

        pausableObjects = GameObject.FindGameObjectsWithTag("Pausable");
    }

    public void Update()
    {
        /*InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonClicked);*/
        if (Input.GetKeyDown(KeyCode.P))// || menuButtonClicked)
        {
            if (!usedPauseCanvas.activeSelf)
            {
                PauseStuff();
            }
            else
            {
                UnpauseStuff();
            }
        }
    }

    public void PauseStuff()
    {
        foreach (GameObject pausableObject in pausableObjects)
        {
            pausableObject.SetActive(false);
        }

        if(sceneManager.GetComponent<SceneControl>().IsVRActivated) { vrRigMenu.SetActive(true); }
        
        usedPauseCanvas.SetActive(true);
    }

    public void UnpauseStuff()
    {
        if (sceneManager.GetComponent<SceneControl>().IsVRActivated) { vrRigMenu.SetActive(false); }

        usedPauseCanvas.SetActive(false);
        
        foreach (GameObject pausableObject in pausableObjects)
        {
            pausableObject.SetActive(true);
        }

    }
}
