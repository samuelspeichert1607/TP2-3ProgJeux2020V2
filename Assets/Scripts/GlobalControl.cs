using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    
    public bool IsVRActivated { get; set; }

    public DifficultyMode difficultyMode;

    [Range(0, 1)]
    public float MusicVolume;

    [Range(0, 1)]
    public float SoundEffectVolume;


    //Build-an-avatar values
    public List<GameObject> possibleAvatars;

    public GameObject avatarChoisi;

    public Vector3 armSize;

    public Vector3 legSize;

    public Color firstColor;

    public Color secondColor;


    private void Awake()
    {
        avatarChoisi = possibleAvatars[0];
        firstColor = Color.white;
        secondColor = Color.white;
        legSize = new Vector3(1f, 1f, 1f);
        armSize = new Vector3(1f, 1f, 1f);

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            MusicVolume = 1;
            SoundEffectVolume = 1;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    /*public GameObject BuildAvatar()
    {
        GameObject avatar = Instantiate(avatarChoisi, gameObject.transform.position, Quaternion.identity);

        avatar.GetComponentInChildren<SkinnedMeshRenderer>().material.color = firstColor;
        avatar.GetComponentInChildren<MeshRenderer>().material.color = secondColor;
        avatar.GetComponentInChildren<ChangeLegSize>().ChangeSize(legSize);
        avatar.GetComponentInChildren<ChangeArmSize>().ChangeSize(armSize);

        return avatar;
    }*/

    public void Avatar1Choisi()
    {
        avatarChoisi = possibleAvatars[0];
    }

    public void Avatar2Choisi()
    {
        avatarChoisi = possibleAvatars[1];
    }

}
