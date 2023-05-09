using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public int maxAmmo = 10; // Maximum number of rockets that can be fired
    public int currentAmmo; // Current number of rockets available to fire

    public Rocket rocket;
    Vector2 direction;

    public bool autoShootRocket = false;
    public float shootRocketIntervalSeconds = 0.5f;
    public float shootRocketDelaySeconds = 0.0f;
    float shootRocketTimer = 0f;
    float delayRocketTimer = 0f;

    public bool isActive = false;
    bool rocketfire;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo; // Initialize ammo to maximum
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        direction = (transform.localRotation * Vector2.right).normalized;

        if (autoShootRocket)
        {
            if (currentAmmo > 0)
            {
                if (delayRocketTimer >= shootRocketDelaySeconds)
                {
                    if (shootRocketTimer >= shootRocketIntervalSeconds)
                    {
                        ShootRocket();
                        shootRocketTimer = 0;
                    }
                    else
                    {
                        shootRocketTimer += Time.deltaTime;
                    }
                }
                else
                {
                    delayRocketTimer += Time.deltaTime;
                }
            }
        }
    }

    public void AddAmmo(int amount)
    {
        Debug.Log("Adding " + amount + " ammo to RocketLauncher...");
        currentAmmo += amount;
    }



    public void ShootRocket()
{
    if (currentAmmo > 0)
    {
        GameObject go = Instantiate(rocket.gameObject, transform.position, Quaternion.identity);
        Rocket goRocket = go.GetComponent<Rocket>();
        goRocket.direction = direction;
        currentAmmo--; // Decrease ammo count
        delayRocketTimer = 0f; // Reset delay timer
        Debug.Log("Ammo Count: " + currentAmmo);
    }
}


 
}