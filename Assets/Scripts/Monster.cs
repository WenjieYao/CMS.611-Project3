using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************/
// The Monster script is used for defining Monster 
// properties and monster behaviors
/****************************************************/

public class Monster: Singleton<Monster>
{
    /****************************************************/
    /***************** Basic Properties *****************/
    /****************************************************/
    // Monster health
    [SerializeField]
    private int health = 0;
    // Monster attack power
    [SerializeField]
    private int attackPower = 0;
    // Monster attack rate
    [SerializeField]
    private float attackRate = 0;
    // Is a immortal target or not
    [SerializeField]
    private bool isImmortal = false;
    [SerializeField]
    private bool singleSpawn = false;
    // Monster movement speed
    [SerializeField]
    private float speed = 0;
    // Monster's attacking target
    [SerializeField]
    private GameObject target = null;
    // Prefab of monster drops collectables
    [SerializeField]
    private GameObject collectiblePrefab = null;
    // Prefab of monster drop text
    [SerializeField]
    private GameObject dropTextPrefab = null;
    // Current rigidbody2d object
    private Rigidbody2D rb2D;
    // Attack cool down time
    private float attackCoolDown = 0;
    /****************************************************/
    // Public properties that corresponds to the private
    // properties above
    /****************************************************/
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            this.health = value;
        }
    }

    public int AttackPower
    {
        get
        {
            return attackPower;
        }
        set
        {
            this.attackPower = value;
        }
    }

    public float AttackRate
    {
        get
        {
            return attackRate;
        }
        set
        {
            this.attackRate = value;
        }
    }

    public bool IsImmortal
    {
        get
        {
            return isImmortal;
        }
        private set
        {
            this.isImmortal = value;
        }
    }
    public bool SingleSpawn
    {
        get
        {
            return singleSpawn;
        }
        set
        {
            this.singleSpawn = value;
        }
    }
    public GameObject Target
    {
        get
        {
            return target;
        }
        set
        {
            this.target = value;
        }
    }

    public GameObject CollectiblePrefab
    {
        get
        {
            return collectiblePrefab;
        }
        set
        {
            this.collectiblePrefab = value;
        }
    }
    /****************************************************/

    /****************************************************/
    /***************** Basic Functions ******************/
    /****************************************************/
    // Start is called before the first frame update
    void Start()
    {
        // Initialized current rigidbody2d object
        rb2D = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        // Monsters die when day comes
        if (!GameManager.Instance.IsNight)
            Destroy(gameObject);
        // The monster heads towards the target
        Vector2 moveDirection = (Vector2)(target.transform.position - transform.position);
        rb2D.MovePosition(rb2D.position + moveDirection * speed * Time.fixedDeltaTime);
        attackCoolDown -= Time.fixedDeltaTime;
    }

    // Monster collides with another object
    void OnCollisionEnter2D(Collision2D col)
    {
        // Hit by the projectile
        if (col.gameObject.tag.Equals("Projectile"))
        {
            if (!IsImmortal)
            {
                health -= Player.Instance.AttackPower;
                // Enemy died when health reaches 0
                if (health<=0)
                {
                    Die();
                }
            }
        }
        // Attack the player
        if (col.gameObject.tag.Equals("Player") && attackCoolDown <=0)
        {
            Player.Instance.Health -= attackPower;
            Player.Instance.PlayerHealthBar.SetHealth(Player.Instance.Health);
            attackCoolDown = 1.0F/attackRate;
        }
    }

    // Monster collides with another object and stays
    void OnCollisionStay2D(Collision2D col)
    {
        // Attack the player
        if (col.gameObject.tag.Equals("Player") && attackCoolDown <=0)
        {
            Player.Instance.Health -= attackPower;
            Player.Instance.PlayerHealthBar.SetHealth(Player.Instance.Health);
            attackCoolDown = 1.0F/attackRate;
        }
    }

    // Code to Execute When Monster Dies
    private void Die()
    {
        // Player gain tech cash
        int techCash = GameManager.Instance.Round;
        GameManager.Instance.TechCash += techCash;
        GameObject dropText = Instantiate(dropTextPrefab, transform.position, Quaternion.identity);
        dropText.GetComponent<DropText>().Text = "+" + techCash + "$";
        // Drop collectables at a chance of 5%
        if (Random.Range(0.0f, 1.0f) > 0.95)
        {
            GameObject collectible = Instantiate(collectiblePrefab, transform.position, Quaternion.identity) as GameObject;

            collectible.transform.SetParent(GameManager.Instance.MonsterParent);
        }
        Destroy(gameObject);
    }
}
