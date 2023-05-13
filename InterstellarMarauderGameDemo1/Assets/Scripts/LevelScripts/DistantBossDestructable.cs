using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantBossDestructable : MonoBehaviour
{
    public GameObject explosion;
    public int maxHealth = 3;
    private int currentHealth;
    bool canBeDestroyed = false;
    public int scoreValue = 100;
    public int bulletDamage = 1;
    public int rocketDamage = 2;
    public bool bulletsAffectEnemies = true;
    public bool rocketsAffectEnemies = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Level.instance.AddDestructable();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 12)
        {
            DestroyPowerUpDestructable();
        }

        if (transform.position.x < 17.0f && !canBeDestroyed)
        {
            canBeDestroyed = true;
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            foreach (Gun gun in guns)
            {
                gun.isActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }
        Bullet bullet = collision.GetComponent<Bullet>();
        Rocket rocket = collision.GetComponent<Rocket>();
        if (bullet != null && bulletsAffectEnemies)
        {
            if (!bullet.isEnemy)
            {
                currentHealth -= bulletDamage;
                if (currentHealth <= 0)
                {
                    Level.instance.AddScore(scoreValue);
                    DestroyPowerUpDestructable();
                }
                Destroy(bullet.gameObject);
            }
        }
        else if (rocket != null && rocketsAffectEnemies)
        {
            if (!rocket.isEnemy)
            {
                currentHealth -= rocketDamage;
                if (currentHealth <= 0)
                {
                    Level.instance.AddScore(scoreValue);
                    DestroyPowerUpDestructable();
                }
                Destroy(rocket.gameObject);
            }
        }
    }

    void DestroyPowerUpDestructable()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Level.instance.RemoveDestructable();
        Destroy(gameObject);
    }
}