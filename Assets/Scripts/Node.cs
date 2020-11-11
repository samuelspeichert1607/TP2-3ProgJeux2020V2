using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<GameObject> neighborsObjects;

    private List<Node> neighborsNodes;

    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    public void initialize()
    {
        neighborsNodes = new List<Node>();
        for (int i = 0; i < neighborsObjects.Count; i++)
        {
            Vector3 direction = getPosition() - neighborsObjects[i].transform.position;
            RaycastHit[] hits;
            hits = Physics.RaycastAll(getPosition(), direction, Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2)));
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
            if (canGoToObject)
            {
                Node neighbor = neighborsObjects[i].GetComponent<Node>();
                neighborsNodes.Add(neighbor);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }

    public List<Node> getNeighbors()
    {
        return neighborsNodes;
    }

    public static bool operator ==(Node left, Node right)
    {
        return left.getPosition() == right.getPosition();
    }

    public static bool operator !=(Node left, Node right)
    {
        return left.getPosition() != right.getPosition();
    }
}
