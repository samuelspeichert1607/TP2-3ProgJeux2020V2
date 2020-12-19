using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootstepHandler : MonoBehaviour
{
    private AudioSource footstepSound;

    private GameObject listener;

    private EnemyAI AI;

    [Range(0, 1)]
    public float wallDampeningPercentage;

    private Volume volume;

    public int nbObj;

    // Start is called before the first frame update
    void Start()
    {
        footstepSound = GetComponent<AudioSource>();
        listener = GameObject.FindObjectOfType<AudioListener>().gameObject;
        AI = GetComponentInParent<EnemyAI>();
        volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AI.speed == 0 || AI.isLookingAround)
        {
            footstepSound.volume = 0;
        }
        else
        {
            Vector3 direction = listener.transform.position - transform.position;
            float distance = Vector3.Distance(transform.position, listener.transform.position);
            if (distance <= footstepSound.maxDistance)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(transform.position, direction, distance);
                nbObj = 0;
                for (int j = 0; j < hits.Length; j++)
                {
                    if (hits[j].collider.GetComponent<ObjectAIBehavior>() != null)
                    {
                        ObjectAIBehavior behavior = hits[j].collider.GetComponent<ObjectAIBehavior>();
                        if (!behavior.canSoundPassThrough)
                        {
                            nbObj++;
                        }
                    }
                }

                footstepSound.volume = volume.volume / ((nbObj * wallDampeningPercentage)+1);
            }
        }
    }
}
