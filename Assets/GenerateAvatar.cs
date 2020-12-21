using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAvatar : MonoBehaviour
{
    private GlobalControl globalControl;
    
    private void Awake()
    {
        globalControl = GameObject.Find("GlobalGameController").GetComponent<GlobalControl>();
        GameObject avatar = globalControl.BuildAvatar();
        avatar.transform.position = gameObject.transform.position;
        avatar.transform.parent = gameObject.transform;
    }
}
