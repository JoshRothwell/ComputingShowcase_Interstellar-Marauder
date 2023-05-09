using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestructable : MonoBehaviour
{
    public GameObject explosion;
    public int maxHealth = 3;
    private int currentHealth;
    bool canBeDestroyed = false;
    public int scoreValue = 100;
    public int bulletDamage = 1;
    public int rocketDamage = 2;
    public bool bulletsAffectBosses = true;
    public bool rocketsAffectBosses = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Level.instance.AddDestructable();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -10)
        {
            DestroyDestructableBoss();
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
        if (bullet != null && bulletsAffectBosses)
        {
            if (!bullet.isEnemy)
            {
                currentHealth -= bulletDamage;
                if (currentHealth <= 0)
                {
                    Level.instance.AddScore(scoreValue);
                    DestroyDestructableBoss();
                }
                Destroy(bullet.gameObject);
            }
        }
        else if (rocket != null && rocketsAffectBosses)
        {
            if (!rocket.isEnemy)
            {
                currentHealth -= rocketDamage;
                if (currentHealth <= 0)
                {
                    Level.instance.AddScore(scoreValue);
                    DestroyDestructableBoss();
                }
                Destroy(rocket.gameObject);
            }
        }
    }

    void DestroyDestructableBoss()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Level.instance.RemoveDestructable();
        Destroy(gameObject);
    }
}