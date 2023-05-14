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
    public AudioClip hitSound;
    public float spawnSoundVolume = 0.5f;
    public AudioClip hitRocketSound;
    public float spawnRocketSoundVolume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Level.instance.AddDestructable();
    }

    // Update is called once per frame
    void Update()
    {
      

        if (transform.position.x < 20.0f && !canBeDestroyed)
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
                AudioSource.PlayClipAtPoint(hitSound, transform.position, spawnSoundVolume);

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
                AudioSource.PlayClipAtPoint(hitRocketSound, transform.position, spawnRocketSoundVolume);

                currentHealth -= rocketDamage;
                //AudioSource.PlayClipAtPoint(hitRocketSound, transform.position, spawnRocketSoundVolume);

                if (currentHealth <= 0)
                {
                    Level.instance.AddScore(scoreValue);
                    DestroyDestructableBoss();
                }
                Destroy(rocket.gameObject);
                Instantiate(explosion, transform.position, Quaternion.identity);
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