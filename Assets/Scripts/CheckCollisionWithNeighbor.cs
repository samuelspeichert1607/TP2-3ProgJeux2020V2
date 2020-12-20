using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionWithNeighbor : MonoBehaviour
{
    private GameObject sceneManager;
    private GameIssue gameIssue;

    private void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
       
        if (sceneManager.GetComponent<SceneControl>().IsVRActivated)
        {
            gameIssue = GameObject.Find("UICanvasVR").GetComponent<GameIssue>();
        }
        else
        {
            gameIssue = GameObject.Find("UICanvas").GetComponent<GameIssue>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Neighbor"))
        {
            Debug.Log("TOUCHÉ");
            gameIssue.GameLost = true;
        }
    }
}
