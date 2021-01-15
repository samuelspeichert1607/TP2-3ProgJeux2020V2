using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMouseMovement : MonoBehaviour
{
    private CharacterMovement movement;

    private GameObject cam;

    private GameObject head;

    private CharacterController controller;

    private bool isCrouched;

    private SphereCollider soundShpere;

    private SoundInfo soundShpereInfo;

    public float speed;

    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
        controller = GetComponent<CharacterController>();
        Transform trans = GetComponent<Transform>().Find("StandardCamera");
        cam = trans.gameObject;
        trans = GetComponent<Transform>().Find("Head");
        head = trans.gameObject;
        soundShpere = GameObject.Find("SoundMade").GetComponent<SphereCollider>();
        soundShpereInfo = soundShpere.GetComponent<SoundInfo>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal") * 4, 0, Input.GetAxis("Vertical") * 4);
        if (isCrouched)
        {
            direction = new Vector3(direction.x / 2, 0, direction.z / 2);
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            direction = new Vector3(direction.x * 2, 0, direction.z * 2);
        }
        direction = transform.TransformDirection(direction);
        movement.Movement(direction);

        speed = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2));
        soundShpere.radius = speed;
        soundShpereInfo.soundRadious = speed;

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            if (!isCrouched)
            {
                cam.transform.Translate(0, -1, 0);
                head.transform.Translate(0, -1, 0);
                controller.height = controller.height - 1;
                controller.center = new Vector3(0, 0.5f, 0);
            }
            else if (isCrouched)
            {
                cam.transform.Translate(0, 1, 0);
                head.transform.Translate(0, 1, 0);
                controller.height = controller.height + 1;
                controller.center = new Vector3(0, 1, 0);
            }
            isCrouched = !isCrouched;
        }

        /*animator.SetFloat("speed", speed);
        animator.SetBool("isCrouched", isCrouched);*/
    }

    public bool IsCrouched()
    {
        return isCrouched;
    }
}
