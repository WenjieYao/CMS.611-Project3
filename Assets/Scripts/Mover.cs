using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 moveVector;
    public int speed;
    public GameObject projectile;

    public float coolDown;
    private float coolDownInterval;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coolDownInterval = coolDown;
    }

    // Update is called once per frame
    void FixedUpdate()
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

        coolDownInterval -= Time.fixedDeltaTime;

        if (coolDownInterval <= 0 && Input.GetKey("space") && moveVector.sqrMagnitude > 0)
        {
            coolDownInterval = coolDown;
            GameObject bullet = Instantiate(projectile, transform.position + (Vector3)moveVector, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce(moveVector * 1000);
        }


    }
}
