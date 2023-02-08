using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //An array containing all the "guns" the ship can use, this will be important for later
    //on in development, I can add different weapons.
    Gun[] guns;

    //The speed at which the ship can move.
    float moveSpeed = 5;

    //Booleans for control functions, this is temporary.
    bool moveUp;
    bool moveDown;
    bool moveRight;
    bool moveLeft;
    bool speedUp;

    bool shoot;

    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
    }
    
    void Update() {
        //The input keys for controlling the ship.
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        
        //This is designed to only fire upon pressing the button down, this prevents the gun from
        //firing automatically
        shoot = Input.GetKeyDown(KeyCode.LeftControl);
        if (shoot)
        {
            shoot = false;
            foreach(Gun gun in guns)
            {
                gun.Shoot();
            }
        }

    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        if (speedUp)
        {
            moveAmount *= 2;
        }
        Vector2 move = Vector2.zero;

        if (moveUp) {
            move.y += moveAmount;
        }
        if (moveDown) {
            move.y -= moveAmount;
        }
        if (moveLeft) {
            move.x -= moveAmount;
        }
        if (moveRight) {
            move.x += moveAmount;
        }
        //A calculation designed to properly calcuate the diagonial movement of the ship.
        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }
      // Code for preventing the ship from flying outside the edge of the screen.
        Debug.Log(moveMagnitude);
        pos += move;
        if (pos.x <= -8.0f)
        {
            pos.x = -8.0f;
        }
        if (pos.x >= 8.0f)
        {
            pos.x = 8.0f;
        }
        if (pos.y <= -3.0f)
        {
            pos.y = -3.0f;
        }
        if (pos.y >= 3.0f)
        {
            pos.y = 3.0f;
        }
        //Debug.Log(moveMagnitude);
        transform.position = pos;
    }
}
