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

    public InputHelpers.Button inputHelpers = InputHelpers.Button.MenuButton;
    public XRNode controller = XRNode.LeftHand;

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
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controller), inputHelpers, out bool isPressed);
        if (Input.GetKeyDown(KeyCode.Return) || isPressed)
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
        
        usedPauseCanvas.SetActive(true);
    }

    public void UnpauseStuff()
    {
        usedPauseCanvas.SetActive(false);
        
        foreach (GameObject pausableObject in pausableObjects)
        {
            pausableObject.SetActive(true);
        }

    }
}
