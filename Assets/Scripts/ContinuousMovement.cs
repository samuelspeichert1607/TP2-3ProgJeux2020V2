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

    private SphereCollider soundShpere;

    private SoundInfo soundShpereInfo;

    // Start is called before the first frame update
    private void Start()
    {
        character = GetComponent<CharacterController>();
        movement = GetComponent<CharacterMovement>();
        rig = GetComponent<XRRig>();
        soundShpere = GameObject.Find("SoundMade").GetComponent<SphereCollider>();
        soundShpereInfo = soundShpere.GetComponent<SoundInfo>();
    }
    
    private void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }
   
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

        Vector3 direction = headYaw * new Vector3(inputAxis.x * 4, 0, inputAxis.y * 4);

        device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool primaryButtonClicked);
        if (primaryButtonClicked)
        {
            direction = headYaw * new Vector3(inputAxis.x * 8, 0, inputAxis.y * 8);
        }

        movement.Movement(direction);

        float speed = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2));
        soundShpere.radius = speed * 2;
        soundShpereInfo.soundRadious = speed * 2;
    }

    private void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}
