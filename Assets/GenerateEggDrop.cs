using UnityEngine;

public class GenerateEggDrop : MonoBehaviour
{
    private ParticlePooling pooling;
    private GameObject eggEffect;

    private void Awake()
    {
        pooling = GameObject.Find("ParticlePooling").GetComponent<ParticlePooling>();

        eggEffect = Instantiate(pooling.Get("EggDrop"), gameObject.transform.position, Quaternion.identity);
        eggEffect.transform.parent = gameObject.transform;
    }

    private void Update()
    {
        if (eggEffect != null)
        {
            if (!eggEffect.GetComponent<ParticleSystem>().isPlaying)
            {
                //eggEffect.GetComponent<ParticleSystem>().Stop();
                pooling.Return(eggEffect);
                DestroyImmediate(eggEffect);
            }
        }
    }
}
