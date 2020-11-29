using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ThrowEgg : MonoBehaviour
{
    public GameObject eggPrefab;

    public GameObject startLocation;

    public GameObject directionAncor;

    public float throwForce;

    public InputHelpers.Button inputHelpers = InputHelpers.Button.Grip;
    public XRNode controller = XRNode.RightHand;

    private void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controller), inputHelpers, out bool isPressed);
        if (Input.GetKeyDown(KeyCode.F) || isPressed)
        {
            Quaternion rotation = Quaternion.LookRotation(directionAncor.transform.position - startLocation.transform.position);

            GameObject newEgg = Instantiate(eggPrefab, startLocation.transform.position, rotation);
            Egg newEggValues = newEgg.GetComponent<Egg>();
            newEggValues.initialise(throwForce);
        }
    }
}
