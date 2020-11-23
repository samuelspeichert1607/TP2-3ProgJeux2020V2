using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InteractWithAlertObjectVR : MonoBehaviour
{
    private DetectCollisions collisions;

    public List<AlertHouseOwner> interactableObjects;

    public InputHelpers.Button inputHelpers = InputHelpers.Button.Trigger;
    public XRNode controller1 = XRNode.LeftHand;
    public XRNode controller2 = XRNode.RightHand;

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
            InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controller1), inputHelpers, out bool isPressed);
            if (isPressed)
            {
                interactableObjects[0].AlertOwner();
            }
        }
        if (interactableObjects.Count >= 2)
        {
            //code UI promt here
            InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controller2), inputHelpers, out bool isPressed);
            if (isPressed)
            {
                interactableObjects[1].AlertOwner();
            }
        }
    }
}
