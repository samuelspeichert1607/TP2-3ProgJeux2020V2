using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Home,
        Player,
        Target
    }

    private Rigidbody body;

    private Graph pathFinding;

    private PlayerDetection detector;

    private Vector3 target;

    private List<Node> currentPath;

    private int currentNode;

    public Vector3 currentDestination;

    private bool hasDestination;

    private Transform centerOfMass;

    public State currentState;

    public float timeLeftUntilGiveUpChase;

    public List<GameObject> home;

    public float speed;


    public float timeUntilGiveUpChase;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        GameObject graph = GameObject.Find("Graph");
        pathFinding = graph.GetComponent<Graph>();
        detector = GetComponent<PlayerDetection>();
        centerOfMass = GetComponent<Transform>().Find("CenterOfMass");
        timeLeftUntilGiveUpChase = 0;
        hasDestination = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeftUntilGiveUpChase > 0)
        {
            timeLeftUntilGiveUpChase -= Time.deltaTime;
        }


        //check state
        if (detector.detectedPlayer != null)
        {
            if (currentState != State.Player || !hasDestination)
            {
                if (!canReachDestinationDirectly(detector.detectedPlayer.transform.position))
                {
                    currentPath = pathFinding.A_Star(transform.position, detector.detectedPlayer.transform.position);
                    currentNode = 0;
                    currentDestination = currentPath[currentNode].getPosition();
                    hasDestination = true;
                }
                else
                {
                    currentPath = null;
                    currentNode = 0;
                    currentDestination = detector.detectedPlayer.transform.position;
                    hasDestination = true;
                }
                currentState = State.Player;
            }
            if (canReachDestinationDirectly(detector.detectedPlayer.transform.position))
            {
                currentPath = null;
                currentNode = 0;
                currentDestination = detector.detectedPlayer.transform.position;
                hasDestination = true;
            }
            Turn(detector.detectedPlayer.transform.position);
            timeLeftUntilGiveUpChase = timeUntilGiveUpChase;
        }
        else if ((timeLeftUntilGiveUpChase <= 0 && currentState == State.Player) || (currentState == State.Home && !hasDestination))
        {
            currentState = State.Home;
            GameObject randomPointInHome = home[Random.Range(0, home.Count)];
            currentPath = pathFinding.A_Star(transform.position, randomPointInHome.transform.position);
            currentNode = 0;
            currentDestination = currentPath[currentNode].getPosition();
            hasDestination = true;
        }
        

        //move
        if (hasDestination)
        {
            Move(currentDestination, detector.detectedPlayer == null);
            if (centerOfMass.position == currentDestination)
            {
                currentDestination = new Vector3();
                hasDestination = false;
            }
        }

        //check path
        if (currentPath != null)
        {
            if (!hasDestination)
            {
                currentNode++;
                if (currentNode >= currentPath.Count)
                {
                    currentPath = null;
                    currentDestination = new Vector3();
                    hasDestination = false;
                    currentNode = 0;
                }
                else
                {
                    currentDestination = currentPath[currentNode].getPosition();
                    hasDestination = true;
                }
            }
        }


    }

    public void getAttention(Vector3 position)
    {
        if (currentState != State.Player)
        {
            target = position;
            if (!canReachDestinationDirectly(position))
            {
                currentPath = pathFinding.A_Star(transform.position, target);
                currentNode = 0;
                currentState = State.Target;
            }
            else
            {
                currentPath = null;
                currentNode = 0;
                currentDestination = position;
                hasDestination = true;
            }
        }
    }

    private bool canReachDestinationDirectly(Vector3 position)
    {
        Vector3 direction = centerOfMass.position - position;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(centerOfMass.position, direction, Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2)));
        bool canGoToObject = true;
        for (int j = 0; j < hits.Length; j++)
        {
            if (hits[j].collider.GetComponent<ObjectAIBehavior>() != null)
            {
                ObjectAIBehavior behavior = hits[j].collider.GetComponent<ObjectAIBehavior>();
                if (!behavior.canActorsPassThrough)
                {
                    canGoToObject = false;
                    break;
                }
            }
        }
        return canGoToObject;
    }

    public void Move(Vector3 destination, bool turn)
    {
        Vector3 currentPosition = centerOfMass.position;

        if (turn)
        {
            Turn(destination);
        }
        transform.position = Vector3.MoveTowards(currentPosition, destination, speed * Time.deltaTime);
    }

    public void Turn(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * Time.deltaTime / 2);

    }
}
