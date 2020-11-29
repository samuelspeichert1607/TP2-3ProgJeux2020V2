using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InteractWithAlertObject : MonoBehaviour
{
    private DetectCollisions collisions;

    public AlertHouseOwner doorBell;

    public PlacePoopBag doorMat;

    // Start is called before the first frame update
    private void Start()
    {
        collisions = GetComponent<DetectCollisions>();
    }

    // Update is called once per frame
    private void Update()
    {
        List<Collider> colliders = collisions.getTriggers();
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].GetComponent<AlertHouseOwner>() != null)
            {
                AlertHouseOwner test = colliders[i].GetComponent<AlertHouseOwner>();
                if (test.type == AlertHouseOwner.AlertType.DoorBell)
                {
                    doorBell = test;
                }
            }
            if (colliders[i].GetComponent<PlacePoopBag>() != null)
            {
                doorMat = colliders[i].GetComponent<PlacePoopBag>();
            }
        }

        if (doorBell != null)
        {
            //code UI promt here
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorBell.AlertOwner();
            }
        }
        if (doorMat != null)
        {
            //code UI promt here
            if (Input.GetKeyDown(KeyCode.Space))
            {
                doorMat.placeBag();
            }
        }
        doorBell = null;
        doorMat = null;
    }
}
