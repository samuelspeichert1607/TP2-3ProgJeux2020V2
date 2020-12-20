using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private PhotonView photonView;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    private bool isVR;

    // Start is called before the first frame update
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        XRRig rig = FindObjectOfType<XRRig>();

        GameObject globalControl = GameObject.Find("SceneManager");

        if (globalControl != null)
        {
            isVR = globalControl.GetComponent<SceneControl>().IsVRActivated;
        }
        
        if (isVR)
        {
            headRig = rig.transform.Find("CameraOffset/VR Camera");
            leftHandRig = rig.transform.Find("CameraOffset/Left Hand");
            rightHandRig = rig.transform.Find("CameraOffset/Right Hand");
        }
        else
        {
            headRig = rig.transform.Find("StandardCamera/Main Camera");
            leftHandRig = rig.transform.Find("StandardCamera/Left Hand Model");
            rightHandRig = rig.transform.Find("StandardCamera/Right Hand Model");
        }

        if (photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (photonView.IsMine)
        {
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }
    }

    private void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        //Trigger
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        //Grip
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
