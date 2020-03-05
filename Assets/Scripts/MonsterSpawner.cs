using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : Singleton<MonsterSpawner>
{

    public float spawnTime;
    public GameObject monsterPrefab;
    private GameObject player;


    private float spawnTimeRemaining;



    // Start is called before the first frame update
    void Start()
    {
        spawnTimeRemaining = spawnTime;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimeRemaining -= Time.fixedDeltaTime;
        if (spawnTimeRemaining <= 0)
        {
            GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            monster.GetComponent<MonsterBehavior>().player = player;
            spawnTimeRemaining = spawnTime;
        }
    }
}
