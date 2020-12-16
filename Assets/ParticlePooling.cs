using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooling : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> particleEffects;

    private Stack<GameObject> particlePool;
    
    private Stack<GameObject> tempParticlePool;

    public int effectCount;
    
    private void Awake()
    {
        particlePool = new Stack<GameObject>();
        tempParticlePool = new Stack<GameObject>();

        foreach (var particleEffect in particleEffects)
        {
            particlePool.Push(particleEffect);
        }
    }
    
    public GameObject Get(string nomParticuleDemandee)
    {
        if(particlePool.Count == 0) // || particlePool.Contains(GameObject.Find("Fire"))
        {
            return null;
        }


        bool objetTrouve = false;

        GameObject particule = null;

        while (!objetTrouve)
        {
            GameObject particuleSortie = particlePool.Pop();

            if (particuleSortie.name == nomParticuleDemandee)
            {
                particule = particuleSortie;
                
                objetTrouve = true;
            }
            else
            {
                tempParticlePool.Push(particuleSortie);
            }
        }

        //On remet les particules sorties inutilement dans la stack principale.
        while (tempParticlePool.Count != 0)
        {
            particlePool.Push(tempParticlePool.Pop());
        }

        return particule;
    }

    public void Return(GameObject instance)
    {
        if(instance != null)
        {
            particlePool.Push(instance);
        }
    }

    private void Update()
    {
        effectCount = particlePool.Count;
    }
}