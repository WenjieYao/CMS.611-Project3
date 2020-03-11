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
    [SerializeField]
    private int monstersPerSpawn = 1;

    // Monster attacking target
    private GameObject target = null;
    // A temporary number used for spawning control
    private float spawnTimeRemaining = 0;
    // A bool for determining whether to reset spawn
    private bool resetSpawn = true;

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
    public int MonstersPerSpawn
    {
        get
        {
            return this.monstersPerSpawn;
        }
        set
        {
            this.monstersPerSpawn = value;
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
        // Determine if we want to reset spawn
        resetSpawn = !(monsterPrefab.GetComponent<Monster>().SingleSpawn);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnRate = GameManager.Instance.Round/2F;
        spawnTimeRemaining -= Time.fixedDeltaTime;
        // Spawn a new monster when cool down time is 0
        if (spawnTimeRemaining <= 0 && GameManager.Instance.IsNight)
        {
            // Spawn monsters
            SpawnMonsters(monsterPrefab, monstersPerSpawn);
            ResetSpawnTime();
        }
        // Reset spawning time at day
        if (!GameManager.Instance.IsNight)
        {
            spawnTimeRemaining = 1.0F/spawnRate;
        }
    }

    /**
     *<summary> Spawn <c>numMonsters</c> monsters around the spawner</summary>
     */
    private void SpawnMonsters(GameObject monsterPrefab, int numMonsters)
    {
        List<Vector2> spawnVectors = Angle.GetCircularVectorPattern(numMonsters, Vector2.up);
        float vectorScale = 1f;
        foreach (Vector2 spawnVector in spawnVectors)
        {
            // Create a new monster object
            GameObject monster = Instantiate(monsterPrefab, transform.position + (Vector3) spawnVector * vectorScale, Quaternion.identity);
            // Set monster target
            monster.GetComponent<Monster>().Target = target;
            // Set transform to a shared parent
            monster.transform.SetParent(GameManager.Instance.MonsterParent);
        }
    }

    // Restore Cool Down Time
    private void ResetSpawnTime()
    {
        if (resetSpawn)
            spawnTimeRemaining = 1.0F / spawnRate;
        else
            spawnTimeRemaining = 1F / 0F;
    }
}
