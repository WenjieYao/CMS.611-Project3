using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMovement : MonoBehaviour
{

    public float horizontalSpeed = 5f;

    Vector2 movement;

    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        movement.x = horizontalSpeed;
        movement.y = 0;
    }

    // FixedUpdate is called 50 times a second
    void Update()
    {
        body.MovePosition(body.position + movement);
        
    }
}
