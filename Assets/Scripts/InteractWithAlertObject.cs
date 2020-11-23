using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InteractWithAlertObject : MonoBehaviour
{
    private DetectCollisions collisions;

    public List<AlertHouseOwner> interactableObjects;

    // Start is called before the first frame update
    void Start()
    {
        collisions = GetComponent<DetectCollisions>();
        List<AlertHouseOwner> interactableObjects = new List<AlertHouseOwner>();
    }

    // Update is called once per frame
    void Update()
    {
        interactableObjects.Clear();
        List<Collider> colliders = collisions.getTriggers();
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].GetComponent<AlertHouseOwner>() != null)
            {
                interactableObjects.Add(colliders[i].GetComponent<AlertHouseOwner>());
            }
        }

        if (interactableObjects.Count >= 1)
        {
            //code UI promt here
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                interactableObjects[0].AlertOwner();
            }
        }
        if (interactableObjects.Count >= 2)
        {
            //code UI promt here
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                interactableObjects[1].AlertOwner();
            }
        }
    }
}
