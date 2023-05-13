using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;

    public Bullet bullet;
    Vector2 direction;

    public GameObject PlayerShip;
    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;

    public bool isActive = false;

    // Reference to the parent object
    private GameObject parentObject;

    // The X position at which the turret gun becomes active
    public float activationXPosition = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = transform.parent.gameObject;
        PlayerShip = GameObject.Find("PlayerShip");

        if (PlayerShip == null)
        {
            Debug.LogError("PlayerShip not found!");
        }

        if (PlayerShip != null)
        {
            // Calculate direction from turret to player ship
            direction = PlayerShip.transform.position - transform.position;
            direction.Normalize();

            // Rotate turret to face player ship
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }




    // Update is called once per frame

    // Update is called once per frame
    void Update()
    {
        // Check if the parent object has reached the activation X position
        if (!isActive && parentObject.transform.position.x <= activationXPosition)
        {
            isActive = true;
        }

        if (!isActive)
        {
            return;
        }

        // Find the player object if it has not been assigned yet
        if (PlayerShip == null)
        {
            PlayerShip = GameObject.FindGameObjectWithTag("PlayerShip");
        }

        // If the player object is still null, return
        if (PlayerShip == null)
        {
            return;
        }

        // Calculate direction from turret to player ship
        direction = PlayerShip.transform.position - transform.position;
        direction.Normalize();

        // Rotate turret to face player ship
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Debug.Log("Direction: " + direction);
        Debug.Log("Angle: " + angle);

        if (autoShoot)
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= shootIntervalSeconds)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }
    }




    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, transform.rotation);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }
}

