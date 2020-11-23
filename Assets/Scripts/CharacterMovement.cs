using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private int speed = 1;
    
    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private LayerMask groundLayer;

    private float fallingSpeed;

    private CharacterController character;

    // Start is called before the first frame update
    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    public void Movement(Vector3 direction)
    {
        character.Move(direction * Time.fixedDeltaTime * speed);

        //gravity
        bool isGrounded = CheckIfGrounded();

        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Tell us if on ground
    /// </summary>
    /// <returns></returns>
    private bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
