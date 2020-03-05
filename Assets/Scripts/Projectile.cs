using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************/
// The Projectile script is used for defining projectile
// properties and projectile behaviors
/****************************************************/

public class Projectile : Singleton<Projectile>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Destroy the projectile when it hits a target
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player"))
            Destroy(gameObject);
    }
}
