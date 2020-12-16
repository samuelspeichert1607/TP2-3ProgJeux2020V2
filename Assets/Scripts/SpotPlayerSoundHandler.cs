using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlayerSoundHandler : MonoBehaviour
{
    private AudioSource sound;

    private EnemyAI AI;

    private EnemyAI.State lastState;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        AI = GetComponentInParent<EnemyAI>();
        lastState = EnemyAI.State.Home;
    }

    // Update is called once per frame
    void Update()
    {
        if (AI.currentState != lastState && AI.currentState == EnemyAI.State.Player)
        {
            sound.Play();
        }
        if (AI.currentState != EnemyAI.State.Target)
        {
            lastState = AI.currentState;
        }
    }
}
