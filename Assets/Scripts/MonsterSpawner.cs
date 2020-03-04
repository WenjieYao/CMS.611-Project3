using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public float spawnTime;
    public GameObject monsterPrefab;


    private float spawnTimeRemaining;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimeRemaining -= Time.fixedDeltaTime;
        if (spawnTimeRemaining <= 0)
        {
            GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        }
    }
}
