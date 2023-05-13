using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRightToLeft : MonoBehaviour
{
    //Speed of enemy
    public float moveSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //direction of enemy
        Vector2 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;

       if (pos.x < -10)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
