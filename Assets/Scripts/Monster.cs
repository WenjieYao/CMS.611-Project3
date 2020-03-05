using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************/
// The Monster script is used for defining Monster 
// properties and monster behaviors
/****************************************************/

public class Monster : Singleton<Monster>
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
    // Monster movement speed
    [SerializeField]
    private float speed = 0;
    // Monster's attacking target
    [SerializeField]
    private GameObject target = null;
    // Current rigidbody2d object
    private Rigidbody2D rb2D;

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
        // The monster heads towards the target
        Vector2 moveDirection = (Vector2)(target.transform.position - transform.position);
        rb2D.MovePosition(rb2D.position + moveDirection * speed * Time.fixedDeltaTime);
        
    }

    // Monster collides with another object
    void OnCollisionEnter2D(Collision2D col)
    {
        // Hit by the projectile
        if (col.gameObject.tag.Equals("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
