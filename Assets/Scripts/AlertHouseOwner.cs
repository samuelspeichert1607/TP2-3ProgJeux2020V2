using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertHouseOwner : MonoBehaviour
{
    private GameObject sceneManager;
    private UpdateUI updateUI;

    public enum AlertType
    {
        DoorBell,
        Window
    }

    private House houseInfo;

    public GameObject OwnerDestination;

    public AlertType type;

    private AudioSource ring;

    // Start is called before the first frame update
    private void Start()
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
        
        houseInfo = transform.root.GetComponent<House>();
        ring = GetComponent<AudioSource>();
    }

    public void AlertOwner()
    {
        if(type == AlertType.Window)
        {
            updateUI.EggsThrown += 1;
        }
        else if (type == AlertType.DoorBell)
        {
            updateUI.DingDongDitchesDone += 1;
            ring.Play();
        }

        houseInfo.getOwner().getAttention(OwnerDestination.transform.position);
    }
}
