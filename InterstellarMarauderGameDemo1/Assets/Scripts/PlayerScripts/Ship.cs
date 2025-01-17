using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Vector2 initialPosition;
    private RocketLauncher rocketLauncher;
    private PowerUp powerUp; // Reference to PowerUp component




    Gun[] guns;
    RocketLauncher[] rockets;

    float moveSpeed = 3;
    float speedMultiplier = 1;

    int hits = 10;
    bool invincible = false;
    float invincibleTimer = 0;
    float invincibleTime = 2;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool speedUp;

    bool shoot;
    bool rocketfire;

    SpriteRenderer spriteRenderer;

    GameObject shield;
    int powerUpGunLevel = 0;
    //public int rocketAmmo = 10; // add rocket ammo variable


    private void Awake()
    {
        initialPosition = transform.position;
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

        // Get reference to PowerUp component on this GameObject
        powerUp = GetComponent<PowerUp>();
        rocketLauncher = GetComponent<RocketLauncher>();

        shield = transform.Find("Shield").gameObject;
        DeactivateShield();
        guns = transform.GetComponentsInChildren<Gun>();
        rockets = transform.GetComponentsInChildren<RocketLauncher>();
        foreach (Gun gun in guns)
        {
            gun.isActive = true;
            if (gun.powerUpLevelRequirement != 0)
            {
                gun.gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        shoot = Input.GetKeyDown(KeyCode.LeftControl);
        if (shoot)
        {
            shoot = false;
            foreach (Gun gun in guns)
            {
                if (gun.gameObject.activeSelf)
                {
                    gun.Shoot();
                }
            }
        }


        rocketfire = Input.GetKeyDown(KeyCode.X);
        if (rocketfire)
        {
            rocketfire = false;
            foreach (RocketLauncher rocketlauncher in rockets)
            {
                if (rocketlauncher.gameObject.activeSelf)
                {
                    rocketlauncher.ShootRocket();
                }
            }
        }


        if (invincible)
        {

            if (invincibleTimer >= invincibleTime)
            {
                invincibleTimer = 0;
                invincible = false;
                spriteRenderer.enabled = true;
            }
            else
            {
                invincibleTimer += Time.deltaTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * speedMultiplier * Time.fixedDeltaTime;
        if (speedUp)
        {
            moveAmount *= 3;
        }
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        {
            move.y -= moveAmount;
        }

        if (moveLeft)
        {
            move.x -= moveAmount;
        }

        if (moveRight)
        {
            move.x += moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;
        if (pos.x <= -8f)
        {
            pos.x = -8f;
        }
        if (pos.x >= 8f)
        {
            pos.x = 8;
        }
        if (pos.y <= -3)
        {
            pos.y = -3;
        }
        if (pos.y >= 3)
        {
            pos.y = 3;
        }

        transform.position = pos;
    }



    void ActivateShield()
    {
        shield.SetActive(true);
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

    bool HasShield()
    {
        return shield.activeSelf;
    }




    void AddGuns()
    {
        powerUpGunLevel++;
        foreach (Gun gun in guns)
        {
            if (gun.powerUpLevelRequirement <= powerUpGunLevel)
            {
                gun.gameObject.SetActive(true);
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    void PowerUpAmmo()
    {
        RocketLauncher rocketLauncher = GetComponentInChildren<RocketLauncher>();
        if (rocketLauncher != null)
        {
            rocketLauncher.AddAmmo(5);
        }
    }


    void SetSpeedMultiplier(float mult)
    {
        speedMultiplier = mult;
    }



    void ResetShip()
    {
        transform.position = initialPosition;
        DeactivateShield();
        powerUpGunLevel = -1;
        AddGuns();
        SetSpeedMultiplier(1);
        hits = 10;
        Level.instance.ResetLevel();
    }


    void Hit(GameObject gameObjectHit)
    {
        if (HasShield())
        {
            DeactivateShield();
        }
        else
        {
            if (!invincible)
            {
                hits--;
                if (hits == 0)
                {
                    ResetShip();
                }
                else
                {
                    invincible = true;
                }
                Destroy(gameObjectHit);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.isEnemy)
            {
                Hit(bullet.gameObject);
            }
        }

        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
        {
            Hit(destructable.gameObject);
        }

        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp)
        {
            if (powerUp.activateShield)
            {
                ActivateShield();
            }

            if (powerUp.addGuns)
            {
                AddGuns();
            }
            if (powerUp.powerUpAmmo)
            {
                PowerUpAmmo();
            }


            if (powerUp.increaseSpeed)
            {
                SetSpeedMultiplier(speedMultiplier + 1);
            }
            Level.instance.AddScore(powerUp.pointValue);
            Destroy(powerUp.gameObject);
        }
    }
}
//}
