using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopBagDeposed : MonoBehaviour
{
    private GameObject sceneManager;
    private UpdateUI updateUI;
    private bool done;

    private void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");

        if (sceneManager.GetComponent<SceneControl>().IsVRActivated)
        {
            updateUI = GameObject.Find("UICanvasVR").GetComponent<UpdateUI>();
        }
        else
        {
            updateUI = GameObject.Find("UICanvas").GetComponent<UpdateUI>();
        }

        done = false;
    }

    private void Update()
    {
        if (isActiveAndEnabled && !done)
        {
            updateUI.PoopBagDeposed += 1;
            done = true;
        }
    }
}
