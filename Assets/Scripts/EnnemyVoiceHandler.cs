using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyVoiceHandler : MonoBehaviour
{
    public AudioClip[] shouts;

    public AudioClip[] huhs;

    public AudioClip[] whines;

    private EnemyAI AI;

    private AudioSource output;

    private DetectCollisions collisions;

    private bool isOutside;

    private GameObject listener;

    private Volume volume;

    [Range(0, 1)]
    public float wallDampeningPercentage;

    public int nbObj;

    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponentInParent<EnemyAI>();
        output = GetComponent<AudioSource>();
        collisions = GetComponent<DetectCollisions>();
        isOutside = true;
        listener = GameObject.FindObjectOfType<AudioListener>().gameObject;
        volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Collider> hearingColliders = collisions.getTriggers();
        for (int i = 0; i < hearingColliders.Count; i++)
        {
            if (hearingColliders[i].gameObject.name == "door")
            {
                if (AI.currentState == EnemyAI.State.Home)
                {
                    isOutside = false;
                }
                else
                {
                    if (isOutside == false)
                    {
                        output.PlayOneShot(huhs[Random.Range(0, huhs.Length)]);
                    }
                    isOutside = true;
                }
            }
        }


        if (!output.isPlaying)
        {
            if (AI.currentState == EnemyAI.State.Player && isOutside)
            {
                output.PlayOneShot(shouts[Random.Range(0, shouts.Length)]);
            }
            else if (AI.currentState == EnemyAI.State.Target && AI.isLookingAround)
            {
                output.PlayOneShot(huhs[Random.Range(0, huhs.Length)]);
            }
            else
            {
                output.PlayOneShot(whines[Random.Range(0, whines.Length)]);
            }
        }

        Vector3 direction = listener.transform.position - transform.position;
        float distance = Vector3.Distance(transform.position, listener.transform.position);
        if (distance <= output.maxDistance)
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

            output.volume = volume.volume / ((nbObj * wallDampeningPercentage) + 1);
        }
    }
}
