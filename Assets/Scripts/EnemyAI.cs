﻿using System.Collections;
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

    private Graph pathFinding;

    private PlayerDetection detector;

    private Vector3 target;

    private List<Node> currentPath;

    private int currentNode;

    private Vector3 currentDestination;

    private bool hasDestination;

    private Transform centerOfMass;

    public State currentState;

    private float timeLeftUntilGiveUpChase;

    private float timeLeftUntilGiveUpDistraction;

    public List<GameObject> home;

    public float speed;

    public float timeUntilGiveUpChase;

    // Start is called before the first frame update
    void Start()
    {
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
        if (timeLeftUntilGiveUpDistraction > 0)
        {
            timeLeftUntilGiveUpDistraction -= Time.deltaTime;
        }


        //check state
        if (detector.detectedPlayer != null)
        {
            Transform playerCenter = detector.detectedPlayer.GetComponent<Transform>().Find("CenterOfMass");
            Vector3 playerPosition = playerCenter.position;
            if (currentState != State.Player || !hasDestination)
            {
                if (!canReachDestinationDirectly(playerPosition))
                {
                    currentPath = pathFinding.A_Star(transform.position, playerPosition);
                    currentNode = 0;
                    currentDestination = currentPath[currentNode].getPosition();
                    hasDestination = true;
                }
                else
                {
                    currentPath = null;
                    currentNode = 0;
                    currentDestination = playerPosition;
                    hasDestination = true;
                }
                currentState = State.Player;
            }
            if (canReachDestinationDirectly(playerPosition))
            {
                currentPath = null;
                currentNode = 0;
                currentDestination = playerPosition;
                hasDestination = true;
            }
            Turn(playerPosition);
            timeLeftUntilGiveUpChase = timeUntilGiveUpChase;
        }
        else if ((timeLeftUntilGiveUpChase <= 0 && currentState == State.Player) || (timeLeftUntilGiveUpDistraction <= 0 && currentState == State.Target) ||
            (currentState == State.Home && !hasDestination))
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
            bool shouldTurn = detector.detectedPlayer == null;
            if (shouldTurn && currentState == State.Target)
            {
                shouldTurn = false;
                Turn(target);
            }
            Move(currentDestination, shouldTurn);

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
                    if (currentState == State.Target)
                    {
                        timeLeftUntilGiveUpDistraction = timeUntilGiveUpChase;
                    }
                }
                else
                {
                    currentDestination = currentPath[currentNode].getPosition();
                    hasDestination = true;
                }
            }
        }

        //look around
        if ((timeLeftUntilGiveUpChase > 0 && currentState == State.Player && detector.detectedPlayer == null && currentPath == null && !hasDestination) ||
            (timeLeftUntilGiveUpDistraction > 0 && currentState == State.Target && currentPath == null && !hasDestination))
        {
            LookAround();
        }

        /*if (currentState == State.Target && currentPath == null && !hasDestination && timeLeftUntilGiveUpDistraction > 0)
        {
            currentDestination = target;
            hasDestination = true;
        }*/


    }

    public void getAttention(Vector3 position)
    {
        if (currentState != State.Player)
        {
            currentState = State.Target;
            target = position;
            currentPath = pathFinding.A_Star(transform.position, position);
            currentNode = 0;
            currentDestination = currentPath[currentNode].getPosition();
            hasDestination = true;
            timeLeftUntilGiveUpDistraction = timeUntilGiveUpChase;
        }
    }

    private bool canReachDestinationDirectly(Vector3 position)
    {
        Vector3 direction = position - centerOfMass.position;
        float distance = Vector3.Distance(centerOfMass.position, position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(centerOfMass.position, direction, distance);
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

    public void LookAround()
    {
        transform.RotateAround(centerOfMass.position, Vector3.up, speed * speed * speed * Time.deltaTime);
    }
}