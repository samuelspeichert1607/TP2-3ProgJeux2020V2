using UnityEngine;
using Photon.Pun;

public class LobbyControl : MonoBehaviourPunCallbacks
{
    private GlobalControl globalControl;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject canvasVR;

    [SerializeField]
    private GameObject eventSystem;

    [SerializeField]
    private GameObject eventSystemVR;

    public bool IsVRActivated;
    
    private void Awake()
    {
        GameObject globalGameController = GameObject.Find("GlobalGameController");

        if (globalGameController != null)
        {
            globalControl = globalGameController.GetComponent<GlobalControl>();
            IsVRActivated = globalControl.IsVRActivated;
        }

        if (IsVRActivated)
        {
            eventSystemVR.SetActive(true);
            canvasVR.SetActive(true);
        }
        else
        {
            eventSystem.SetActive(true);
            canvas.SetActive(true);
        }
    }
}
