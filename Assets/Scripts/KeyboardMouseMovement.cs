using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMouseMovement : MonoBehaviour
{
    private CharacterMovement movement;

    // Start is called before the first frame update
    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = transform.TransformDirection(direction);
        movement.Movement(direction);
    }
}
