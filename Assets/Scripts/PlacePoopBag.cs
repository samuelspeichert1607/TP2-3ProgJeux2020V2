using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePoopBag : MonoBehaviour
{
    public GameObject poopBag;

    private DetectCollisions collisions;

    public bool isPlayerClose;

    // Start is called before the first frame update
    void Start()
    {
        collisions = GetComponentInChildren<DetectCollisions>();
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerClose = false;
        List<Collider> colliders = collisions.getTriggers();
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i] is CharacterController)
            {
                isPlayerClose = true;
            }
        }
        if (isPlayerClose)
        {
            //code display HUD prompt here

            if (Input.GetKeyDown(KeyCode.Space))
            {
                poopBag.SetActive(true);
            }
        }
    }
}
