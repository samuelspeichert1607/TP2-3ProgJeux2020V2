using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertHouseOwner : MonoBehaviour
{

    public enum AlertType
    {
        DoorBell,
        Window
    }

    private House houseInfo;

    public GameObject OwnerDestination;

    public AlertType type;

    // Start is called before the first frame update
    void Start()
    {
        houseInfo = transform.root.GetComponent<House>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlertOwner()
    {
        houseInfo.getOwner().getAttention(OwnerDestination.transform.position);
    }
}
