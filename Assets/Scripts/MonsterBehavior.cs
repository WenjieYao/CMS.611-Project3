using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{

    public float speed;
    public GameObject player;

    Vector2 movement;

    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {

    }

    // FixedUpdate is called 50 times a second
    void Update()
    {
        Vector2 moveDirection = (Vector2)(player.transform.position - transform.position);

        body.MovePosition(body.position + moveDirection * speed * Time.fixedDeltaTime);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Projectile"))
        {
            col.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
