using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHatPosition : MonoBehaviour
{
    private KeyboardMouseMovement keyboardMouseMovement;

    public Vector3 positionCrouched;

    public Vector3 positionUncrouched;

    private void Start()
    {
        keyboardMouseMovement = GetComponentInParent<KeyboardMouseMovement>();
    }
    
    private void Update()
    {
        if (keyboardMouseMovement.IsCrouched())
        {
            gameObject.transform.localPosition = positionCrouched;
        }
        else
        {
            gameObject.transform.localPosition = positionUncrouched;
        }
    }
}
