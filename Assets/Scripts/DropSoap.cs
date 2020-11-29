using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DropSoap : MonoBehaviour
{
    public GameObject soapPrefab;

    public GameObject soapDropLocation;

    public InputHelpers.Button inputHelpers = InputHelpers.Button.PrimaryButton;
    public XRNode controller = XRNode.RightHand;
    
    private void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controller), inputHelpers, out bool isPressed);
        if (Input.GetKeyDown(KeyCode.Q) || isPressed)
        {
            Instantiate(soapPrefab, soapDropLocation.transform.position, Quaternion.identity);
        }
    }
}
