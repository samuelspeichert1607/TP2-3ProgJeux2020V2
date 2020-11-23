using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public GameObject brokenEggPrefab;

    public float speed;

    public float elapsedTime;

    public float timeUntillStartFalling;

    private bool isGoingDown;

    private DetectCollisions collisions;

    public bool isBroken;

    // Start is called before the first frame update
    void Start()
    {
        collisions = GetComponent<DetectCollisions>();
        isBroken = false;
        isGoingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (!isGoingDown && elapsedTime >= timeUntillStartFalling)
        {
            transform.Rotate(-1*transform.eulerAngles.x*2, 0, 0);
            isGoingDown = true;
        }

        //check collisions
        List<Collider> colliders = collisions.getTriggers();
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].GetComponent<AlertHouseOwner>() != null)
            {
                AlertHouseOwner test = colliders[i].GetComponent<AlertHouseOwner>();
                if (test.type == AlertHouseOwner.AlertType.Window)
                {
                    test.AlertOwner();
                }
            }
            if (colliders[i].GetComponent<ObjectAIBehavior>() != null)
            {
                ObjectAIBehavior objectInfo = colliders[i].GetComponent<ObjectAIBehavior>();
                if (!objectInfo.canActorsPassThrough)
                {
                    isBroken = true;
                    Instantiate(brokenEggPrefab, transform.position, colliders[i].transform.rotation);
                    gameObject.SetActive(false);
                    break;
                }
            }
        }

        //move
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    public void initialise(float _speed)
    {
        speed = _speed;
        elapsedTime = 0;
    }
}
