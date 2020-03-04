using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{

    public float speed;
    public GameObject player;

    Vector2 movement;

    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called 50 times a second
    void Update()
    {
        Vector2 moveDirection = (Vector2)(player.transform.position - transform.position);

        rb2D.MovePosition(rb2D.position + moveDirection * speed * Time.fixedDeltaTime);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Projectile"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
