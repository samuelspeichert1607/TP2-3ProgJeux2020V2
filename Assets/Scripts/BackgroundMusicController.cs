using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public float transitionDuration;

    public float timeLeftInTransition;

    private PlayerStatus status;

    private AudioSource normalMusic;

    private AudioSource chaseMusic;

    private bool isChaseMusicPlaying;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeftInTransition < transitionDuration)
        {
            timeLeftInTransition += Time.deltaTime;
            float volume = 1 * timeLeftInTransition / transitionDuration;
            if (volume > 1)
            {
                volume = 1;
            }

            if (isChaseMusicPlaying)
            {
                normalMusic.volume = 1 - volume;
                chaseMusic.volume = volume;
            }
            else
            {
                normalMusic.volume = volume;
                chaseMusic.volume = 1 - volume;
            }
        }
        else
        {
            if (isChaseMusicPlaying)
            {
                normalMusic.volume = 0;
                chaseMusic.volume = 1;
            }
            else
            {
                normalMusic.volume = 1;
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
