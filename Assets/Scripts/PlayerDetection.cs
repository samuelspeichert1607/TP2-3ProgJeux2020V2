using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private DetectCollisions visionCollider;

    private DetectCollisions hearingCollider;

    private Transform visionPoint;

    private Transform centerOfMass;

    public GameObject detectedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        visionCollider = GetComponentInChildren<MeshCollider>().GetComponent<DetectCollisions>();
        hearingCollider = GetComponentInChildren<SphereCollider>().GetComponent<DetectCollisions>();
        visionPoint = GetComponent<Transform>().Find("VisionPoint");
        centerOfMass = GetComponent<Transform>().Find("CenterOfMass");
        detectedPlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        detectedPlayer = null;
        List<Collider> visionColliders = visionCollider.getTriggers();
        for (int i = 0; i < visionColliders.Count; i++)
        {
            if (visionColliders[i] is CharacterController)
            {
                GameObject player = visionColliders[i].gameObject;
                Transform camera = player.GetComponent<Transform>().Find("StandardCamera");
                Vector3 direction = camera.position - visionPoint.position;
                RaycastHit[] hits;
                hits = Physics.RaycastAll(visionPoint.position, direction, Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2)));
                bool canSeeObject = true;
                for (int j = 0; j < hits.Length; j++)
                {
                    if (hits[j].collider.GetComponent<ObjectAIBehavior>() != null)
                    {
                        ObjectAIBehavior behavior = hits[j].collider.GetComponent<ObjectAIBehavior>();
                        if (!behavior.canLightPassThrough)
                        {
                            canSeeObject = false;
                            break;
                        }
                    }
                }

                if (canSeeObject)
                {
                    detectedPlayer = player;
                }
                break;
            }
        }
        List<Collider> hearingColliders = hearingCollider.getTriggers();
        for (int i = 0; i < hearingColliders.Count; i++)
        {
            if (hearingColliders[i] is SphereCollider)
            {
                if (hearingColliders[i].GetComponentInParent<CharacterController>() != null)
                {
                    GameObject player = hearingColliders[i].GetComponentInParent<CharacterController>().gameObject;
                    Transform sound = player.GetComponent<Transform>().Find("SoundMade");
                    Vector3 direction = sound.position - centerOfMass.position;
                    RaycastHit[] hits;
                    hits = Physics.RaycastAll(centerOfMass.position, direction, Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2)));
                    bool canHearObject = true;
                    for (int j = 0; j < hits.Length; j++)
                    {
                        if (hits[j].collider.GetComponent<ObjectAIBehavior>() != null)
                        {
                            ObjectAIBehavior behavior = hits[j].collider.GetComponent<ObjectAIBehavior>();
                            if (!behavior.canSoundPassThrough)
                            {
                                canHearObject = false;
                                break;
                            }
                        }
                    }

                    if (canHearObject)
                    {
                        detectedPlayer = player;
                    }
                    break;
                }
            }
        }

    }
}
