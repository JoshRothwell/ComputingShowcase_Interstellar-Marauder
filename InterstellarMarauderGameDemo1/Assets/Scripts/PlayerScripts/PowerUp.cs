using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int pointValue = 1000;
    public bool activateShield;
    public bool addGuns;
    public bool increaseSpeed;
    public bool giveRocketAmmo;

    // Reference to the RocketLauncher script
    public RocketLauncher rocketLauncher;

    // Start is called before the first frame update
    void Start()
    {
        // Find the RocketLauncher script in the scene and store a reference to it
        rocketLauncher = GameObject.FindObjectOfType<RocketLauncher>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method is called when the player collides with the powerup
    public void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.CompareTag("PlayerShip"))
            {
                RocketLauncher launcher = other.GetComponent<RocketLauncher>();
                if (launcher != null)
                {
                    Debug.Log("Found RocketLauncher component on player ship.");
                    launcher.AddAmmo(5);
                }
            }
        

    }

}
