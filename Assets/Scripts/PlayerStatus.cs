using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isChased;

    private EnemyAI[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        isChased = false;
        enemies = GameObject.FindObjectsOfType<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        isChased = false;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].currentState == EnemyAI.State.Player)
            {
                isChased = true;
            }
        }
    }
}
