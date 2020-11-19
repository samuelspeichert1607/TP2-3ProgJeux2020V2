using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private Node[] Nodes;

    public bool DrawGraph;//debug only

    // Start is called before the first frame update
    void Start()
    {
        Nodes = GetComponentsInChildren<Node>();
        if (DrawGraph)
        {
            drawGraph(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Node[] getNodes()
    {
        return Nodes;
    }

    public void drawGraph(bool inEditor)
    {
        if (inEditor)
        {
            Nodes = GetComponentsInChildren<Node>();
        }
        for (int i = 0; i < Nodes.Length; i++)
        {
            Nodes[i].initialize();
            Vector3 position = Nodes[i].getPosition();
            Debug.DrawLine(position - new Vector3(0.1f, 0.1f, 0.1f), position + new Vector3(0.1f, 0.1f, 0.1f), Color.blue);

            List<Node> neighbors = Nodes[i].getNeighbors();
            for (int j = 0; j < neighbors.Count; j++)
            {
                List<Node> neighborsNeighbors = neighbors[j].getNeighbors();
                for (int k = 0; k < neighborsNeighbors.Count; k++)
                {
                    if (neighborsNeighbors[k] == Nodes[i])
                    {
                        Debug.DrawLine(position, neighbors[j].getPosition(), Color.green);
                        break;
                    }
                    Debug.DrawLine(position, neighbors[j].getPosition(), Color.red);
                }
            }
        }
    }


    public Node getColsestNodeToPoint(Vector3 position)
    {
        Node closestNode = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < Nodes.Length; i++)
        {
            Vector3 direction = Nodes[i].getPosition() - position;
            float currentDistance = Vector3.Distance(Nodes[i].getPosition(), position);
            if (closestNode == null || currentDistance < closestDistance)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(position, direction, currentDistance);
                bool canGoToObject = true;
                for (int j = 0; j < hits.Length; j++)
                {
                    if (hits[j].collider.GetComponent<ObjectAIBehavior>() != null)
                    {
                        ObjectAIBehavior behavior = hits[j].collider.GetComponent<ObjectAIBehavior>();
                        if (!behavior.canActorsPassThrough)
                        {
                            Debug.Log("ok");
                            if (Nodes[i].gameObject.name == "GameObject(15)")
                            {
                                Debug.Log("ok");
                            }
                            canGoToObject = false;
                            break;
                        }
                    }
                }
                if (canGoToObject)
                {
                    closestNode = Nodes[i];
                    closestDistance = currentDistance;
                }
            }
        }

        return closestNode;
    }

    public List<Node> A_Star(Vector3 start, Vector3 end)
    {
        Node startNode = getColsestNodeToPoint(start);
        Node endNode = getColsestNodeToPoint(end);

        // The set of currently discovered nodes that are not evaluated yet.
        // Initially, only the start node is known.
        List<Node> openSet = new List<Node>();
        openSet.Add(startNode);

        bool[] vistedNodes = new bool[Nodes.Length];

        // For each node, which node it can most efficiently be reached from.
        // If a node can be reached from many nodes, cameFrom will eventually contain the
        // most efficient previous step.
        Node[] cameFrom = new Node[Nodes.Length];

        // For each node, the cost of getting from the start node to that node.
        float[] gScore = new float[Nodes.Length];

        // For each node, the total cost of getting from the start node to the goal
        // by passing by that node. That value is partly known, partly heuristic.
        float[] fScore = new float[Nodes.Length];
        //initialize all the lists
        for (int i = 0; i < Nodes.Length; i++)
        {
            cameFrom[i] = null;
            vistedNodes[i] = false;
            if (Nodes[i] == startNode)
            {
                // The cost of going from start to start is zero.
                gScore[i] = 0;
                // For the first node, that value is completely heuristic.
                fScore[i] = getDistance(startNode.getPosition(), endNode.getPosition());
            }
            else
            {
                gScore[i] = float.MaxValue;
                fScore[i] = float.MaxValue;
            }
        }

        while (openSet.Count > 0)
        {
            /*if (!isAlive)
            {
                return null;
            }*/
            int currentPosition = findPositionOfSmallest(fScore, vistedNodes);
            Node current = Nodes[currentPosition];
            if (current == endNode)
            {
                return reconstruct_path(cameFrom, current);
            }
            openSet.Remove(current);
            vistedNodes[currentPosition] = true;
            foreach (Node neighbor in current.getNeighbors())
            {
                int neighborPosition = findPositionInGrid(neighbor);
                bool isNeighborVisited = false;
                isNeighborVisited = vistedNodes[neighborPosition];
                if (!isNeighborVisited)
                {
                    if (!isNodeInList(openSet, current))
                    {
                        openSet.Add(current);
                    }
                    float tentative_gScore = gScore[currentPosition] + getDistance(current.getPosition(), neighbor.getPosition());
                    if (!(tentative_gScore >= gScore[neighborPosition]))
                    {
                        cameFrom[neighborPosition] = current;
                        gScore[neighborPosition] = tentative_gScore;
                        fScore[neighborPosition] = tentative_gScore + getDistance(neighbor.getPosition(), endNode.getPosition());
                    }
                }
            }
        }
        Debug.Log("A* couldn't find the path between " + startNode.getPosition() + " and " + endNode.getPosition());
        throw new System.Exception("A* couldn't find the path between " + startNode.getPosition() + " and " + endNode.getPosition());
    }

    private List<Node> reconstruct_path(Node[] cameFrom, Node current)
    {
        List<Node> totalPath = new List<Node>();
        totalPath.Add(current);
        while (cameFrom[findPositionInGrid(current)] != null)
        {
            current = cameFrom[findPositionInGrid(current)];
            List<Node> temp = new List<Node>();
            temp.Add(current);
            temp.AddRange(totalPath);
            totalPath = temp;
        }
        return totalPath;
    }

    private int findPositionOfSmallest(float[] fScore, bool[] visitedNodes)
    {
        float smallestYet = float.MaxValue;
        int positionOfSmallest = -1;
        for (int i = 0; i < fScore.Length; i++)
        {
            if (!visitedNodes[i] && fScore[i] < smallestYet)
            {
                smallestYet = fScore[i];
                positionOfSmallest = i;
            }
        }
        if (positionOfSmallest == -1)
        {
            Debug.Log("oh no");
        }
        return positionOfSmallest;
    }

    private float getDistance(Vector3 nodeA, Vector3 nodeB)
    {
        Vector3 direction = nodeA - nodeB;
        return Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2));
    }

    private bool isNodeInList(List<Node> nodes, Node node)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] == node)
            {
                return true;
            }
        }
        return false;
    }

    private int findPositionInGrid(Node node)
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
            if (Nodes[i] == node)
            {
                return i;
            }
        }
        throw new System.Exception("the given node (" + node.getPosition() + ") isn't in the grid");
    }



}
