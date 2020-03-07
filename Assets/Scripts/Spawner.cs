using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************/
// The Spawner script is used for the monster spawning
// control
/****************************************************/

public class Spawner : Singleton<Spawner>
{
    /****************************************************/
    /***************** Basic Properties *****************/
    /****************************************************/
    // Number of monsters spawned in 1 second
    [SerializeField]
    private float spawnRate = 0;
    // Spawning monster prefab
    [SerializeField]
    private GameObject monsterPrefab = null;

    // Monster attacking target
    private GameObject target = null;
    // A temporary number used or spawning control
    private float spawnTimeRemaining = 0;

    /****************************************************/
    // Public properties that corresponds to the private
    // properties above
    /****************************************************/
    public float SpawnRate
    {
        get
        {
            return spawnRate;
        }
        set
        {
            this.spawnRate = value;
        }
    }

    public GameObject MonsterPrefab
    {
        get
        {
            return monsterPrefab;
        }
        set
        {
            this.monsterPrefab = value;
        }
    }
    /****************************************************/

    /****************************************************/
    /***************** Basic Functions ******************/
    /****************************************************/
    // Start is called before the first frame update
    void Start()
    {
        // Does not spawning at the start 
        spawnTimeRemaining = 1.0F/spawnRate;
        // Set the player as the target
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnRate = GameManager.Instance.Round/2F;
        spawnTimeRemaining -= Time.fixedDeltaTime;
        // Spawn a new monster when cool down time is 0
        if (spawnTimeRemaining <= 0 && GameManager.Instance.IsNight)
        {
            // Create a new monster object
            GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            // Set monster target
            monster.GetComponent<Monster>().Target = target;
            // Set transform to a shared parent
            monster.transform.SetParent(GameManager.Instance.MonsterParent);
            // Restore cool down time
            if (monster.GetComponent<Monster>().IsImmortal)
            {
                // Spawn only one immortal monster per night
                spawnTimeRemaining = 1F/0F;
            }
            else
            {
                spawnTimeRemaining = 1.0F/spawnRate;
            }
        }
        // Reset spawning time at day
        if (!GameManager.Instance.IsNight)
        {
            spawnTimeRemaining = 1.0F/spawnRate;
        }
    }
}
