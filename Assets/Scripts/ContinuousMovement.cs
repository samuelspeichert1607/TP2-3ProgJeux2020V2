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

    private SphereCollider soundShpere;

    // Start is called before the first frame update
    private void Start()
    {
        character = GetComponent<CharacterController>();
        movement = GetComponent<CharacterMovement>();
        rig = GetComponent<XRRig>();
        soundShpere = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        movement.Movement(direction);

        float speed = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2));
        soundShpere.radius = speed * 2;
    }

    private void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}
