using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private List<Collision> collisions;
    private List<Collider> triggers;
    // Start is called before the first frame update
    void Start()
    {
        collisions = new List<Collision>();
        triggers = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions.Add(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        triggers.Add(other);
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions.Remove(collision);
    }

    private void OnTriggerExit(Collider other)
    {
        triggers.Remove(other);
    }

    public List<Collision> getCollisions()
    {
        return collisions;
    }

    public List<Collider> getTriggers()
    {
        return triggers;
    }
}
