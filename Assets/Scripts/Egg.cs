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

    private Vector3 originalLocation;

    // Start is called before the first frame update
    void Start()
    {
        collisions = GetComponent<DetectCollisions>();
        isGoingDown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
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
                    Vector3 brokenEggPosition = transform.position;

                    Vector3 direction = brokenEggPosition - originalLocation;
                    float distance = Vector3.Distance(originalLocation, brokenEggPosition);
                    RaycastHit[] hits;
                    hits = Physics.RaycastAll(originalLocation, direction, distance);
                    for (int j = 0; j < hits.Length; j++)
                    {
                        if (hits[j].collider == colliders[i])
                        {
                            brokenEggPosition = hits[j].point;
                            break;
                        }
                    }


                    Instantiate(brokenEggPrefab, brokenEggPosition, colliders[i].transform.rotation);
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
        originalLocation = transform.position;
    }


}
