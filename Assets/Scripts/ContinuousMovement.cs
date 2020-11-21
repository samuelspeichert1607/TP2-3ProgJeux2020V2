using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    [SerializeField]
    private XRNode inputSource;

    [SerializeField]
    private float additionalHeight = 0.2f;

    private XRRig rig;
    private Vector2 inputAxis;
    private CharacterController character;
    private CharacterMovement movement;
    private InputDevice device;

    // Start is called before the first frame update
    private void Start()
    {
        character = GetComponent<CharacterController>();
        movement = GetComponent<CharacterMovement>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    private void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool primaryButtonClicked);
        if (primaryButtonClicked)
        {
            direction = headYaw * new Vector3(inputAxis.x * 2, 0, inputAxis.y * 2);
        }

        movement.Movement(direction);
    }

    private void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}
