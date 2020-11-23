using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSoap : MonoBehaviour
{
    public GameObject soapPrefab;

    public GameObject soapDropLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(soapPrefab, soapDropLocation.transform.position, Quaternion.identity);
        }
    }
}
