using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public GameObject HouseOwner;

    private EnemyAI owner;
    // Start is called before the first frame update
    void Start()
    {
        owner = HouseOwner.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EnemyAI getOwner()
    {
        return owner;
    }
}
