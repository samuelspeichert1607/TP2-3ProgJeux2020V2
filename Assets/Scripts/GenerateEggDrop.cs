using UnityEngine;

public class GenerateEggDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject eggEffect;

    private void Awake()
    {
        GameObject yolk = Instantiate(eggEffect, gameObject.transform.position, Quaternion.identity);
        yolk.transform.parent = gameObject.transform;
    }
}
