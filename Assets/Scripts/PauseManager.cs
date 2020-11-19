using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private GameObject[] vrRigs;

    [SerializeField]
    private GameObject pauseCanvas;

    public void Awake()
    {
        vrRigs = GameObject.FindGameObjectsWithTag("Pausable");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pauseCanvas.activeSelf)
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
        foreach (GameObject vrRig in vrRigs)
        {
            vrRig.SetActive(false);
        }

        pauseCanvas.SetActive(true);
    }

    public void UnpauseStuff()
    {
        pauseCanvas.SetActive(false);

        foreach (GameObject vrRig in vrRigs)
        {
            vrRig.SetActive(true);
        }

    }
}
