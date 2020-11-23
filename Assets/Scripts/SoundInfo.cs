using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInfo : MonoBehaviour
{
    public float soundRadious;

    public bool isMakingSound;

    private float timeLeftUntilChange;

    private float timeUntilChange;

    // Start is called before the first frame update
    void Start()
    {
        timeUntilChange = 0.5f;
        timeLeftUntilChange = timeUntilChange;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (soundRadious > 0)
        {
            isMakingSound = true;
            timeLeftUntilChange = Time.deltaTime*2;
        }
        timeLeftUntilChange -= Time.deltaTime;
        if (timeLeftUntilChange < 0)
        {
            if (soundRadious <= 0)
            {
                isMakingSound = false;
            }
            timeLeftUntilChange = Time.deltaTime * 2;
        }
    }
}
