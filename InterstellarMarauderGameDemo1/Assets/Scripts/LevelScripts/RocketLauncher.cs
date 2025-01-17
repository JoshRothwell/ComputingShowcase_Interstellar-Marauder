using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;

    public Rocket rocket;
    Vector2 direction;

    public bool autoShootRocket = false;
    public float shootRocketIntervalSeconds = 0.5f;
    public float shootRocketDelaySeconds = 0.0f;
    float shootRocketTimer = 0f;
    float delayRocketTimer = 0f;

    public bool isActive = false;
    bool rocketfire;

    private int currentAmmo = 10;

    // Start is called before the first frame update
    void Start()
    {
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

    public void ShootRocket()
    {
        if (currentAmmo > 0)
        {
            GameObject go = Instantiate(rocket.gameObject, transform.position, Quaternion.identity);
            Rocket goRocket = go.GetComponent<Rocket>();
            goRocket.direction = direction;
            goRocket.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            currentAmmo--; // Decrease ammo count
            delayRocketTimer = 0f; // Reset delay timer
            Debug.Log("Ammo Count: " + currentAmmo);
        }
    }

    public void AddAmmo(int amount)
    {
        Debug.Log("Adding " + amount + " ammo to RocketLauncher...");
        currentAmmo += amount;
    }
}




