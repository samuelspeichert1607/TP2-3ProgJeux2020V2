using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private DetectCollisions visionCollider;

    private DetectCollisions hearingCollider;

    public GameObject detectedPlayer;

    public float timeUntilGiveUp;//in seconds

    public float TimeUntilChaseStops;//in seconds

    // Start is called before the first frame update
    void Start()
    {
        visionCollider = GetComponentInChildren<MeshCollider>().GetComponent<DetectCollisions>();
        hearingCollider = GetComponentInChildren<SphereCollider>().GetComponent<DetectCollisions>();
        detectedPlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        List<Collider> visionColliders = visionCollider.getTriggers();
        for (int i = 0; i < visionColliders.Count; i++)
        {
            if (visionColliders[i] is CharacterController)
            {
                detectedPlayer = visionColliders[i].gameObject;
                timeUntilGiveUp = TimeUntilChaseStops;
            }
        }
        List<Collider> hearingColliders = hearingCollider.getTriggers();
        for (int i = 0; i < hearingColliders.Count; i++)
        {
            if (hearingColliders[i] is SphereCollider)
            {
                if (hearingColliders[i].GetComponentInParent<CharacterController>() != null)
                {
                    detectedPlayer = hearingColliders[i].GetComponentInParent<CharacterController>().gameObject;
                    timeUntilGiveUp = TimeUntilChaseStops;
                }
            }
        }
        if (detectedPlayer != null)
        {
            if (timeUntilGiveUp <= 0)
            {
                detectedPlayer = null;
            }
            timeUntilGiveUp -= Time.deltaTime;
        }

    }
}
