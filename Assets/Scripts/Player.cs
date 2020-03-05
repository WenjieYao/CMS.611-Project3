﻿using System.Collections;
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
    // Player health
    [SerializeField]
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

    // Current rigidbody2d object
    private Rigidbody2D rb2d;
    // A temporary vector indicating the movement direction
    private Vector2 moveVector;
    // A temporary value to apply the fire rate
    private float coolDownInterval;

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
    /****************************************************/

    /****************************************************/
    /***************** Basic Functions ******************/
    /****************************************************/
    // Start is called before the first frame update
    void Start()
    {
        // Get current rigidbody2d object
        rb2d = GetComponent<Rigidbody2D>();
        // Player doesn't fire at the beginning
        coolDownInterval = 1.0F/fireRate;
    }

    // Get the firing direction as a vector
    private Vector2 GetFireDirection(int angularOffset)
	{
        float rotateAngle = angularOffset + rb2d.rotation;

        float x = Mathf.Cos(rotateAngle * Mathf.Deg2Rad);
        float y = Mathf.Sin(rotateAngle * Mathf.Deg2Rad);
        Vector2 rotateVector = new Vector2(x, y);

        return rotateVector;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move the player
        moveVector = new Vector2(0, 0);
        int rotateDirection = 0;

        if (Input.GetKey("w"))
            moveVector += Vector2.up;
        if (Input.GetKey("s"))
            moveVector += Vector2.down;
        if (Input.GetKey("a"))
            moveVector += Vector2.left;
        if (Input.GetKey("d"))
            moveVector += Vector2.right;
        // Rotate the player
        if (Input.GetKey(KeyCode.LeftArrow))
            rotateDirection += 1;
        if (Input.GetKey(KeyCode.RightArrow))
            rotateDirection += -1;

        moveVector.Normalize();
        // Implement movement and rotation using rigidbody2d
        rb2d.MovePosition(rb2d.position + speed * moveVector * Time.fixedDeltaTime);
        rb2d.MoveRotation(rb2d.rotation + rotateDirection * revSpeed * Time.fixedDeltaTime);

        Vector2 fireDirection = GetFireDirection(angularOffset);

        // The cool down time
        coolDownInterval -= Time.fixedDeltaTime;
        // Fire a projectile when the cool down time is 0
        if (coolDownInterval <= 0 && (isAutomatic || Input.GetKey("space")))
        {
            // Restore the cool down time according to fire rate
            coolDownInterval = 1.0F/fireRate;
            // Fire a projectile
            GameObject bullet = Instantiate(projectilePrefab, transform.position + 0.1f * (Vector3) fireDirection, Quaternion.identity) as GameObject;
            // Apply force to get a initial velocity
            bullet.GetComponent<Rigidbody2D>().AddForce(fireDirection * 500);
            // Set a shared parent
            bullet.transform.SetParent(GameManager.Instance.ProjectileParent);
        }


    }

}