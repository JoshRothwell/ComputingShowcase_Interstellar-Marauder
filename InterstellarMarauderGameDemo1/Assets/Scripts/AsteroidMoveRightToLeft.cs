using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMoveRightToLeft : MonoBehaviour
{
    //speed at which enemy moves
    public float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //direction of the enemy
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;
        //If enemy position reaches this position, destroy game object
        if (pos.x < -10)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
