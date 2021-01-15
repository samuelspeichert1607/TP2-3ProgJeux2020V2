using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private GameObject sceneManager;

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
                Transform head = player.GetComponent<Transform>().Find("Head");
                Vector3 direction = head.position - visionPoint.position;
                float distance = Vector3.Distance(visionPoint.position, head.position);
                RaycastHit[] hits;
                hits = Physics.RaycastAll(visionPoint.position, direction, distance);
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
                    SoundInfo info = hearingColliders[i].GetComponent<SoundInfo>();
                    if(info != null)
                    {
                        if (info.isMakingSound)
                        {
                            GameObject player = hearingColliders[i].GetComponentInParent<CharacterController>().gameObject;
                            Transform sound = player.GetComponent<Transform>().Find("SoundMade");
                            Vector3 direction = sound.position - centerOfMass.position;
                            float distance = Vector3.Distance(centerOfMass.position, sound.position);
                            RaycastHit[] hits;
                            hits = Physics.RaycastAll(centerOfMass.position, direction, distance);
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

    }
}
