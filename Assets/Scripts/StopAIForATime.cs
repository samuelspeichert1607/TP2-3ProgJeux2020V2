using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAIForATime : MonoBehaviour
{
    public float howLongToStopAIFor;

    private float timeLeft;

    private EnemyAI interactedAI;

    private float interactedAIOriginalSpeed;

    private DetectCollisions collisions;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = howLongToStopAIFor;
        collisions = GetComponentInChildren<DetectCollisions>();
        interactedAI = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactedAI == null)
        {
            List<Collider> colliders = collisions.getTriggers();
            for (int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i].GetComponent<EnemyAI>() != null)
                {
                    interactedAI = colliders[i].GetComponent<EnemyAI>();
                    interactedAIOriginalSpeed = interactedAI.speed;
                    interactedAI.speed = 0;
                }
            }
        }
        else
        {
            if (timeLeft <= 0)
            {
                interactedAI.speed = interactedAIOriginalSpeed;
                gameObject.SetActive(false);
            }
            timeLeft -= Time.deltaTime;
        }
    }
}
