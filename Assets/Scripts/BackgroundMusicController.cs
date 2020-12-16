using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public float transitionDuration;

    private float timeLeftInTransition;

    private PlayerStatus status;

    private AudioSource normalMusic;

    private AudioSource chaseMusic;

    private bool isChaseMusicPlaying;

    private Volume volume;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerStatus>();

        Transform musObj = GetComponent<Transform>().Find("BackgroundMusicNormal");
        normalMusic = musObj.GetComponent<AudioSource>();

        musObj = GetComponent<Transform>().Find("BackgroundMusicChase");
        chaseMusic = musObj.GetComponent<AudioSource>();

        timeLeftInTransition = transitionDuration;
        isChaseMusicPlaying = false;
        volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        float maxVolume = volume.volume;
        if (timeLeftInTransition < transitionDuration)
        {
            timeLeftInTransition += Time.deltaTime;
            float volume = maxVolume * timeLeftInTransition / transitionDuration;
            if (volume > maxVolume)
            {
                volume = maxVolume;
            }

            if (isChaseMusicPlaying)
            {
                normalMusic.volume = maxVolume - volume;
                chaseMusic.volume = volume;
            }
            else
            {
                normalMusic.volume = volume;
                chaseMusic.volume = maxVolume - volume;
            }
        }
        else
        {
            if (isChaseMusicPlaying)
            {
                normalMusic.volume = 0;
                chaseMusic.volume = maxVolume;
            }
            else
            {
                normalMusic.volume = maxVolume;
                chaseMusic.volume = 0;
            }
        }


        if (status.isChased != isChaseMusicPlaying)
        {
            timeLeftInTransition = 0;
            isChaseMusicPlaying = status.isChased;
        }
    }
}
