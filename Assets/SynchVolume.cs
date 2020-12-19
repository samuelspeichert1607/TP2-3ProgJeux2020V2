using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynchVolume : MonoBehaviour
{
    private Slider music;
    private Slider sfx;
    private GlobalControl globalControl;

    // Start is called before the first frame update
    private void Start()
    {
        globalControl = GameObject.Find("GlobalGameController").GetComponent<GlobalControl>();
        music = GameObject.Find("Musique").GetComponent<Slider>();
        sfx = GameObject.Find("SFX").GetComponent<Slider>();
    }

    // Update is called once per frame
    private void Update()
    {
        globalControl.MusicVolume = music.value;
        globalControl.SoundEffectVolume = sfx.value;
    }
    
}
