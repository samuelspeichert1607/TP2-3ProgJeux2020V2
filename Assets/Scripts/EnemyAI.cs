using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
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

    private Vector3 currentDestination;

    private bool hasDestination;

    private Transform centerOfMass;

    private State currentState;

    private float timeLeftUntilGiveUpChase;

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
            GameObject randomPointInHome = home[Random.Range(0, home.Count - 1)];
            if (!canReachDestinationDirectly(randomPointInHome.transform.position))
            {
                currentPath = pathFinding.A_Star(transform.position, randomPointInHome.transform.position);
                currentNode = 0;
            }
            else
            {
                currentPath = null;
                currentNode = 0;
                currentDestination = randomPointInHome.transform.position;
                hasDestination = true;
            }
        }
        

        //move
        if (hasDestination)
        {
            Move(currentDestination, detector.detectedPlayer != null);
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
        float newX = currentPosition.x;
        float newY = currentPosition.y;
        float newZ = currentPosition.y;
        if (currentPosition.x < destination.x)
        {
            newX += speed * Time.deltaTime;
            if (newX > destination.x)
            {
                newX = destination.x;
            }
        }
        else if (currentPosition.x > destination.x)
        {
            newX -= speed * Time.deltaTime;
            if (newX < destination.x)
            {
                newX = destination.x;
            }
        }
        if (currentPosition.y < destination.y)
        {
            newY += speed * Time.deltaTime;
            if (newY > destination.y)
            {
                newY = destination.y;
            }
        }
        else if (currentPosition.y > destination.y)
        {
            newY -= speed * Time.deltaTime;
            if (newY < destination.y)
            {
                newY = destination.y;
            }
        }
        if (currentPosition.z < destination.z)
        {
            newZ += speed * Time.deltaTime;
            if (newZ > destination.z)
            {
                newZ = destination.z;
            }
        }
        else if (currentPosition.z > destination.z)
        {
            newZ -= speed * Time.deltaTime;
            if (newZ < destination.z)
            {
                newZ = destination.z;
            }
        }
        Vector3 newPosition = new Vector3(newX, newY, newZ);

        if (turn)
        {
            Turn(newPosition);
        }
        transform.Translate(newPosition - currentPosition);
    }

    public void Turn(Vector3 destination)
    {
        Vector3 mouvement = destination - transform.position;
        float angle = Mathf.Atan2(mouvement.y, mouvement.x) * Mathf.Rad2Deg;
        transform.Rotate(new Vector3(0,angle,0));
    }
}
