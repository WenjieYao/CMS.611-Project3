using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************/
// The Player script is used for defining player
// properties and player behaviors
/****************************************************/

public class Player : Singleton<Player>
{
    /****************************************************/
    /***************** Basic Properties *****************/
    /****************************************************/
    // Player maximum health
    [SerializeField]
    private int maxHealth = 0;
    // Player health
    private int health = 0;
    // Player attack power
    [SerializeField]
    private int attackPower = 0;
    // Player fire rate
    // Number of bullets fired per second
    [SerializeField]
    private float fireRate = 0;
    // Player movement speed
    [SerializeField]
    private float speed = 0;
    // Firing angular speed
    [SerializeField]
    private int revSpeed = 0;
    // Initial shooting direction
    [SerializeField]
    private int angularOffset = 0;
    // Shooting project prefab
    [SerializeField]
    private GameObject projectilePrefab = null;
    // Does the play shoot automatically?
    [SerializeField]
    private bool isAutomatic = false;

    // Health bar of the player
    [SerializeField]
    private HealthBar playerHealthBar = null;

    // Current rigidbody2d object
    private Rigidbody2D rb2d;
    // A temporary vector indicating the movement direction
    private Vector2 moveVector;
    // A temporary value to apply the fire rate
    private float coolDownInterval;
    // Fire direction using arrow key or mouse
    private bool FDMouse = true;
    // Bullet shooting rotation
    private Vector3 bulletRotation = new Vector3(0,0,0);

    /****************************************************/
    // Public properties that corresponds to the private
    // properties above
    /****************************************************/
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            this.maxHealth = value;
        }
    }

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

    public float FireRate
    {
        get
        {
            return fireRate;
        }
        set
        {
            this.fireRate = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            this.speed = value;
        }
    }

    public int RevSpeed
    {
        get
        {
            return revSpeed;
        }
        set
        {
            this.revSpeed = value;
        }
    }

    public int AngularOffset
    {
        get
        {
            return angularOffset;
        }
        set
        {
            this.angularOffset = value;
        }
    }

    public GameObject ProjectilePrefab
    {
        get
        {
            return projectilePrefab;
        }
        set
        {
            this.projectilePrefab = value;
        }
    }

    public bool IsAutomatic
    {
        get
        {
            return isAutomatic;
        }
        set
        {
            this.isAutomatic = value;
        }
    }

    public HealthBar PlayerHealthBar
    {
        get
        {
            return playerHealthBar;
        }
        set
        {
            this.playerHealthBar = value;
        }
    }
    /****************************************************/

    /****************************************************/
    /***************** Basic Functions ******************/
    /****************************************************/
    // Start is called before the first frame update
    void Start()
    {
        // Initialze player with full health
        health = maxHealth;
        // Initialze health bar
        playerHealthBar.SetMaxHealth(maxHealth);
        // Get current rigidbody2d object
        rb2d = GetComponent<Rigidbody2D>();
        // Player doesn't fire at the beginning
        coolDownInterval = 1.0F/fireRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update Position and Rotation of Player
        UpdatePosition();
        Vector2 fireDirection = UpdateRotation();
        
        // The cool down time
        coolDownInterval -= Time.fixedDeltaTime;
        CheckIfFireProjectile(fireDirection);
    }

    // Collect free food to refill heath
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("FreeFood"))
        {
            if (health<maxHealth)
            {
                health += 1;
                playerHealthBar.SetHealth(health);
            }
            Destroy(collision.gameObject);
        }
    }

    // Player triggers the shop interface
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Shop"))
        {
            GameManager.Instance.ShopMenu.SetActive(true);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        PlayerHealthBar.SetHealth(Health);
    }

    // Move physical position upon update
    private void UpdatePosition()
    {
        moveVector = new Vector2(0, 0);

        if (Input.GetKey("w"))
            moveVector += Vector2.up;
        if (Input.GetKey("s"))
            moveVector += Vector2.down;
        if (Input.GetKey("a"))
            moveVector += Vector2.left;
        if (Input.GetKey("d"))
            moveVector += Vector2.right;

        moveVector.Normalize();
        rb2d.MovePosition(rb2d.position + speed * moveVector * Time.fixedDeltaTime);
    }

    // Set player to direction of mouse and return the direction
    private Vector2 UpdateRotation()
    {
        int rotateDirection = 0;
        // Rotate the player
        if (Input.GetKey(KeyCode.LeftArrow))
            rotateDirection += 1;
        if (Input.GetKey(KeyCode.RightArrow))
            rotateDirection += -1;


        Vector2 fireDirection = GetFireDirection(angularOffset, FDMouse);
        bulletRotation.z = -angularOffset + (float)Mathf.Atan2(fireDirection.y, fireDirection.x) * (float)(180 / Mathf.PI);

        // Implement movement and rotation using rigidbody2d
        transform.GetChild(0).rotation = Quaternion.Euler(bulletRotation);

        return fireDirection;
    }

    // Fire Projectile in direction of fireDirection if cooldown time is reached
    private void CheckIfFireProjectile(Vector2 fireDirection)
    {
        if (coolDownInterval <= 0 && GameManager.Instance.IsNight && (isAutomatic || Input.GetKey("space")))
        {
            // Restore the cool down time according to fire rate
            coolDownInterval = 1.0F / fireRate;
            // Fire a projectile
            GameObject bullet = Instantiate(projectilePrefab, transform.position + 0.15f * (Vector3)fireDirection, Quaternion.Euler(bulletRotation)) as GameObject;
            // Apply force to get a initial velocity
            bullet.GetComponent<Rigidbody2D>().velocity = fireDirection * 8;
            // Set a shared parent
            bullet.transform.SetParent(GameManager.Instance.ProjectileParent);
        }
    }

    // Get the firing direction as a vector
    private Vector2 GetFireDirection(int angularOffset, bool FDMouse)
    {
        Vector2 rotateVector = new Vector2(0, 0);
        if (FDMouse)
        {
            // Fire at the mouse direction
            Vector3 MouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotateVector.x = MouseDirection.x;
            rotateVector.y = MouseDirection.y;
        }
        else
        {
            // Tune direction using arrow key
            float rotateAngle = angularOffset + rb2d.rotation;
            rotateVector.x = Mathf.Cos(rotateAngle * Mathf.Deg2Rad);
            rotateVector.y = Mathf.Sin(rotateAngle * Mathf.Deg2Rad);
        }
        rotateVector.Normalize();
        return rotateVector;
    }
}