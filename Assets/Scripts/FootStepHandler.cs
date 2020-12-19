using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepHandler : MonoBehaviour
{

    private AudioSource footstepSound;

    private KeyboardMouseMovement kMovment;

    private ContinuousMovement cMovment;

    private Volume volume;

    public float normalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        footstepSound = GetComponent<AudioSource>();

        kMovment = GetComponentInParent<KeyboardMouseMovement>();
        cMovment = GetComponentInParent<ContinuousMovement>();
        volume = GetComponent<Volume>();

    }

    // Update is called once per frame
    void Update()
    {
        float speed = kMovment.speed + cMovment.speed;
        if (speed > 0)
        {
            footstepSound.mute = false;
            footstepSound.pitch = 1 * speed / normalSpeed;
        }
        else
        {
            footstepSound.mute = true;
        }
    }
}
