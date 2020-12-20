using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFire : MonoBehaviour
{
    private ParticlePooling pooling;
    private GameObject fireEffect;
    
    private void Awake()
    {
        pooling = GameObject.Find("ParticlePooling").GetComponent<ParticlePooling>();
    }

    // Update is called once per frame
    public void OnEnable()
    {
        fireEffect = Instantiate(pooling.Get("Fire"), gameObject.transform.position, Quaternion.identity);

        if (fireEffect != null)
        {
            fireEffect.transform.parent = gameObject.transform;
            fireEffect.transform.position = new Vector3(fireEffect.transform.position.x, fireEffect.transform.position.y + 0.5f, fireEffect.transform.position.z);
        }

        //x = -90, y = 0, z = 0
        fireEffect.transform.rotation = new Quaternion(-0.707106829f, 0, 0, 0.707106829f);
    }

    public void OnDisable()
    {
        if(fireEffect != null)
        {
            fireEffect.GetComponent<ParticleSystem>().Stop();
            pooling.Return(fireEffect);
        }
    }
}
