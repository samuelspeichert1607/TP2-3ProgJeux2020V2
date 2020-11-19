using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStuff : MonoBehaviour
{
    private GameObject[] vrRigs;

    public void Awake()
    {
        vrRigs = GameObject.FindGameObjectsWithTag("Pausable");

        
    }

    public void Pause()
    {
        foreach (GameObject vrRig in vrRigs)
        {
            vrRig.SetActive(false);
        }
    }


    public void OnDestroy()
    {
        foreach (GameObject vrRig in vrRigs)
        {
            vrRig.SetActive(true);
        }
    }
}
