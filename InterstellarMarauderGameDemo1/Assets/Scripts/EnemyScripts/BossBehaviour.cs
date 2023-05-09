using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    //Speed of enemy
    public float moveSpeed = 7;
    public float maxY = 2f;
    public float minY = -2f;
    private bool moveVertically = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start moving horizontally
        moveVertically = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Direction of enemy
        Vector2 pos = transform.position;

        if (!moveVertically)
        {
            // Move along X-axis
            pos.x -= moveSpeed * Time.fixedDeltaTime;

            // Check if X position is less than 5
            if (pos.x < 5)
            {
                // Start moving vertically
                moveVertically = true;
            }
        }
        else
        {
            // Move along Y-axis
            pos.y += moveSpeed * Time.fixedDeltaTime;

            // Check if Y position is greater than maxY
            if (pos.y > maxY)
            {
                // Change direction to move down
                moveSpeed = -moveSpeed;
            }

            // Check if Y position is less than minY
            if (pos.y < minY)
            {
                // Change direction to move up
                moveSpeed = -moveSpeed;
            }
        }

        // Update position
        transform.position = pos;
    }
}
