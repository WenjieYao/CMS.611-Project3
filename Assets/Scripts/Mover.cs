using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 moveVector;
    public int speed;
    public int revSpeed;
    public int angularOffset;
    public GameObject projectile;
    public bool isAutomatic;

    public float coolDown;
    private float coolDownInterval;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coolDownInterval = coolDown;
    }

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

        if (Input.GetKey(KeyCode.LeftArrow))
            rotateDirection += 1;
        if (Input.GetKey(KeyCode.RightArrow))
            rotateDirection += -1;

        moveVector.Normalize();

        rb2d.MovePosition(rb2d.position + speed * moveVector * Time.fixedDeltaTime);
        rb2d.MoveRotation(rb2d.rotation + rotateDirection * revSpeed * Time.fixedDeltaTime);

        Vector2 fireDirection = GetFireDirection(angularOffset);


        coolDownInterval -= Time.fixedDeltaTime;

        if (coolDownInterval <= 0 && (isAutomatic || Input.GetKey("space")))
        {
            coolDownInterval = coolDown;
            GameObject bullet = Instantiate(projectile, transform.position + 0.1f * (Vector3) fireDirection, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce(fireDirection * 1000);
        }


    }
}