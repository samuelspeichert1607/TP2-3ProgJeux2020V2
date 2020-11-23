using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePoopBag : MonoBehaviour
{
    public GameObject poopBag;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void placeBag()
    {
        poopBag.SetActive(true);
    }
}
