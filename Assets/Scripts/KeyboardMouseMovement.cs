﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMouseMovement : MonoBehaviour
{
    private CharacterMovement movement;

    private GameObject cam;

    private CharacterController controller;

    private bool isCrouched;

    // Start is called before the first frame update
    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
        controller = GetComponent<CharacterController>();
        Transform trans = GetComponent<Transform>().Find("StandardCamera");
        cam = trans.gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = transform.TransformDirection(direction);
        movement.Movement(direction);

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!isCrouched)
            {
                cam.transform.Translate(0, -0.5f, 0);
                controller.height /= 2;
            }
            else if (isCrouched)
            {
                cam.transform.Translate(0, 0.5f, 0);
                controller.height *= 2;
            }
            isCrouched = !isCrouched;
        }
    }
}
