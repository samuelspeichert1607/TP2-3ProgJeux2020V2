using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEgg : MonoBehaviour
{
    public GameObject eggPrefab;

    public GameObject startLocation;

    public GameObject directionAncor;

    public float throwForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Quaternion rotation = Quaternion.LookRotation(directionAncor.transform.position - startLocation.transform.position);

            GameObject newEgg = Instantiate(eggPrefab, startLocation.transform.position, rotation);
            Egg newEggValues = newEgg.GetComponent<Egg>();
            newEggValues.initialise(throwForce);
        }
    }
}
